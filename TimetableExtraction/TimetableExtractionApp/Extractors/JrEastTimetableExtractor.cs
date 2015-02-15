using Sgml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TimetableExtractionApp.Extractors
{
    public class JrEastTimetableExtractor
    {
        public List<TimetableExtractionResult> ExtractTimetable(int from, int to, bool consecutive)
        {
            var results = new List<TimetableExtractionResult>();

            var fromIndex = from;
            var toIndex = consecutive ? to : from;
            for (int staionIndex = fromIndex; staionIndex <= toIndex; staionIndex++)
            {
                try
                {
                    for (int railwayIndex = 1; railwayIndex < 20/*本当は親画面のレコード数*/; railwayIndex++)
                    {
                        for (int dayOfWeekIndex = 0; dayOfWeekIndex < 2; dayOfWeekIndex++)
                        {
                            var timetableURL = this.GetTimetableUrl(staionIndex, railwayIndex, dayOfWeekIndex);
                            var extractor = new JrEastTimetableExtractor();
                            var extractResult = extractor.Extract(timetableURL);
                            results.Add(extractResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 00, 01, 10, ... 行けるところまで、最後に例外で終了
                    Console.WriteLine(ex.ToString());
                }
            }

            return results;
        }

        private string GetTimetableUrl(int staionIndex, int railwayIndex, int dayOfWeekIndex)
        {
            // 左上の時刻表（平日）
            string timetableURL = string.Format(@"http://www.jreast-timetable.jp/1502/timetable/tt{0:D4}/{0:D4}{1:D2}{2}.html", staionIndex, railwayIndex, dayOfWeekIndex);
            return timetableURL;
        }
        
        public TimetableExtractionResult Extract(string timetableUrl)
        {
            TimetableExtractionResult extractResult = new TimetableExtractionResult()
                {
                    TimetableContents = new List<TimetableContent>()
                };
            extractResult.RailwayCompanyName = "東日本旅客鉄道";

            try
            {
                XDocument xml;
                using (var sgml = new SgmlReader() { Href = timetableUrl })
                {
                    xml = XDocument.Load(sgml);
                }

                // 名称マッピング
                var nameMappings = this.GetNameMappings(xml.Root);

                // 駅名・路線
                string stationNameAndRailwayLineName = this.GetStationNameAndRailwayLineName(xml, extractResult);

                // 起点を決め、巡回する
                var ns = xml.Root.Name.Namespace;
                var startCell = xml.DescendantNodes()
                    .OfType<XElement>()
                    .Where(element => element.Value == "時")
                    .FirstOrDefault();

                if(startCell != null)
                {
                    // 平日／土日
                    var dayOfTypeElement = startCell.ElementsAfterSelf().FirstOrDefault() as XElement;
                    extractResult.TypeOfDay = dayOfTypeElement == null ? string.Empty : dayOfTypeElement.Value;

                    startCell.Parent.ElementsAfterSelf().ToList().ForEach(timetablerecord =>
                        {
                            var hourElement = timetablerecord.Descendants().Where(innerElement1 =>
                                {
                                    return innerElement1.Attributes().Where(attr => attr.Name == "class" && attr.Value == "head-m").FirstOrDefault() != null;
                                }).FirstOrDefault();

                            if (hourElement != null)
                            {
                                // その時間帯に本数分class="time"の項目がある
                                var timeElements = timetablerecord.Descendants().Where(innerElement2 =>
                                    {
                                        return innerElement2.Attributes().Where(attr2 => attr2.Name == "class" && attr2.Value == "time").FirstOrDefault() != null;
                                    }).ToList();

                                // がんばれば、〇等の記号から、始発等の情報も出すことができる
                                timeElements.ForEach(timeElement =>
                                    {
                                        var timetableContent = new TimetableContent();

                                        // 行先「大船」当 無印あり
                                        timetableContent.Destination = this.GetDestinationMappingValue(nameMappings, this.GetTimetableContentValue(timeElement, "dist"));

                                        // train 列車種別「快速」無印は「普通」
                                        timetableContent.TrainClass = this.GetTrainClassMappingValue(nameMappings, this.GetTimetableContentValue(timeElement, "train"));

                                        // mark_etc「〇：当駅始発、■」等
                                        timetableContent.Notes = this.GetNotesMappingValue(nameMappings, this.GetTimetableContentValue(timeElement, "mark_etc"));

                                        // 分
                                        var minuteElement = timeElement.Descendants().Where(innerElement4 =>
                                            {
                                                return innerElement4.Attributes().Where(attr4 => attr4.Name == "class" && attr4.Value == "minute").FirstOrDefault() != null;
                                            }).FirstOrDefault();

                                        int minute = 0;
                                        if (minuteElement != null && int.TryParse(minuteElement.Value, out minute))
                                        {
                                            string format = string.Format("{0:D2}:{1}", hourElement.Value, minuteElement.Value);
                                            var departureTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, int.Parse(hourElement.Value), int.Parse(minuteElement.Value), 0);
                                            //var departureTime = DateTime.ParseExact(format, "HH:mm", null);

                                            timetableContent.DepartureTime = departureTime;
                                            extractResult.TimetableContents.Add(timetableContent);
                                        }
                                    });
                            }
                        });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            return extractResult;
        }

        private Dictionary<string, Dictionary<string, string>> GetNameMappings(XElement root)
        {
            var tablemarkTable = root.Descendants().Where(element =>
            {
                return element.Name == "table" && element.Attributes().Where(attr => attr.Name == "class" && attr.Value == "table_mark").FirstOrDefault() != null;
            })
            .FirstOrDefault();

            var nameMappings = new Dictionary<string, Dictionary<string, string>>();
            if(tablemarkTable != null)
            {
                nameMappings.Add("train", GetNameMapping(tablemarkTable, "列車種別・列車名 : "));
                nameMappings.Add("dist", GetNameMapping(tablemarkTable, "行き先・経由 : "));
                nameMappings.Add("mark_etc", GetNameMapping(tablemarkTable, "変更・注意マーク : "));
            }

            return nameMappings;
        }

        private string GetDestinationMappingValue(Dictionary<string, Dictionary<string, string>> nameMappings, string keyValue)
        {
            string mappedValue = keyValue;
            string adujestedKeyValue = string.IsNullOrWhiteSpace(keyValue) ? "無印" : keyValue;
            if (nameMappings.ContainsKey("dist") && nameMappings["dist"].ContainsKey(adujestedKeyValue))
            {
                mappedValue = nameMappings["dist"][adujestedKeyValue];
            }

            return mappedValue;
        }

        private string GetTrainClassMappingValue(Dictionary<string, Dictionary<string, string>> nameMappings, string keyValue)
        {
            string mappedValue = keyValue;
            string adujestedKeyValue = string.IsNullOrWhiteSpace(keyValue) ? "無印" : keyValue;
            if (nameMappings.ContainsKey("train") && nameMappings["train"].ContainsKey(adujestedKeyValue))
            {
                mappedValue = nameMappings["train"][adujestedKeyValue];
            }

            return mappedValue;
        }

        private string GetNotesMappingValue(Dictionary<string, Dictionary<string, string>> nameMappings, string keyValue)
        {
            string mappedValue = keyValue;
            string adujestedKeyValue = keyValue == "◆" ? "(斜体)◆" : keyValue;
            if (adujestedKeyValue != null && nameMappings.ContainsKey("mark_etc") && nameMappings["mark_etc"].ContainsKey(adujestedKeyValue))
            {
                mappedValue = nameMappings["mark_etc"][adujestedKeyValue];
            }

            return mappedValue;
        }

        private Dictionary<string, string> GetNameMapping(XElement tablemarkTable, string mappingTarget)
        {
            var nameMapping = new Dictionary<string, string>();
            var labelCell = tablemarkTable.Descendants().Where(element => element.Name == "td" && element.Value == mappingTarget).FirstOrDefault();
            if (labelCell != null)
            {
                var trainClasses = labelCell.Parent.Descendants().Where(element => element.Name == "nobr").ToList();
                trainClasses.ForEach(element =>
                {
                    var keyAndValue = element.Value.Split(new string[] { "=" }, StringSplitOptions.None);
                    var keys = keyAndValue[0].Split('・');
                    foreach (var key in keys)
                    {
                        nameMapping.Add(key, keyAndValue[1]);
                    }
                });
            }

            return nameMapping;
        }

        private string GetTimetableContentValue(XElement timeElement, string classAttrValue)
        {
            string valueString = null;

            var targetElement = timeElement.Descendants().Where(innerElement3 =>
            {
                return innerElement3.Attributes().Where(attr3 => attr3.Name == "class" && attr3.Value == classAttrValue).FirstOrDefault() != null;
            }).FirstOrDefault();

            if (targetElement != null)
            {
                valueString = targetElement.Value;
            }

            return valueString;
        }

        private string GetStationNameAndRailwayLineName(XDocument xml, TimetableExtractionResult extractionResult)
        {
            string stationNameAndRailwayLineName = string.Empty;

            // imgが閉じられていない等、XHTMLに対応していないと想定した操作にならない
            var ns = xml.Root.Name.Namespace;
            //var targetCell = xml.Elements()
            //    .OfType<XElement>()
            //    .Where(rootElement => rootElement.Name == "html")
            //    .FirstOrDefault()
            //    .Elements()
            //    .Where(element => element.Name == "body")
            //    .FirstOrDefault()
            //    .Elements()
            //    .Where(elementTable => 
            //        {
            //            return elementTable.Name == "table";
            //        })
            //    .ElementAt(4)
            //    .Descendants()
            //    .Where(elementInner => elementInner.Attributes().Where(attr1 => attr1.Name == "class" && attr1.Value == "text-l").FirstOrDefault() != null)
            //    .FirstOrDefault();

            // 新宿(866)だと12番目ではないっぽい
            //var targetCell = xml.Descendants().Where(element => element.Name == "table").ElementAt(12)
            //    .Descendants()
            //    .Where(elementInner => elementInner.Attributes().Where(attr1 => attr1.Name == "class" && attr1.Value == "text-l").FirstOrDefault() != null)
            //    .FirstOrDefault();

            // 丸ごとなめて、最初に見つかったものを採用
            var targetCell = xml.Descendants().Where(element =>
                {
                    return element.Name == "table" && Regex.IsMatch(element.Value, @".+駅.+方面", RegexOptions.Singleline);
                })
                .Descendants()
                .Where(elementInner => elementInner.Attributes().Where(attr1 => attr1.Name == "class" && attr1.Value == "text-l").FirstOrDefault() != null)
                .FirstOrDefault();

            if(targetCell != null)
            {
                var stationInfos = targetCell.Value.Trim().Split(' ', '　').Where(str => !string.IsNullOrEmpty(str));
                extractionResult.StationName = stationInfos.ElementAtOrDefault(0);
                extractionResult.StationName = extractionResult.StationName.TrimEnd('駅');
                extractionResult.RailwayLineName = stationInfos.ElementAtOrDefault(1);
                extractionResult.Direction = string.Join("", stationInfos.ElementAtOrDefault(2), stationInfos.ElementAtOrDefault(3));
                stationNameAndRailwayLineName = targetCell.Value;
            }

            return stationNameAndRailwayLineName;
        }
    }
}

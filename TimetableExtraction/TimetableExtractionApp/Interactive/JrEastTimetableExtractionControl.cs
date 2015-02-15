using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimetableExtractionApp.Extractors;

namespace TimetableExtractionApp.Interactive
{
    public partial class JrEastTimetableExtractionControl : UserControl
    {
        public JrEastTimetableExtractionControl()
        {
            InitializeComponent();
        }

        private void extractTimetableButton_Click(object sender, EventArgs e)
        {
            try
            {
                var fromIndex = (int)this.consecutiveFromNumericUpDown.Value;
                var toIndex = this.consecutiveCheckBox.Checked ? (int)this.consecutiveToNumericUpDown.Value : fromIndex;

                var extractor = new JrEastTimetableExtractor();
                var results = extractor.ExtractTimetable(fromIndex, toIndex, this.consecutiveCheckBox.Checked);
                this.TraceOutResults(results);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TraceOutResults(List<TimetableExtractionResult> results)
        {
            StringBuilder builder = new StringBuilder();
            results.ForEach(result =>
                {
                    builder
                        .AppendFormat("RailwayCompanyName:{0}", result.RailwayCompanyName).AppendLine()
                        .AppendFormat("RailwayLineName:{0}", result.RailwayLineName).AppendLine()
                        .AppendFormat("StationName:{0}", result.StationName).AppendLine()
                        .AppendFormat("TypeOfDay:{0}", result.TypeOfDay).AppendLine()
                        .AppendFormat("Direction:{0}", result.Direction).AppendLine()
                        .AppendLine("TimetableContents:");

                    result.TimetableContents.ForEach(timetableContent => 
                        {
                            builder
                                .AppendFormat(" DepartureTime:{0}", timetableContent.DepartureTime.ToString("HH:mm"))
                                .AppendFormat(" Destination:{0}", timetableContent.Destination)
                                .AppendFormat(" TrainClass:{0}", timetableContent.TrainClass)
                                .AppendFormat(" Notes:{0}", timetableContent.Notes)
                                .AppendLine();
                        });
                });

            this.traceTextBox.Text = builder.ToString();
        }
    }
}

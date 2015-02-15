using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableExtractionApp.Extractors
{
    public class TimetableExtractionResult
    {
        public string RailwayCompanyName { get; set; }
        public string StationName { get; set; }
        public string RailwayLineName { get; set; }
        public string Direction { get; set; }
        public string TypeOfDay { get; set; }
        public List<TimetableContent> TimetableContents { get; set; }
    }

    public class TimetableContent
    {
        public string Destination { get; set; }
        public string TrainClass { get; set; }
        public string Notes { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}

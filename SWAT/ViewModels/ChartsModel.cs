using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts;

namespace SWAT.ViewModels
{
    public class ChartsModel
    {
        public Highcharts OverallChart { get; set; }
        public Highcharts DetailsChart { get; set; }
        public Highcharts BackgroundChart { get; set; }
        public Highcharts WAChart { get; set; }
        public Highcharts CCChart { get; set; }
        public Highcharts ReportChart { get; set; }
        public Highcharts WPChart { get; set; }
        public Highcharts WPDetailsChart { get; set; }
        public List<Highcharts> SectionChartList { get; set; }
        public List<Highcharts> WPChartList { get; set; }
    }
}
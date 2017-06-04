using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Attributes;
using Point = DotNet.Highcharts.Options.Point;
using SWAT.Models;
using SWAT.ViewModels;

namespace SWAT.Controllers
{
    public class SurveyController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /Survey/
        //public ActionResult Index()
        //{
        //    var tblswatsurveys = db.tblSWATSurveys.Include(t => t.tblSWATLocation).Include(t => t.Userid1);
        //    return View(tblswatsurveys.ToList());
        //}

        // Return subcomponent score by giving a subcomponentID and surveyID
        private double? getSubcomponentScore(int SurveyID, int SubcomponentID)
        {
            double? subcomScore = null;
           
            var scoreIDs = db.lkpswatindicatorsubcomponentlus.Where(e => e.SubComponentID == SubcomponentID).Select(e => e.ScoreVarID).ToList();
            int scoreCounter = 0;
            foreach (int scoreVarID in scoreIDs)
            {
                double? score = db.tblswatscores.Single(e => e.SurveyID == SurveyID && e.VariableID == scoreVarID).Value;
                if (score != null)
                {
                    subcomScore = subcomScore.GetValueOrDefault(0) + (double)score * 100;
                    scoreCounter++;
                }
            }
            if (scoreCounter > 0)
            {
                return subcomScore / scoreCounter;
            }
            return subcomScore;
        }

        // Return component score by giving a ComponentID and SurveyID
        private double? getComponentScore(int SurveyID, int ComponentID)
        {
            double? comScore = null;

            int[] subComIDs = db.lkpswatsubcomponentlus.Include(t => t.lkpswatcomponentlu).Where(e => e.ComponentID == ComponentID).Select(e => e.ID).ToArray();
            int scoreCounter = 0;

            foreach (int subComID in subComIDs)
            {
                double? score = getSubcomponentScore(SurveyID, subComID);
                if (score != null)
                {
                    comScore = comScore.GetValueOrDefault(0) + score;
                    scoreCounter++;
                }
            }
            if (scoreCounter > 0)
            {
                return comScore / scoreCounter;
            }
            return comScore;

        }

        // Return Indicator group score by giving a SurveyID and GroupID
        private double? getGroupScore(int SurveyID, int GroupID)
        {
            double? groupScore = null;
            int[] comIDs = db.lkpswatcomponentlus.Include(t => t.IndicatorGroupID).Include(t => t.lkpswatindicatorgrouplu).Include(t => t.lkpswatsubcomponentlus).Where(e => e.IndicatorGroupID == GroupID).Select(e => e.id).ToArray();
            int scoreCounter = 0;

            foreach (int comID in comIDs)
            {
                double? score = getComponentScore(SurveyID, comID);
                if (score != null)
                {
                    groupScore = groupScore.GetValueOrDefault(0) + score;
                    scoreCounter++;
                }
            }
            if (scoreCounter > 0)
            {
                return groupScore / scoreCounter;
            }
            return groupScore;  
        }
        
        // Return a chart Point by giving surveyID and indicatorgroupID
        private Point getGroupScorePoint(int SurveyID, int IndicatorGroupID)
        {
            string[] colors = { "#006699", "#FFCC66", "#A5DF00", "#4BACC6", "#C0504D", "#C5D9F1" };
            double? score = getGroupScore(SurveyID, IndicatorGroupID);
            
            Point column = new Point { Y = score.GetValueOrDefault(0), Color = ColorTranslator.FromHtml(colors[IndicatorGroupID - 1]) };
            return column;
        }

        // Return a chart Point by giving surveyID and componentID
        private Point getComponentScorePoint(int SurveyID, int ComponentID)
        {
            string[] colors = { "#006699", "#FFCC66", "#A5DF00", "#4BACC6", "#C0504D", "#C5D9F1" };
            double? score = getComponentScore(SurveyID, ComponentID);
            int colorIndex = (int)db.lkpswatcomponentlus.Find(ComponentID).IndicatorGroupID - 1;
            Point column = new Point { Y = score.GetValueOrDefault(0), Color = ColorTranslator.FromHtml(colors[colorIndex]) };
            return column;
        }

        private YAxisPlotBands[] getYGradient()
        {
            YAxisPlotBands[] ygradient = new YAxisPlotBands[100];
            const int step = 5;
            string red = "FF";
            string green = "00";
            string blue = "00";

            for (int i = 0; i < 50; i++)
            {
                int decVal = i * step;
                green = decVal.ToString("X");
                if (decVal < 16)
                {
                    green = "0" + green;
                }

                string colorCode1 = "#" + red + green + blue;
                string colorCode2 = "#" + green + red + blue;
                ygradient[i] = new YAxisPlotBands { From = i, To = i + 1, Color = ColorTranslator.FromHtml(colorCode1) };
                ygradient[99 - i] = new YAxisPlotBands { From = 99 - i, To = 100 - i, Color = ColorTranslator.FromHtml(colorCode2) };
            }

            return ygradient;
        }

        private Highcharts getOverviewChart(int id, tblswatsurvey tblswatsurvey)
        {
            List<Point> groupPointList = new List<Point> { };

            foreach (var item in db.lkpswatindicatorgrouplus.ToList())
            {
                groupPointList.Add(getGroupScorePoint((int)id, item.ID));
            }
            
            Data data = new Data(groupPointList.ToArray());

            String[] categories = db.lkpswatindicatorgrouplus.Select(e => e.Description).ToArray();

            Highcharts barColumn = new Highcharts("overallchart")
                .InitChart(new Chart
                {
                    DefaultSeriesType = ChartTypes.Column,
                    Width = 720
                })
                .SetTitle(new Title { Text = "Overall Result - " + tblswatsurvey.tblswatlocation.name })
                .SetXAxis(new XAxis { Categories = categories })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Max = 100,
                    Title = new YAxisTitle { Text = "Index" },
                    PlotBands = getYGradient(),
                    GridLineColor = ColorTranslator.FromHtml("#999999"),
                    GridLineWidth = 1
                })
                .SetLegend(new Legend { Enabled = false })
                .SetCredits(new Credits { Enabled = false })
                .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.y; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0,
                        BorderWidth = 1,
                        BorderColor = ColorTranslator.FromHtml("#555555")
                    }
                })
                .SetExporting(new Exporting
                {
                    Buttons = new ExportingButtons
                    {
                        ContextButton = new ExportingButtonsContextButton { SymbolFill = ColorTranslator.FromHtml("#55BE3B") },
                    }
                })
                .SetSeries(
                        new Series { Name = tblswatsurvey.tblswatlocation.name, Data = data }
                    );
            return barColumn;
        }

        private Highcharts getDetailsChart(int id, tblswatsurvey tblswatsurvey)
        {
            // Details result chart
            List<Point> comPointList = new List<Point> { };

            foreach (var item in db.lkpswatcomponentlus.ToList())
            {
                comPointList.Add(getComponentScorePoint((int)id, item.id));
            }
            Data detailsData = new Data(comPointList.ToArray());
            String[] categories = db.lkpswatcomponentlus.Select(e => e.Description).ToArray();
            Highcharts detailsBarColumn = new Highcharts("detailschart")
                .InitChart(new Chart { 
                    DefaultSeriesType = ChartTypes.Column,
                    Width = 720
                })
                .SetTitle(new Title { Text = "Details Result - " + tblswatsurvey.tblswatlocation.name })
                .SetXAxis(new XAxis
                {
                    Categories = categories,
                    Labels = new XAxisLabels
                    {
                        Rotation = -90,
                        Align = HorizontalAligns.Right
                    }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Max = 100,
                    Title = new YAxisTitle { Text = "Index" },
                    PlotBands = getYGradient(),
                    GridLineColor = ColorTranslator.FromHtml("#999999"),
                    GridLineWidth = 1
                })
                .SetLegend(new Legend { Enabled = false })
                .SetCredits(new Credits { Enabled = false })
                .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.y; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0,
                        BorderWidth = 1,
                        BorderColor = ColorTranslator.FromHtml("#555555")
                    }
                })
                .SetExporting(new Exporting
                {
                    Buttons = new ExportingButtons
                    {
                        ContextButton = new ExportingButtonsContextButton { SymbolFill = ColorTranslator.FromHtml("#55BE3B") },
                    }
                })
                .SetSeries(
                        new Series { Name = tblswatsurvey.tblswatlocation.name, Data = detailsData }
                    );

            return detailsBarColumn;
        }

        private Highcharts getGroupChart(int id, int reportID, tblswatsurvey tblswatsurvey)
        {
            List<Point> comPointList = new List<Point> { };

            foreach (var item in db.lkpswatcomponentlus.Where(e => e.IndicatorGroupID == reportID).ToList())
            {
                comPointList.Add(getComponentScorePoint(id, item.id));
            }
            Data comData = new Data(comPointList.ToArray());
            String[] categories = db.lkpswatcomponentlus.Where(e => e.IndicatorGroupID == reportID).Select(e => e.Description).ToArray();

            Highcharts groupChart = new Highcharts("groupchart")
                .InitChart(new Chart { 
                    DefaultSeriesType = ChartTypes.Column,
                    Width = 720
                })
                .SetTitle(new Title { Text = db.lkpswatindicatorgrouplus.Find(reportID).Description + " - " + tblswatsurvey.tblswatlocation.name })
                .SetXAxis(new XAxis
                {
                    Categories = categories,
                    Labels = new XAxisLabels
                    {
                        Rotation = 0,
                        Align = HorizontalAligns.Center
                    }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Max = 100,
                    Title = new YAxisTitle { Text = "Index" },
                    PlotBands = getYGradient(),
                    GridLineColor = ColorTranslator.FromHtml("#999999"),
                    GridLineWidth = 1
                })
                .SetLegend(new Legend { Enabled = false })
                .SetCredits(new Credits { Enabled = false })
                .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.y; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0,
                        BorderWidth = 1,
                        BorderColor = ColorTranslator.FromHtml("#555555")
                    }
                })
                .SetExporting(new Exporting
                {
                    Buttons = new ExportingButtons
                    {
                        ContextButton = new ExportingButtonsContextButton { SymbolFill = ColorTranslator.FromHtml("#55BE3B") },
                    }
                })
                .SetSeries(
                        new Series { Name = tblswatsurvey.tblswatlocation.name, Data = comData }
                    );

            return groupChart;
        }

        public ActionResult _BarColumn(int? id, int? reportID)
        {
            if (id == null || reportID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return HttpNotFound();
            }

            ChartsModel barCharts = new ChartsModel();

            if (reportID == -1)
            {
                barCharts.ReportChart = getOverviewChart((int)id, tblswatsurvey);
            }
            else if (reportID == -2)
            {
                barCharts.ReportChart = getDetailsChart((int)id, tblswatsurvey);
            }
            else
            {
                barCharts.ReportChart = getGroupChart((int)id, (int)reportID, tblswatsurvey);
            }
            
            return PartialView(barCharts);
        }

        private double? getWPSubcomponentScore(long wpID, int subcomID)
        {
            double? subcomScore = null;
            int scoreCount = 0;
            int[] scoreIDs = db.lkpswatwpscoresubcomponentlus.Where(e => e.SubcomponentID == subcomID).Select(e => e.ScoreID).ToArray();
            var wpScores = db.tblswatwpscores.Where(e => e.wpID == wpID);

            foreach (int scoreID in scoreIDs)
            {
                double? score = wpScores.Single(e => e.ScoreID == scoreID).Value;
                if (score != null)
                {
                    subcomScore = subcomScore.GetValueOrDefault(0) + (double)score*100;
                    scoreCount++;
                }
            }

            if (scoreCount > 0)
            {
                return subcomScore / scoreCount;
            }
            return subcomScore;
        }

        private double? getWPComponetScore(long wpID, int componentID)
        {
            double? comScore = null;
            int scoreCount = 0;
            lkpswatwpcomponentlu component = db.lkpswatwpcomponentlus.Find(componentID);
            if (component.hasSubcomponent)
            {
                int[] subcomIDs = db.lkpswatwpsubcomponentlus.Where(e => e.ComponentID == componentID).Select(e => e.id).ToArray();
                foreach (int subcomID in subcomIDs)
                {
                    double? subcomScore = getWPSubcomponentScore(wpID, subcomID);
                    if (subcomScore != null)
                    {
                        comScore = comScore.GetValueOrDefault(0) + subcomScore;
                        scoreCount++;
                    }
                }
            }
            int[] scoreIDs = db.lkpswatwpscorecomponentlus.Where(e => e.ComponentID == componentID).Select(e => e.ScoreID).ToArray();
            var wpScores = db.tblswatwpscores.Where(e => e.wpID == wpID).ToList();
            foreach (int scoreID in scoreIDs)
            {
                //double? score = db.tblswatwpscores.Single(e => e.ScoreID == scoreID && e.wpID == wpID).Value;
                double? score = wpScores.Single(e => e.ScoreID == scoreID).Value;
                if (score != null)
                {
                    comScore = comScore.GetValueOrDefault(0) + (double)score*100;
                    scoreCount++;
                }
            }

            if (scoreCount > 0)
            {
                return comScore / scoreCount;
            }
            return comScore;
        }

        private double? getWPGroupScore(long wpID, int indicatorGroupID)
        {
            double? groupScore = null;
            int[] comIDs = db.lkpswatwpcomponentlus.Where(e => e.IndicatorGroupID == indicatorGroupID).Select(e => e.ID).ToArray();
            int scoreCount = 0;

            foreach (int comID in comIDs)
            {
                double? score = getWPComponetScore(wpID, comID);
                if (score != null)
                {
                    groupScore = groupScore.GetValueOrDefault(0) + score;
                    scoreCount++;
                }
            }

            if (scoreCount > 0)
            {
                return groupScore / scoreCount;
            }

            return groupScore;
        }

        private Point getWPAvgGroupScorePoint(int id, int groupID)
        {
            double? scoreSum = null;
            int scoreCount = 0;
            var wps = db.tblswatwpoverviews.Where(e => e.SurveyID == id).ToList();
            foreach (var item in wps)
            {
                double? wpScore = getWPGroupScore(item.ID, groupID);
                if (wpScore != null)
                {
                    scoreSum = scoreSum.GetValueOrDefault(0) + wpScore;
                    scoreCount++;
                }
            }
            Point groupPoint = null;
            if (scoreCount > 0)
            {
                groupPoint = new Point { Y = (double)scoreSum.GetValueOrDefault(0) / scoreCount };
            }
            else
            {
                groupPoint = new Point { Y = 0};
            }

            return groupPoint;
        }

        private Point getWPGroupScorePoint(int indicatorGroupID, long wpID)
        {
            double? wpScore = getWPGroupScore(wpID, indicatorGroupID);
            
            Point wpGroupPoint = new Point { Y = wpScore.GetValueOrDefault(0)};
            return wpGroupPoint;
        }

        private Highcharts getWPChart(int id, long wpID, tblswatsurvey tblswatsurvey)
        {
            string avgColor = "#019FDE";
            List<Point> groupPointList = new List<Point> { };
            var groups = db.lkpswatwpindicatorgrouplus.ToList();
            foreach (var item in groups)
            {
                groupPointList.Add(getWPAvgGroupScorePoint((int)id, item.ID));
            }

            Data groupData = new Data(groupPointList.ToArray());

            List<Series> wpSeries = new List<Series> { };
            wpSeries.Add(new Series { Name = "Average", Color = ColorTranslator.FromHtml(avgColor), Data = groupData });

            if (wpID > 0)
            {
                string wpColor = "#E3E8F4";
                List<Point> wpPointList = new List<Point> { };
                foreach (var item in groups)
                { 
                    wpPointList.Add(getWPGroupScorePoint(item.ID, wpID));
                }
                Data wpData = new Data(wpPointList.ToArray());
                wpSeries.Add(new Series { Name = db.tblswatwpoverviews.Find(wpID).wpname, Color = ColorTranslator.FromHtml(wpColor), Data = wpData });
            }

            String[] categories = db.lkpswatwpindicatorgrouplus.Select(e => e.Description).ToArray();
            Highcharts wpchart = new Highcharts("wpchart")
                .InitChart(new Chart { 
                    DefaultSeriesType = ChartTypes.Column,
                    Width = 720
                })
                .SetTitle(new Title { Text = "Water Point Assessment" + " - " + tblswatsurvey.tblswatlocation.name })
                .SetXAxis(new XAxis
                {
                    Categories = categories,
                    Labels = new XAxisLabels
                    {
                        Rotation = 0,
                        Align = HorizontalAligns.Center
                    }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Max = 100,
                    Title = new YAxisTitle { Text = "Index" },
                    PlotBands = getYGradient(),
                    GridLineColor = ColorTranslator.FromHtml("#999999"),
                    GridLineWidth = 1
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Horizontal,
                    Align = HorizontalAligns.Center,
                    VerticalAlign = VerticalAligns.Bottom,
                    Floating = false,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true,
                    Enabled = true
                })
                .SetCredits(new Credits { Enabled = false })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.x + ': '+ this.y; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0,
                        BorderWidth = 1,
                        BorderColor = ColorTranslator.FromHtml("#555555")
                    }
                })
                .SetExporting(new Exporting
                {
                    Buttons = new ExportingButtons
                    {
                        ContextButton = new ExportingButtonsContextButton { SymbolFill = ColorTranslator.FromHtml("#55BE3B") },
                    }
                })
                .SetSeries(
                        wpSeries.ToArray()
                    );
            return wpchart;
        }

        private Point getWPAvgComScorePoint(int id, int comID)
        {
            double? scoreSum = null;
            int scoreCount = 0;
            var wps = db.tblswatwpoverviews.Where(e => e.SurveyID == id).ToList();

            foreach (var item in wps)
            {
                double? wpScore = getWPComponetScore(item.ID, comID);
                if (wpScore != null)
                {
                    scoreSum = scoreSum.GetValueOrDefault(0) + wpScore;
                    scoreCount++;
                }
            }

            Point comPoint = null;

            if (scoreCount > 0)
            {
                comPoint = new Point { Y = (double)scoreSum.GetValueOrDefault(0) / scoreCount };
            }
            else
            {
                comPoint = new Point { Y = 0 };
            }

            return comPoint;
        }

        private Point getWPComScorePoint(int comID, long wpID)
        {
            double? wpScore = getWPComponetScore(wpID, comID);

            Point wpComPoint = new Point { Y = wpScore.GetValueOrDefault(0) };
            return wpComPoint;
        }
        
        private Highcharts getWPDetailsChart(int id, long wpID, int indicatorGroupID, tblswatsurvey tblswatsurvey)
        {
            string avgColor = "#019FDE";
            List<Point> comPointList = new List<Point> { };
            var coms = db.lkpswatwpcomponentlus.Where(e => e.IndicatorGroupID == indicatorGroupID).ToList();
            foreach (var item in coms)
            {
                comPointList.Add(getWPAvgComScorePoint((int)id, item.ID));
            }

            Data groupData = new Data(comPointList.ToArray());

            List<Series> wpSeries = new List<Series> { };
            wpSeries.Add(new Series { Name = "Average", Color = ColorTranslator.FromHtml(avgColor), Data = groupData });

            if (wpID > 0)
            {
                string wpColor = "#E3E8F4";
                List<Point> wpPointList = new List<Point> { };
                foreach (var item in coms)
                {
                    wpPointList.Add(getWPComScorePoint(item.ID, wpID));
                }
                Data wpData = new Data(wpPointList.ToArray());
                wpSeries.Add(new Series { Name = db.tblswatwpoverviews.Find(wpID).wpname, Color = ColorTranslator.FromHtml(wpColor), Data = wpData });
            }

            String[] categories = db.lkpswatwpcomponentlus.Where(e => e.IndicatorGroupID == indicatorGroupID).Select(e => e.Description).ToArray();
            Highcharts wpchart = new Highcharts("wpdetailschart")
                .InitChart(new Chart { 
                    DefaultSeriesType = ChartTypes.Column,
                    Width = 720
                })
                .SetTitle(new Title { Text = db.lkpswatwpindicatorgrouplus.Find(indicatorGroupID).Description + " - " + tblswatsurvey.tblswatlocation.name })
                .SetXAxis(new XAxis
                {
                    Categories = categories,
                    Labels = new XAxisLabels
                    {
                        Rotation = 0,
                        Align = HorizontalAligns.Center
                    }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Max = 100,
                    Title = new YAxisTitle { Text = "Index" },
                    PlotBands = getYGradient(),
                    GridLineColor = ColorTranslator.FromHtml("#999999"),
                    GridLineWidth = 1
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Horizontal,
                    Align = HorizontalAligns.Center,
                    VerticalAlign = VerticalAligns.Bottom,
                    Floating = false,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true,
                    Enabled = true
                })
                .SetCredits(new Credits { Enabled = false })
                .SetTooltip(new Tooltip { Formatter = @"function() { return this.x + ': '+ this.y; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0,
                        BorderWidth = 1,
                        BorderColor = ColorTranslator.FromHtml("#555555")
                    }
                })
                .SetExporting(new Exporting
                {
                    Buttons = new ExportingButtons
                    {
                        ContextButton = new ExportingButtonsContextButton { SymbolFill = ColorTranslator.FromHtml("#55BE3B") },
                    }
                })
                .SetSeries(
                        wpSeries.ToArray()
                    );
            return wpchart;
        }

        public ActionResult _WPGroupBarColumn(int? id, long? wpID, int? metricID)
        {
            if (id == null || wpID == null || metricID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return HttpNotFound();
            }

            ChartsModel barCharts = new ChartsModel();
            barCharts.WPDetailsChart = getWPDetailsChart((int)id, (long)wpID, (int) metricID, tblswatsurvey);

            return PartialView(barCharts);
        }

        public ActionResult _WPBarColumn(int? id, long? wpID)
        {
            if (id == null || wpID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return HttpNotFound();
            }

            ChartsModel barCharts = new ChartsModel();
            barCharts.WPChart = getWPChart((int)id, (long) wpID, tblswatsurvey);

            return PartialView(barCharts);
        }

        public ActionResult _WPreportlist(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            ViewBag.numWP = db.tblswatwpoverviews.Where(e => e.SurveyID == id).Count();

            return PartialView(tblswatsurvey);
        }

        // GET: /Survey/Report/5
        public ActionResult Report(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> reportList = new List<SelectListItem>();
            reportList.Add(new SelectListItem() { Text = "Overview", Value = "-1", Selected = true });
            reportList.Add(new SelectListItem() { Text = "Details", Value = "-2", Selected = false });
            foreach (var item in db.lkpswatindicatorgrouplus.OrderBy(e => e.ID).ToList())
            {
                reportList.Add(new SelectListItem() { Text = item.Description, Value = item.ID.ToString(), Selected = false });
            }


            ViewBag.ReportList = reportList;
            return View(tblswatsurvey);
        }

        public ActionResult WPReport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> wpList = new List<SelectListItem>();
            wpList.Add(new SelectListItem() { Text = String.Empty, Value = "0", Selected = true });
            foreach (var item in db.tblswatwpoverviews.Where(e => e.SurveyID == id).OrderBy(e => e.ID))
            {
                wpList.Add(new SelectListItem() { Text = item.wpname, Value = item.ID.ToString(), Selected = false });
            }

            List<SelectListItem> metricList = new List<SelectListItem>();
            //metricList.Add(new SelectListItem() { Text = String.Empty, Value = "0", Selected = true });
            foreach (var item in db.lkpswatwpindicatorgrouplus.OrderBy(e => e.ID))
            {
                if (item.ID > 1)
                {
                    metricList.Add(new SelectListItem() { Text = item.Description, Value = item.ID.ToString(), Selected = false });
                }
                else
                {
                    metricList.Add(new SelectListItem() { Text = item.Description, Value = item.ID.ToString(), Selected = true });
                }
            }

            ViewBag.metricList = metricList;
            ViewBag.wpList = wpList;
            return View(tblswatsurvey);
        }

        private double getSectionProgress(int id, int sectionID)
        {
            int scores = db.tblswatscores.Include(t => t.lkpswatscorevarslu).Where(e => e.lkpswatscorevarslu.SectionID == sectionID && e.SurveyID == id).Count();
            int answered = db.tblswatscores.Include(t => t.lkpswatscorevarslu).Where(e => e.lkpswatscorevarslu.SectionID == sectionID && e.SurveyID == id && e.Value != null).Count();

            if (sectionID == 1 || sectionID == 5)
            {
                return (double) answered / scores;
            }
            if (sectionID == 2)
            {
                // check risk scores
                int fireRiskID = db.lkpswatscorevarslus.First(e => e.VarName == "riskFireSCORE" && e.SectionID == sectionID).ID;
                if (fireRiskID > 0)
                {
                    double? score = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == fireRiskID).Value;
                    if (score == null || score <= 0.25)
                    {
                        scores--;
                    }
                }

                int floodRiskID = db.lkpswatscorevarslus.First(e => e.VarName == "riskFloodSCORE" && e.SectionID == sectionID).ID;
                if (floodRiskID > 0)
                {
                    double? score = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == floodRiskID).Value;
                    if (score == null || score <= 0.25)
                    {
                        scores--;
                    }
                }

                int droughtRiskID = db.lkpswatscorevarslus.First(e => e.VarName == "riskDroughtSCORE" && e.SectionID == sectionID).ID;
                if (droughtRiskID > 0)
                {
                    double? score = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == droughtRiskID).Value;
                    if (score == null || score <= 0.25)
                    {
                        scores--;
                    }
                }
                return (double)answered / scores;
            }

            if (sectionID == 3)
            {
                int fundAppID = db.lkpswatscorevarslus.First(e => e.VarName == "fundApp_LINKSCORE" && e.SectionID == sectionID).ID;
                if (fundAppID > 0)
                {
                    double? score = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == fundAppID).Value;
                    if (score == null || score < 1)
                    {
                        scores--;
                    }
                }
                return (double)answered / scores;
            }

            if (sectionID == 4)
            {
                int swpSection = 0;
                tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == id);

                int? ls = bg.isEconLs;
                int? ag = bg.isEconAg;
                int? dev = bg.isEconDev;

                var swpag = db.tblswatswpags.Where(e => e.SurveyID == id);
                var swpdev = db.tblswatswpdevs.Where(e => e.SurveyID == id);
                var swpls = db.tblswatswpls.Where(e => e.SurveyID == id);
                if (dev == 1511)
                {
                    swpSection = 3;
                }
                if (ag == 1511)
                {
                    swpSection = 2;
                }
                if (ls == 1511)
                {
                    swpSection = 1;
                }

                if (ls == null || ls != 1511)
                {
                    scores = scores - 3;
                }
                else
                {
                    int livestockScoreID = db.lkpswatscorevarslus.First(e => e.VarName == "livestockSCORE" && e.SectionID == sectionID).ID;
                    if (livestockScoreID > 0)
                    {
                        double? score = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == livestockScoreID).Value;
                        if (score == null || score != 1)
                        {
                            scores--;
                        }
                    }
                }

                if (ag == null || ag != 1511)
                {
                    scores = scores - 6;
                }
                
                if (dev == null || dev != 1511)
                {
                    scores = scores - 3;
                }
                else
                {
                    if (swpdev.Any())
                    {
                        int? devSiteTotal = db.tblswatswpdevs.First(e => e.SurveyID == id).devSiteTOTAL;
                        if (devSiteTotal == null || devSiteTotal <= 0)
                        {
                            scores--;
                        }
                    }
                }

                if (scores <= 0)
                {
                    return -1;
                }
                else
                {
                    ViewBag.swpSection = swpSection;
                    return (double)answered / scores;
                }
            }

            if (sectionID == 6)
            {
                int tscoreID = db.lkpswatscorevarslus.First(e => e.VarName == "toiletsAllSCORE" && e.SectionID == sectionID).ID;
                if (tscoreID > 0)
                {
                    double? score = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == tscoreID).Value;
                    if (score == 1)
                    {
                        scores = scores - 13;
                    }
                    else
                    {
                        int odNonescoreID = db.lkpswatscorevarslus.First(e => e.VarName == "ODnoneSCORE" && e.SectionID == sectionID).ID;
                        if (odNonescoreID > 0)
                        {
                            double? odNonescore = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == odNonescoreID).Value;
                            if (odNonescore == 1)
                            {
                                scores = scores - 5;
                            }
                            else
                            {
                                int hhtoiletscoreID = db.lkpswatscorevarslus.First(e => e.VarName == "hhNoToiletSCORE" && e.SectionID == sectionID).ID;
                                if (hhtoiletscoreID > 0)
                                {
                                    double? hhNoToiletscore = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == hhtoiletscoreID).Value;
                                    if (hhNoToiletscore <= 0)
                                    {
                                        scores = scores - 4;
                                    }
                                    else
                                    {
                                        int ODPercentscoreID = db.lkpswatscorevarslus.First(e => e.VarName == "ODPercentSCORE" && e.SectionID == sectionID).ID;
                                        if (ODPercentscoreID > 0)
                                        {
                                            double? odpercentscore = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == ODPercentscoreID).Value;
                                            if (odNonescore <= 1)
                                            {
                                                score = scores - 3;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                int sanTypeID = db.lkpswatscorevarslus.First(e => e.VarName == "sanTypeVAL" && e.SectionID == sectionID).ID;
                if (sanTypeID > 0)
                {
                    double? sanTypeVal = db.tblswatscores.First(e => e.VariableID == sanTypeID && e.SurveyID == id).Value;
                    if(sanTypeVal == null)
                    {
                        scores = scores - 37;
                    }
                    else
                    {
                        var sfpoint = db.tblswatsfpoints.First(e => e.SurveyID == id);
                        bool isPuborCom = sfpoint.sanUsePub || sfpoint.SanUseCom;
                        bool isInd = sfpoint.sanUseInd;
                        if (sanTypeVal == 1)
                        {
                            scores = scores - 27;
                            if (!isPuborCom)
                            {
                                scores = scores - 5;
                            }
                            else
                            {
                                int feeChargedID = db.lkpswatscorevarslus.First(e => e.VarName == "centralfeesChargedSCORE" && e.SectionID == sectionID).ID;
                                if (feeChargedID > 0)
                                {
                                    double? feechargedscore = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == feeChargedID).Value;
                                    if (feechargedscore != 1)
                                    {
                                        scores = scores - 2;
                                    }
                                }
                            }

                            if (!isInd)
                            {
                                scores--;
                            }
                        }

                        if (sanTypeVal == 2)
                        {
                            scores = scores - 22;
                            if (!isPuborCom)
                            {
                                scores = scores - 5;
                            }
                            else
                            {
                                int feeChargedID = db.lkpswatscorevarslus.First(e => e.VarName == "septicfeesChargedSCORE" && e.SectionID == sectionID).ID;
                                if (feeChargedID > 0)
                                {
                                    double? feechargedscore = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == feeChargedID).Value;
                                    if (feechargedscore != 1)
                                    {
                                        scores = scores - 2;
                                    }
                                }
                            }

                            if (!isInd)
                            {
                                scores--;
                            }
                        }

                        if (sanTypeVal == 3)
                        {
                            scores = scores - 25;
                            if (!isPuborCom)
                            {
                                scores = scores - 5;
                            }
                            else
                            {
                                int feeChargedID = db.lkpswatscorevarslus.First(e => e.VarName == "latrinefeesChargedSCORE" && e.SectionID == sectionID).ID;
                                if (feeChargedID > 0)
                                {
                                    double? feechargedscore = db.tblswatscores.First(e => e.SurveyID == id && e.VariableID == feeChargedID).Value;
                                    if (feechargedscore != 1)
                                    {
                                        scores = scores - 2;
                                    }
                                }
                            }

                            if (!isInd)
                            {
                                scores--;
                            }
                        }

                    }
                }

                if (scores <= 0)
                {
                    return 0;
                }
                else
                {
                    return (double)answered / scores;
                }
            }

            return 0;
        }

        private double getWPProgress(long wpid)
        {
            int answered = db.tblswatwpscores.Where(e => e.wpID == wpid && e.Value != null).Count();
            int scores = db.tblswatwpscores.Where(e => e.wpID == wpid).Count();
            var wp = db.tblswatwpoverviews.Find(wpid);

            if ((bool)wp.wpaUseDom)
            {
                if (wp.wpaType == "piped" || wp.wpaType == "bottled")
                {
                    scores = scores - 11;
                }
                else
                {
                    int scoreID = db.lkpswatwpscorelus.First(e => e.ScoreName == "domcollectDangerSCORE").ID;
                    double? scoreValue = db.tblswatwpscores.First(e => e.wpID == wpid && e.ScoreID == scoreID).Value;
                    if (scoreValue == null || scoreValue <= 0)
                    {
                        scores = scores - 1;
                    }
                    scores = scores - 2;
                }
            }
            else
            {
                scores = scores - 14;
            }

            if ((bool)wp.wpaUseInst)
            {
                if (wp.wpaType == "piped" || wp.wpaType == "bottled")
                {
                    scores = scores - 5;
                }
                else
                {
                    int scoreID = db.lkpswatwpscorelus.First(e => e.ScoreName == "incollectDangerSCORE").ID;
                    double? scoreValue = db.tblswatwpscores.First(e => e.wpID == wpid && e.ScoreID == scoreID).Value;
                    if (scoreValue == null || scoreValue <= 0)
                    {
                        scores = scores - 1;
                    }
                }
            }
            else
            {
                scores = scores - 9;
            }

            if (!(bool)wp.wpaUseEc)
            {
                scores = scores - 2;
            }

            int wpFreqID = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaInterruptionFreqSCORE").ID;
            double? wpFreqScore = db.tblswatwpscores.First(e => e.wpID == wpid && e.ScoreID == wpFreqID).Value;
            if (wpFreqScore == null || wpFreqScore == 1)
            {
                scores = scores - 1;
            }

            int qualTreatedID = db.lkpswatwpscorelus.First(e => e.ScoreName == "qualTreatedSCORE").ID;
            double? qualTreatedScore = db.tblswatwpscores.First(e => e.wpID == wpid && e.ScoreID == qualTreatedID).Value;
            if (qualTreatedScore != 0)
            {
                scores = scores - 1;
            }

            int userTreatedID = db.lkpswatwpscorelus.First(e => e.ScoreName == "userTreatedSCORE").ID;
            double? userTreatedScore = db.tblswatwpscores.First(e => e.wpID == wpid && e.ScoreID == userTreatedID).Value;
            if (userTreatedScore == null || userTreatedScore == 0)
            {
                scores = scores - 1;
            }

            if (wp.wpaType != "surface")
            {
                scores = scores - 3;
            }

            if (wp.wpaType != "spring")
            {
                scores = scores - 8;
            }

            if (wp.wpaType != "borehole")
            {
                scores = scores - 6;
            }

            if (wp.wpaType != "well")
            {
                scores = scores - 5;
            }

            if (wp.wpaType != "rain")
            {
                scores = scores - 8;
            }

            if (wp.wpaType != "piped")
            {
                scores = scores - 15;
            }

            return (double)answered / scores;
        }

        public ActionResult _PieChart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return HttpNotFound();
            }

            ChartsModel piecharts = new ChartsModel();
            piecharts.SectionChartList = new List<Highcharts>();
            List<string> secDescription = new List<string>();
            List<int> ctrlList = new List<int>();
            var sections = db.lkpswatsectionlus.ToList();
            foreach (var item in sections)
            {
                if (item.ID == 1)
                {
                    ctrlList.Add(tblswatsurvey.LocationID);
                }
                if (item.ID == 2)
                {
                    var listItem = db.tblswatwaprecipitations.Where(e => e.SurveyID == id).ToList();
                    if (listItem.Any())
                    {
                        ctrlList.Add(listItem.First(e => e.SurveyID == id).ID);
                        
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    
                }
                if (item.ID == 3)
                {
                    ctrlList.Add(db.tblswatccedus.First(e => e.SurveyID == id).ID);
                }

                double answered = getSectionProgress((int)id, item.ID) * 100;
                if (item.ID == 4)
                {
                    if (answered < 0)
                    {
                        ViewBag.swp = false;
                        continue;
                    }
                    ViewBag.swp = true;
                    if (ViewBag.swpSection == 1)
                    {
                        ctrlList.Add(db.tblswatswpls.First(e => e.SurveyID == id).ID);
                    }
                    if (ViewBag.swpSection == 2)
                    {
                        ctrlList.Add(db.tblswatswpags.First(e => e.SurveyID == id).ID);
                    }
                    if (ViewBag.swpSection == 3)
                    {
                        ctrlList.Add(db.tblswatswpdevs.First(e => e.SurveyID == id).ID);
                    }
                }
                if (item.ID == 5)
                {
                    ctrlList.Add(db.tblswathppcoms.First(e => e.SurveyID == id).ID);
                }
                if (item.ID == 6)
                {
                    ctrlList.Add(db.tblswatsfsanitations.First(e => e.SurveyID == id).ID);
                }
                double unanswered = 100 - answered;

                secDescription.Add(item.Description);
                string chartName = "chart" + item.ID.ToString();
                string serieName = item.Description + " Progress";
                Highcharts chart = new Highcharts(chartName)
                    .InitChart(new Chart
                    {
                        PlotShadow = false,
                        Height = 180,
                        Width = 360,
                    })
                .SetTitle(new Title { 
                    Text = item.Description, 
                    Style = "fontSize: '11px'"
                })
                .SetCredits(new Credits { Enabled = false })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Color = ColorTranslator.FromHtml("#000000"),
                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            Style = "fontSize: '10px'",
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = serieName,
                    Data = new Data(new object[]
                            {
                                new object[] { "Answered", answered },
                                new Point
                                    {
                                        Name = "No Answer",
                                        Y = unanswered,
                                        Sliced = true,
                                        Selected = true, 
                                        Color = ColorTranslator.FromHtml("#FFFF00")
                                    }
                            })
                });
                piecharts.SectionChartList.Add(chart);
            }

            var wps = db.tblswatwpoverviews.Where(e => e.SurveyID == id).OrderBy(e => e.ID).ToList();
            if (wps.Any())
            {
                piecharts.WPChartList = new List<Highcharts>();
                List<string> wpName = new List<string>();
                List<long> wpID = new List<long>();
                foreach (var wp in wps)
                {
                    wpName.Add(wp.wpname);
                    wpID.Add(wp.ID);
                    double answered = getWPProgress(wp.ID)*100;
                    double unanswered = 100 - answered;

                    string chartName = "wpchart" + wp.ID.ToString();
                    string serieName = wp.wpname + " Progress";
                    Highcharts wpchart = new Highcharts(chartName)
                    .InitChart(new Chart
                    {
                        PlotShadow = false,
                        Height = 180,
                        Width = 360,
                    })
                .SetTitle(new Title
                {
                    Text = wp.wpname,
                    Style = "fontSize: '11px'"
                })
                .SetCredits(new Credits { Enabled = false })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Color = ColorTranslator.FromHtml("#000000"),
                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            Style = "fontSize: '10px'",
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = serieName,
                    Data = new Data(new object[]
                            {
                                new object[] { "Answered", answered },
                                new Point
                                    {
                                        Name = "No Answer",
                                        Y = unanswered,
                                        Sliced = true,
                                        Selected = true, 
                                        Color = ColorTranslator.FromHtml("#FFFF00")
                                    }
                            })
                });
                    piecharts.WPChartList.Add(wpchart);

                }
                ViewBag.wpName = wpName.ToArray();
                ViewBag.wpID = wpID.ToArray();
            }

            //Highcharts g2 = new Highcharts("chartwp1");

            //piecharts.WPChartList.Add(g2);
            //piecharts.WPChartList.Add(g4);
            ViewBag.SurveyID = id;
            ViewBag.uid = tblswatsurvey.UserID;
            ViewBag.secDescription = secDescription.ToArray();
            ViewBag.ctrlList = ctrlList.ToArray();

            return PartialView(piecharts);
        }

        public ActionResult Progress(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Details", "User", new { id = 191 });
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return RedirectToAction("Details", "User", new { id = 191 });
            }
            if (tblswatsurvey.tblswatbackgroundinfoes.Count == 0 || tblswatsurvey.tblswatccedus.Count == 0)
            {
                return RedirectToAction("Details", "User", new { id = 191 });
            }


           
            return View(tblswatsurvey);
        }

        // GET: /Survey/WaterPoints/5
        public ActionResult WaterPoints(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return HttpNotFound();
            }


            return View(tblswatsurvey);
        }

        // GET: /Survey/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
        //    if (tblswatsurvey == null)
        //    {
        //        return HttpNotFound();
        //    }


        //    return View(tblswatsurvey);
        //}

        // GET: /Survey/Create
        public ActionResult Create(int UserID, int LocationID, string submitBtn)
        {
            // Create a survey record
            tblswatsurvey tblswatsurvey = new tblswatsurvey();
            tblswatsurvey.LocationID = LocationID;
            tblswatsurvey.UserID = UserID;
            tblswatsurvey.StartTime = DateTime.Now;
            tblswatsurvey.Status = 1;
            
            if (ModelState.IsValid)
            {
                // Add and Save the survey record to database
                db.tblswatsurveys.Add(tblswatsurvey);
                db.SaveChanges();
                // Get the id of tblswatsurvey record (new record)
                var newSurveyID = tblswatsurvey.ID;
                var scorevars = db.lkpswatscorevarslus.ToList();
                foreach (var scorevar in scorevars)
                {
                    tblswatscore tblswatscore = new tblswatscore();
                    tblswatscore.SurveyID = newSurveyID;
                    tblswatscore.VariableID = scorevar.ID;

                    // For reference copy the score name from lkpswatscorevarslu table to tblSWATScore table varname field
                    tblswatscore.VarName = scorevar.VarName;

                    if (ModelState.IsValid)
                    {
                        db.tblswatscores.Add(tblswatscore);
                        db.SaveChanges();
                    }
                }
                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "Background", new { SurveyID = newSurveyID });
                }
                return RedirectToAction("Edit", "Location", new { id = LocationID, uid = UserID, SurveyID = newSurveyID });
            }

            //ViewBag.LocationID = new SelectList(db.tblSWATLocations, "ID", "name");
            //ViewBag.UserID = new SelectList(db.Userids.Where(user => user.type == 0), "Userid1", "Username");
            return View();
        }

        // POST: /Survey/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,UserID,Status,StartTime,EndTime,LocationID")] tblswatsurvey tblswatsurvey)
        {
            if (ModelState.IsValid)
            {
                db.tblswatsurveys.Add(tblswatsurvey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.LocationID = new SelectList(db.tblSWATLocations, "ID", "name", tblswatsurvey.LocationID);
            //ViewBag.UserID = new SelectList(db.Userids, "Userid1", "Username", tblswatsurvey.UserID);
            return View(tblswatsurvey);
        }

        // GET: /Survey/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return HttpNotFound();
            }
            //ViewBag.LocationID = new SelectList(db.tblSWATLocations, "ID", "name", tblswatsurvey.LocationID);
            //ViewBag.UserID = new SelectList(db.Userids, "Userid1", "Username", tblswatsurvey.UserID);
            return View(tblswatsurvey);
        }

        // POST: /Survey/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,UserID,Status,StartTime,EndTime,LocationID")] tblswatsurvey tblswatsurvey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatsurvey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.LocationID = new SelectList(db.tblSWATLocations, "ID", "name", tblswatsurvey.LocationID);
           // ViewBag.UserID = new SelectList(db.Userids, "Userid1", "Username", tblswatsurvey.UserID);
            return View(tblswatsurvey);
        }

        // GET: /Survey/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            if (tblswatsurvey == null)
            {
                return HttpNotFound();
            }
            return View(tblswatsurvey);
        }

        private void DeleteRelatedRecords(int id)
        {
            var waterpts = db.tblswatwpoverviews.Where(e => e.SurveyID == id);
            foreach (tblswatwpoverview w in waterpts)
            {
                var wpCtrl = new WaterPointController();
                wpCtrl.DeleteRelatedForms(w.ID);
                db.tblswatwpoverviews.Remove(w);
            }

            var centrals = db.tblswatsfcentrals.Where(e => e.SurveyID == id);
            foreach (tblswatsfcentral c in centrals)
            {
                db.tblswatsfcentrals.Remove(c);
            }

            var septics = db.tblswatsfseptics.Where(e => e.SurveyID == id);
            foreach (tblswatsfseptic s in septics)
            {
                db.tblswatsfseptics.Remove(s);
            }

            var lats = db.tblswatsflats.Where(e => e.SurveyID == id);
            foreach (tblswatsflat l in lats)
            {
                db.tblswatsflats.Remove(l);
            }

            var tblswatsfpoint = db.tblswatsfpoints.Where(e => e.SurveyID == id);
            foreach (tblswatsfpoint item in tblswatsfpoint)
            {
                db.tblswatsfpoints.Remove(item);
            }

            var tblswatsfod = db.tblswatsfods.Where(e => e.SurveyID == id);
            foreach (tblswatsfod item in tblswatsfod)
            {
                db.tblswatsfods.Remove(item);
            }

            var tblswatsfsanitation = db.tblswatsfsanitations.Where(e => e.SurveyID == id);
            foreach (tblswatsfsanitation item in tblswatsfsanitation)
            {
                db.tblswatsfsanitations.Remove(item);
            }

            var tblswathppkhp = db.tblswathppkhps.Where(e => e.SurveyID == id);
            foreach (tblswathppkhp item in tblswathppkhp)
            {
                db.tblswathppkhps.Remove(item);
            }

            var tblswathppcom = db.tblswathppcoms.Where(e => e.SurveyID == id);
            foreach (tblswathppcom item in tblswathppcom)
            {
                db.tblswathppcoms.Remove(item);
            }

            var tblswatswpdev = db.tblswatswpdevs.Where(e => e.SurveyID == id);
            foreach (tblswatswpdev item in tblswatswpdev)
            {
                db.tblswatswpdevs.Remove(item);
            }

            var tblswatswpag = db.tblswatswpags.Where(e => e.SurveyID == id);
            foreach (tblswatswpag item in tblswatswpag)
            {
                db.tblswatswpags.Remove(item);
            }

            var tblswatswpls = db.tblswatswpls.Where(e => e.SurveyID == id);
            foreach (tblswatswpl item in tblswatswpls)
            {
                db.tblswatswpls.Remove(item);
            }

            var tblswatccwatermanagement = db.tblswatccwatermanagements.Where(e => e.SurveyID == id);
            foreach (tblswatccwatermanagement item in tblswatccwatermanagement)
            {
                db.tblswatccwatermanagements.Remove(item);
            }

            var tblswatccexternal = db.tblswatccexternalsupports.Where(e => e.SurveyID == id);
            foreach (tblswatccexternalsupport item in tblswatccexternal)
            {
                db.tblswatccexternalsupports.Remove(item);
            }

            var tblswatcccom = db.tblswatcccoms.Where(e => e.SurveyID == id);
            foreach (tblswatcccom item in tblswatcccom)
            {
                db.tblswatcccoms.Remove(item);
            }

            var tblswatccsocial = db.tblswatccsocials.Where(e => e.SurveyID == id);
            foreach (tblswatccsocial item in tblswatccsocial)
            {
                db.tblswatccsocials.Remove(item);
            }

            var tblswatccgender = db.tblswatccgenders.Where(e => e.SurveyID == id);
            foreach (tblswatccgender item in tblswatccgender)
            {
                db.tblswatccgenders.Remove(item);
            }

            var tblswatccfinancial = db.tblswatccfinancials.Where(e => e.SurveyID == id);
            foreach(tblswatccfinancial item in tblswatccfinancial)
            {
               db.tblswatccfinancials.Remove(item);
            }

            var tblswatccindig = db.tblswatccindigs.Where(e => e.SurveyID == id);
            foreach (tblswatccindig item in tblswatccindig)
            {
                db.tblswatccindigs.Remove(item);
            }

            var tblswatccschool = db.tblswatccschools.Where(e => e.SurveyID == id);
            foreach (tblswatccschool item in tblswatccschool)
            {
                db.tblswatccschools.Remove(item);
            }

            var tblswatcctrain = db.tblswatcctrains.Where(e => e.SurveyID == id);
            foreach (tblswatcctrain item in tblswatcctrain)
            {
                db.tblswatcctrains.Remove(item);
            }

            var tblswatcceduc = db.tblswatccedus.Where(e => e.SurveyID == id);
            foreach (tblswatccedu item in tblswatcceduc)
            {
                db.tblswatccedus.Remove(item);
            }

            var tblswatwagroundwater = db.tblswatwagroundwaters.Where(e => e.SurveyID == id);
            foreach (tblswatwagroundwater item in tblswatwagroundwater)
            {
                db.tblswatwagroundwaters.Remove(item);
            }

            var tblswatwasurfacewater = db.tblswatwasurfacewaters.Where(e => e.SurveyID == id);
            foreach (tblswatwasurfacewater item in tblswatwasurfacewater)
            {
                db.tblswatwasurfacewaters.Remove(item);
            }

            var tblswatwariskprep = db.tblswatwariskpreps.Where(e => e.SurveyID == id);
            foreach (tblswatwariskprep item in tblswatwariskprep)
            {
                db.tblswatwariskpreps.Remove(item);
            }

            var tblswatwaextremeevent = db.tblswatwaextremeevents.Where(e => e.SurveyID == id);
            foreach (tblswatwaextremeevent item in tblswatwaextremeevent)
            {
                db.tblswatwaextremeevents.Remove(item);
            }

            var tblswatwaclimatechange = db.tblswatwaclimatechanges.Where(e => e.SurveyID == id);
            foreach (tblswatwaclimatechange item in tblswatwaclimatechange)
            {
                db.tblswatwaclimatechanges.Remove(item);
            }

            var tblswatwaannualprecip = db.tblswatwaannualprecips.Where(e => e.SurveyID == id);
            foreach (tblswatwaannualprecip item in tblswatwaannualprecip)
            {
                db.tblswatwaannualprecips.Remove(item);
            }

            var tblswatwamonthlyquantity = db.tblswatwamonthlyquantities.Where(e => e.SurveyID == id);
            foreach (tblswatwamonthlyquantity item in tblswatwamonthlyquantity)
            {
                db.tblswatwamonthlyquantities.Remove(item);
            }

            var tblswatbackgroundinfos = db.tblswatbackgroundinfoes.Where(e => e.SurveyID == id);
            foreach(tblswatbackgroundinfo item in tblswatbackgroundinfos)
            {
                db.tblswatbackgroundinfoes.Remove(item);
            }

            var tblswatwaprecipitations = db.tblswatwaprecipitations.Where(e => e.SurveyID == id);
            foreach(tblswatwaprecipitation item in tblswatwaprecipitations)
            {
                db.tblswatwaprecipitations.Remove(item);
            }

            var tblswatscores = db.tblswatscores.Where(e => e.SurveyID == id);
            foreach (tblswatscore item in tblswatscores)
            {
                db.tblswatscores.Remove(item);
            }

            db.SaveChanges();
        }
        // POST: /Survey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            
            
            tblswatsurvey tblswatsurvey = db.tblswatsurveys.Find(id);
            db.tblswatsurveys.Remove(tblswatsurvey);
            DeleteRelatedRecords(id);
            db.SaveChanges();
            
            //return RedirectToAction("Index");
            return RedirectToAction("Details", "User", new { id=tblswatsurvey.UserID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

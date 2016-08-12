using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

using YSQ.core.Historical;

namespace TheHastyFinn
{
    public class XFactorGraphModel
    {
        public PlotModel TickerModel { get; private set; }
        public PlotModel XFactorModel { get; private set; }

        public List<DateTime> Dates { get; set; }
        public List<int> PeriodsToPlot { get; set; }

        public XFactor XF { get; set; }

        public XFactorGraphModel()
        {
            this.TickerModel = new PlotModel();
            this.XFactorModel = new PlotModel();

            this.PeriodsToPlot = new List<int>();
        }

        public void LoadData(XFactor xf)
        {
            XF = xf;
            UpdateModel();
        }

        public void UpdateModel()
        {
            this.TickerModel.Series.Clear();
            this.TickerModel.Axes.Clear();

            this.XFactorModel.Series.Clear();
            this.XFactorModel.Axes.Clear();

            /*
             * basic ticker
             */
            this.TickerModel.Title = XF.Ticker;
            this.TickerModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd",
                IntervalType = DateTimeIntervalType.Days,
            });
            this.TickerModel.Series.Add(GenPoints());

            /*
             * xfactor ticker
             */
            this.XFactorModel.Title = String.Format("XFactor");
            this.XFactorModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd",
                IntervalType = DateTimeIntervalType.Days,
            });
            foreach (int p in PeriodsToPlot)
            {
                this.XFactorModel.Series.Add(GenPointsXFVelocity(p));
                this.XFactorModel.Series.Add(GenPointsXFGravity(p));
            }

        }

        private LineSeries GenPointsXFVelocity(int period)
        {
            OxyPlot.Series.LineSeries series = new LineSeries();
            
            List<int> periods = XF.Periods;
            List<double> list = XF.PeriodVelocityData[period];
            List<HistoricalPrice> quotes = XF.Quotes;

            for(int i = 0; i < list.Count(); i++)
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(quotes[i].Date), list[i]));
            }

            return series;
        }
        private LineSeries GenPointsXFGravity(int period)
        {
            OxyPlot.Series.LineSeries series = new LineSeries();

            List<int> periods = XF.Periods;
            List<double> list = XF.PeriodGravityData[period];
            List<HistoricalPrice> quotes = XF.Quotes;

            for (int i = 0; i < list.Count(); i++)
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(quotes[i].Date), list[i]));
            }

            return series;
        }

        private LineSeries GenPoints()
        {
            OxyPlot.Series.LineSeries series = new LineSeries();

            foreach (var price in XF.Quotes)
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(price.Date), LinearAxis.ToDouble(price.Price)));
            }

            return series;
        }
        private LineSeries GenPointsTest()
        {
            OxyPlot.Series.LineSeries series = new LineSeries();
            Random rd = new Random();
            for (int i = 1; i < 15; i++)
            {
                series.Points.Add(new DataPoint(i, rd.Next()));
            }
            return series;
        }

    }
}

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

        public XFactor XF { get; set; }

        public XFactorGraphModel()
        {
            this.TickerModel = new PlotModel();
            this.XFactorModel = new PlotModel();
        }

        public void LoadData(XFactor xf)
        {
            XF = xf;
            UpdateModel();
        }

        public void UpdateModel()
        {
            /*
             * basic ticker
             */
            this.TickerModel.Title = XF.Ticker;
            this.TickerModel.Series.Add(GenPoints());
            this.TickerModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd",
                IntervalType = DateTimeIntervalType.Days,
            });

            /*
             * xfactor ticker
             */
            this.XFactorModel.Title = String.Format("XFactor");
            this.XFactorModel.Series.Add(GenPointsXFVelocity());
            this.XFactorModel.Series.Add(GenPointsXFGravity());
            this.XFactorModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd",
                IntervalType = DateTimeIntervalType.Days,
            });
        }

        private LineSeries GenPointsXFVelocity()
        {
            OxyPlot.Series.LineSeries series = new LineSeries();
            
            List<int> periods = XF.Periods;
            List<double> list = XF.PeriodVelocityData[periods[2]];
            List<HistoricalPrice> quotes = XF.Quotes;

            for(int i = 0; i < list.Count(); i++)
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(quotes[i].Date), list[i]));
            }

            return series;
        }
        private LineSeries GenPointsXFGravity()
        {
            OxyPlot.Series.LineSeries series = new LineSeries();

            List<int> periods = XF.Periods;
            List<double> list = XF.PeriodGravityData[periods[2]];
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

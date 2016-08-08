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
        private string _ticker;

        public PlotModel TickerModel { get; private set; }
        public PlotModel XFactorModel { get; private set; }

        private List<DateTime> Dates { get; }
        private IEnumerable<HistoricalPrice> Quotes { get; set; }

        public XFactorGraphModel()
        {
            this.TickerModel = new PlotModel();
            this.XFactorModel = new PlotModel();

            _ticker = "";
        }

        private void UpdateModel()
        {
            /*
             * basic ticker
             */
            this.TickerModel.Title = StockTicker;
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
            this.XFactorModel.Series.Add(GenPoints());
            this.XFactorModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd",
                IntervalType = DateTimeIntervalType.Days,
            });
        }

        public string StockTicker
        {
            get { return _ticker; }
            set
            {
                _ticker = value;
                GetQuotes(_ticker); // update quotes!
                UpdateModel();
            }
        }
        private void GetQuotes(string ticker)
        {
            StockQuotes sq = new StockQuotes(ticker);
            Quotes = sq.HistPrices();
        }

        /*    THIS IS RECURSIVE... NOT GOOD
        private LineSeries GenPointsXFVelocity()
        {
            OxyPlot.Series.LineSeries series = new LineSeries();

            XFactor xf = new XFactor(StockTicker);
            List<int> periods = xf.Periods;
            List<decimal> list = xf.PeriodVelocityData[periods[0]];
            for(int i = 0; i < list.Count(); i++)
            {
                series.Points.Add(new DataPoint(i, Convert.ToDouble(list[i])));
            }
            return series;
        }
        */

        private LineSeries GenPoints()
        {
            OxyPlot.Series.LineSeries series = new LineSeries();

            foreach (var price in Quotes)
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

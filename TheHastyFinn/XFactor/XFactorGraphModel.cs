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
        IEnumerable<HistoricalPrice> _quotes;
        string _ticker;

        public PlotModel MyModel { get; private set; }

        public XFactorGraphModel()
        {
            this.MyModel = new PlotModel();
            
            _ticker = "";
        }
        
        private void UpdateModel()
        {
            this.MyModel.Title = StockTicker;
            this.MyModel.Series.Add(GenPoints(StockTicker));
            this.MyModel.Axes.Add(new DateTimeAxis
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
                UpdateModel();                
            }
        }

        private LineSeries GenPoints(string ticker)
        {
            StockQuotes sq = new StockQuotes(ticker);
            _quotes = sq.HistPrices();

            OxyPlot.Series.LineSeries series = new LineSeries();

            foreach (var price in _quotes)
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(price.Date), LinearAxis.ToDouble(price.Price)));
            }

            return series;
        }

    }
}

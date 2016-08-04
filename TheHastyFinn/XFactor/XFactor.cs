using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YSQ.core.Historical;
using OxyPlot;
using OxyPlot.Axes;

namespace TheHastyFinn
{
    public class XFactor
    {
        IEnumerable<HistoricalPrice> _quotes;

        public XFactor()
        {
            this.Points = new List<DataPoint>();
            this.PlotTitle = "";
        }
        
        public void GenPoints(string ticker)
        {
            StockQuotes sq = new StockQuotes(ticker);
            _quotes = sq.HistPrices();

            this.Points = new List<DataPoint>();
            foreach(var price in _quotes)
            {
                this.Points.Add(new DataPoint(DateTimeAxis.ToDouble(price.Date), LinearAxis.ToDouble(price.Price)));
            }
            
            this.PlotTitle = "";
        }

        public IList<DataPoint> Points { get; private set; }
        public string PlotTitle { get; private set; }

    }
}

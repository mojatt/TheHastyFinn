using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSQ.core.Historical;

namespace TheHastyFinn
{
    class StockQuotes
    {
        // https://github.com/jchristian/yahoo_stock_quotes

        public StockQuotes(string ticker)
        {
            Ticker = ticker;

            Start = DateTime.Now;
            Start = Start.AddYears(-1); // default a year of history

            End = DateTime.Now;
        }

        public IEnumerable<HistoricalPrice> HistPrices()
        {
            var hps = new HistoricalPriceService();

            IEnumerable<HistoricalPrice> histprices = hps.Get(Ticker, Start, End, Period.Daily);

            return histprices;
        }

        public string Ticker { get; private set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}

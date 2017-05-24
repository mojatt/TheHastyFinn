using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooFinanceAPI;

namespace TheHastyFinn
{
    class StockQuotes
    {
        // https://github.com/dennislwy/YahooFinanceAPI.git

        public StockQuotes(string ticker)
        {
            Ticker = ticker;

            Start = DateTime.Now;
            Start = Start.AddYears(-2); // default a year of history

            End = DateTime.Now;
        }

        public List<HistoryPrice> HistPrices()
        {
            // first get valid token from yahoo finance
            while(string.IsNullOrEmpty(Token.Cookie) | string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> histprices = Historical.Get(Ticker, Start, End);
            //histprices.Reverse();

            return histprices;
        }

        public string Ticker { get; private set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}

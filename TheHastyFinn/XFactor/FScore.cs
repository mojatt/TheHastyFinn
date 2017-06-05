using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TheHastyFinn
{
    class FScore
    {
        private string _url = @"https://docs.google.com/spreadsheets/d/1GMU1-wmxpiruXEZd8gqYh8E0RZxw6pXw6Z5Pl7svA_4/pub?single=true&gid=15&output=csv";

        private List<FScoreStock> _list;

        public FScore()
        {
            _list = new List<FScoreStock>();
            
            ParseCSV(GetCSV());
        }

        public List<FScoreStock> StockList { get { return _list; } }

        private string GetCSV()
        {
            WebClient wc = new WebClient();
            string tmpfilename = System.IO.Path.GetTempFileName();
            wc.DownloadFile(_url, tmpfilename);
            return tmpfilename;
        }

        private void ParseCSV(string input)
        {
            using (TextReader tr = File.OpenText(input))
            {
                var csv = new CsvHelper.CsvReader(tr);
                csv.Configuration.IgnoreHeaderWhiteSpace = true;
                
                while (csv.Read())
                {
                    FScoreStock fss = new FScoreStock();
                    fss.Ticker = csv.GetField<string>(0);
                    fss.Name = csv.GetField<string>(1);
                    fss.Sector = csv.GetField<string>(2);
                    fss.Industry = csv.GetField<string>(3);
                    fss.LastPrice = csv.GetField<string>(6);

                    _list.Add(fss);
                }
            }
        }
        
    }

    public class FScoreStock
    {
        // header
        // Ticker,Name,Sector,Industry,Country,Earnings Date, Last, Market Cap($M),Volume, % off 52Wk High,% off 52Wk Low, P/E, Forward P/E , PEG , P/S , P/B , P/Cash ,P/FCF,Yield, Payout Ratio, EPS (ttm), EPS growth this year, EPS growth nxt year, EPS growth past 5 yrs, EPS growth nxt 5 yrs, Sales growth past 5 yrs, ROA, ROE, ROI, Current Ratio, LT Debt / Equity, Total Debt / Equity, Gross Mgn, Operating Mgn, Profit Mgn, Insider Ownership, Insider Transactions, Institutional Ownership, Short Ratio, Analyst Recom

        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string LastPrice { get; set; }
    }
}

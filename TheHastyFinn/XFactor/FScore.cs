using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHastyFinn
{
    class FScore
    {
        private string _url = @"https://docs.google.com/spreadsheets/d/1GMU1-wmxpiruXEZd8gqYh8E0RZxw6pXw6Z5Pl7svA_4/pub?single=true&gid=15&output=csv";

        public FScore()
        {
            GetCSV();
        }

        private void GetCSV()
        {
            WebCSVHandler wc = new WebCSVHandler(new System.Net.CookieContainer());
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
            wc.Headers.Add("DNT", "1");
            wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            wc.Headers.Add("Accept-Encoding", "deflate");
            wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");

            byte[] dt = wc.DownloadData(_url);
            var outputCSVdata = System.Text.Encoding.UTF8.GetString(dt ?? new byte[] { });

        }
    }
}

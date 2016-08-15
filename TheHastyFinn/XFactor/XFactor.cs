using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OxyPlot.Series;

using YSQ.core.Historical;

namespace TheHastyFinn
{
    public class XFactor
    {
        public XFactor(string ticker)
        {
            Ticker = ticker;
            
            StockQuotes sq = new StockQuotes(Ticker);
            Quotes = sq.HistPrices();

            Periods = new List<int>() { 25, 30, 50, 60, 75, 90, 100, 120 };

            PeriodVelocityData = new Dictionary<int, List<double>>();
            PeriodGravityData = new Dictionary<int, List<double>>();

            GenXFactor();
        }

        public string Ticker { get; private set; }
        public Dictionary<int, List<double>> PeriodVelocityData { get; set; }
        public Dictionary<int, List<double>> PeriodGravityData { get; set; }

        public List<HistoricalPrice> Quotes { get; set; }
        public List<int> Periods { get; set; }

        // calculate XFactor across many moving windows (25, 50, 100, 125, 150, etc).
        // maybe even have a spread? what do the trends look like for diff intervals and ranges?

        private void GenXFactor()
        {
            XFactorAlgoData data = new XFactorAlgoData();
            
            foreach (int period in Periods)
            {
                List<double> velocity = new List<double>();
                List<double> gravity = new List<double>();

                data.ResetValues();

                for(int i = 1; i < Quotes.Count(); i++)
                {
                    int range = 1;
                    int start = i;

                    /*
                    if(i < period)
                    {
                        start = 1;
                        range = i;
                    }
                    else
                    {
                        start = i - period;
                        range = period;
                    }
                    */
                    
                    
                    if (period >= Quotes.Count() - i) // tail end of segments
                    {
                        range = period;
                        start = i - period;
                    }
                    else if (i > period) // middle of segment
                    {
                        range = period;
                        start = i - period;
                    }
                    else // beginning of segment quotes
                    {
                        range = i;
                        start = 1;
                    }
                    

                    // TODO 
                    // TODO --- two ways to calculate the window . start full or end full
                    // TODO --- current way is to end full
                    // want the most recent data to be accurate so the end must be full & good
                    // TODO 
                    List<HistoricalPrice> segment = Quotes.GetRange(start, range);

                    // the hi/lo can expire! must account for that by reset if the previous 
                    // pHighHi is outside of the segment range

                    // find the highest high for period
                    if (data.pHighHi < (i - period))
                    {
                        data.HighestHi = 0;
                    }
                    SearchData fpmax = FindPeriodMax(segment);
                    if (fpmax.Price > data.HighestHi)
                    {
                        data.HighestHi = fpmax.Price;
                        data.pHighHi = fpmax.Index + (i - period);
                    }

                    // find lowest low for period
                    if (data.pLowLo < (i - period))
                    {
                        data.LowestLo = double.MaxValue;
                    }
                    SearchData fpmin = FindPeriodMin(segment);
                    if(fpmin.Price < data.LowestLo)
                    {
                        data.LowestLo = fpmin.Price;
                        data.pLowLo = fpmin.Index + (i - period);
                    }

                    // calc num of periods since high/low
                    data.pSinceHighHi = i - data.pHighHi;
                    data.pSinceLowLo = i - data.pLowLo;

                    // calc velocity
                    double v = (((double)period - (double)data.pSinceHighHi) / (double)period) * 100;
                    velocity.Add(v);

                    // calc gravity
                    double g = (((double)period - (double)data.pSinceLowLo) / (double)period) * 100;
                    gravity.Add(g);
                }

                PeriodVelocityData.Add(period, velocity);
                PeriodGravityData.Add(period, gravity);
            }
        }

        private SearchData FindPeriodMax(List<HistoricalPrice> segment)
        {
            SearchData data = new SearchData();
            double max = 0;

            for(int i = 0; i < segment.Count(); i++)
            {
                if ((double)segment[i].Price > max)
                {
                    max = (double)segment[i].Price;
                    data.Price = max;
                    data.Index = i;
                }
            }
            return data;
        }

        private SearchData FindPeriodMin(List<HistoricalPrice> segment)
        {
            SearchData data = new SearchData();
            double min = double.MaxValue;

            for (int i = 0; i < segment.Count(); i++)
            {
                if((double)segment[i].Price < min)
                {
                    min = (double)segment[i].Price;
                    data.Price = min;
                    data.Index = i;
                }
            }
            return data;
        }

    }

    class SearchData
    {
        public double Price { get; set; }
        public int Index { get; set; }

        public SearchData() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace TheHastyFinn
{
    public class XFactor
    {
        public XFactor(string ticker)
        {
            Ticker = ticker;
            _graphmodel = new XFactorGraphModel();
            _graphmodel.StockTicker = ticker;
        }

        private XFactorGraphModel _graphmodel;

        public string Ticker { get; private set; }

        public XFactorGraphModel XFGraphModel
        {
            get { return _graphmodel; }
            private set { _graphmodel = value; }
        }

        // go and get quotes
        
        // calculate XFactor across many moving windows (25, 50, 100, 125, 150, etc).
        // maybe even have a spread? what do the trends look like for diff intervals and ranges?

    }
}

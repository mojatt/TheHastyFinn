using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHastyFinn
{
    class XFactorHandler
    {
        public XFactor xF { get; set; }
        public XFactorGraphModel xFGM { get; set; }

        public XFactorHandler(string ticker)
        {
            xF = new XFactor(ticker);

            xFGM = new XFactorGraphModel();
            xFGM.LoadData(ticker, xF.Quotes);
        }
    }
}

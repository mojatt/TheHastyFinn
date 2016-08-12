using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHastyFinn
{
    public class XFactorHandler
    {
        public XFactor xFactor { get; set; }
        public XFactorGraphModel xFactorGraphModel { get; set; }

        public XFactorHandler(string ticker)
        {
            xFactor = new XFactor(ticker);

            xFactorGraphModel = new XFactorGraphModel();
            xFactorGraphModel.LoadData(xFactor);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHastyFinn
{
    public class XFactorAlgoData
    {
        public decimal HighestHi { get; set; }
        public decimal LowestLo { get; set; }
        public int pSinceHighHi { get; set; }
        public int pSinceLowLo { get; set; }
        public int pHighHi { get; set; }
        public int pLowLo { get; set; }

        public XFactorAlgoData()
        {
            ResetValues();
        }

        public void ResetValues()
        {
            HighestHi = 0;
            LowestLo = decimal.MaxValue;
            pSinceHighHi = 0;
            pSinceLowLo = 0;
            pHighHi = 0;
            pLowLo = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Bar
{
    public class BarTransaction : IBarTransaction
    {
        public string SourceWalletAddress { get; set; }
        public string TargetWalletAddress { get; set; }
        public float TokenAmount { get; set; }
        public DateTime TxDate { get; set; }
    }


}

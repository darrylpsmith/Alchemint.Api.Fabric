using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Alchemint.Bar
{
    public class BarWallet : IBarWallet
    {
        public string OwnerId { get; set; }
        public DateTime CreationTime { get; set; }
        public string ReceiveAddress { get; set; }
        [UniqueKey]
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}

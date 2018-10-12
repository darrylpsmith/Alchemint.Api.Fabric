using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Alchemint.Bar
{

    public class BarToken : IBarToken
    {
        [UniqueKey]
        [PrimaryKey]
        public string Id { get; set; }
        public DateTime IssueTime { get; set; }
        public string OriginatorWalletAddress { get; set; }
        public string CurrentWallet { get; set; }
        public Int64 TokenType { get; set; }
    }


}

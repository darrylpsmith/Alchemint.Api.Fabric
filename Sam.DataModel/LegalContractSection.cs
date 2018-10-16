using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Sam.DataModel
{
    [GeneratedController("api/legalcontractsection")]
    public class LegalContractSection
    {
        public LegalContractSection()
        {
        }
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public int PositionInContract { get; set; }
        public string Content { get; set; }
    }
}

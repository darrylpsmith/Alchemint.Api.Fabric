using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Sam.DataModel
{
    public class LegalContractSection
    {
        public LegalContractSection()
        {
        }
        [PrimaryKey]
        public string Id { get; set; }
        [UniqueKey]
        public string Name { get; set; }
        public int PositionInContract { get; set; }
    }
}

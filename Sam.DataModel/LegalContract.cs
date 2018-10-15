using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Sam.DataModel
{

    public class LegalContract
    {
        public LegalContract()
        {
        }
        [PrimaryKey]
        public string Id { get; set; }
        [UniqueKey]
        public string Name { get; set; }
    }
}

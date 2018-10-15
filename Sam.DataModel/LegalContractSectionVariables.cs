using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Sam.DataModel
{
    public class LegalContractSectionVariables

    {
        public LegalContractSectionVariables()
        {
        }
        [PrimaryKey]
        public string Id { get; set; }
        [UniqueKey]
        public string LegalContractSectionId { get; set; }
        [UniqueKey]
        public string Name { get; set; }
        public string Description { get; set; }
        public string FormulaLogicJSON { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Sam.DataModel
{
    public class ContractPartyAuthority
    {
        public ContractPartyAuthority()
        {
        }
        [PrimaryKey]
        public string Id { get; set; }
        [UniqueKey]
        public string LegalContractId { get; set; }
        [UniqueKey]
        public string PartytAuthorityId { get; set; }
    }

}

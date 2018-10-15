using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Sam.DataModel
{
    public class PartyAuthority
    {
        [PrimaryKey]
        public string Id { get; set; }
        [UniqueKey]
        public string PartyId { get; set; }
        [UniqueKey]
        public string AuthorisedOnBehalfPartyId { get; set; }
        [UniqueKey]
        public bool Deleted { get; set; }
    }
}

using System;
using Alchemint.Core;

namespace Sam.DataModel
{
    public enum PartyType
    {
        Organisation,
        Company,
        Person
    }

    public class Party
    {
        public Party()
        {
        }

        [PrimaryKey]
        public string Id { get; set; }
        [UniqueKey]
        public string Name { get; set; }
        [UniqueKey]
        public string IdentificationNumber { get; set; }

        public int Type { get; set; }
        
    }
}

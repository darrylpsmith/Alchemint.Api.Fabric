using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Alchemint.Bar
{
    public class Patient
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        [UniqueKey]
        public string IdNumber { get; set; }
        public string Telephone { get; set; }

        public long Age { get; set; }

    }
}

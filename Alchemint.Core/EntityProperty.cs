using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public class EntityProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }
        public bool IsPartOfUniqueKey { get; set; }
        public bool IsPartOfPrimaryKey { get; set; }


    }
}

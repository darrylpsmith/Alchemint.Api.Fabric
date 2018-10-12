using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alchemint.Core.Fabric
{
    public class EntitySearchObject
    {
        public object TypedObject { get; set; }
        public List<string> PropertiesToSearch { get; set; }
    }

}

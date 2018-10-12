using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public class DatabaseTenant : IDatabaseTenant
    {
        public string Code { get ; set ; }
        public string Name { get ; set ; }
    }
}

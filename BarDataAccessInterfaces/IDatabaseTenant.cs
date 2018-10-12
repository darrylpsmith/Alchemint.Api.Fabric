using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public interface IDatabaseTenant
    {
        string Code { get; set; }
        string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public interface ISQLDMLStatementVariable
    {
        string Name { get; set; }
        object Value { get; set; }
    }
}

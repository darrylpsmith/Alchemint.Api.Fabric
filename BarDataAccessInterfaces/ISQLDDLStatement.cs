using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public interface ISQLDDLStatement
    {
        string PreparedStatement { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public interface ISQLDMLStatement
    {
        string PreparedStatement { get; set; }
        List<ISQLDMLStatementVariable> Variables { get; set; }
        DMLStatemtType StatemtType { get; set; }
        int ParameterCount { get; }
    }
}

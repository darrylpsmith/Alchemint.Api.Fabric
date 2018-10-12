using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{

    public class SQLDMLStatementVariable : ISQLDMLStatementVariable
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public int ParameterCount { get; }
    }

    public class SQLDDLStatement : ISQLDDLStatement
    {
        public string PreparedStatement { get; set; }
    }

    public class SQLDMLStatement : ISQLDMLStatement
    {
        public string PreparedStatement { get; set; }
        public List<ISQLDMLStatementVariable> Variables { get; set; }
        public DMLStatemtType StatemtType { get; set; }
        public int ParameterCount { 
            get {
                if (this.Variables == null)
                    return 0;
                else
                    return this.Variables.Count;
            }

        }
    }

}

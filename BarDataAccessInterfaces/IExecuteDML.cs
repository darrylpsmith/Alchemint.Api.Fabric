using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public interface IExecuteDML
    {
        object Execute(ISQLDMLStatement Statement);

        bool TableExists(string Name);
    }
}

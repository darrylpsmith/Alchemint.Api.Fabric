using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public interface IExecuteDDL
    {
        void Execute(ISQLDDLStatement Statement);
    }
}

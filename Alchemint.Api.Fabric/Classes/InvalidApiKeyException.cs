using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alchemint.Core.Fabric
{
    public class InvalidApiKeyException : Exception
    {
        public InvalidApiKeyException()
            : base("Invalid Api Key.")
        {
            
        }

    }
}

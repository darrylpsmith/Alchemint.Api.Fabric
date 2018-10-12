using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public class Database
    {

        public Database()
        {
            DatabaseTenant = new DatabaseTenant();
        }

        public DatabaseType Type { get; set; }
        public string ConnectionString { get; set; }
        public DatabaseTenant DatabaseTenant { get; set; }

    }
}

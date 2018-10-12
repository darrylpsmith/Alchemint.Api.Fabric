using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Alchemint.Core
{
    public class DatabaseCreate : IDatabaseCreate
    {
        readonly ICreateDatabase _dbCreateProvider;
        readonly IExecuteDDL _executeDDLProvider;

        public DatabaseCreate(ICreateDatabase CreateDatabaseProvider, IExecuteDDL ExecuteDDLProvider)
        {
            _dbCreateProvider = CreateDatabaseProvider;
            _executeDDLProvider = ExecuteDDLProvider;
        }


        public void CreateDatabase()
        {
            ((ICreateDatabase)_dbCreateProvider).Create();
        }

        public void CreateDatabaseEntities()
        {

            SQLCreationScripts creationScripts = new SQLCreationScripts();

            foreach (var sql in creationScripts.Scripts())
            {
                ((IExecuteDDL)_dbCreateProvider).Execute(sql);
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Core
{
    public delegate void SqlStatementExecuted(string Sql, List<ISQLDMLStatementVariable> Variables);

    public interface IDatabaseAccess
    {
        
        event SqlStatementExecuted SqlStatementExecuted;

        IDatabaseTenant Tenant { get; set; }

        bool DoesEntityWithSameUniqueKeyExist(dynamic Entity);

        bool TableExists(string Name);

        List<string> BuildFilterList(string propertyNames);

        void CreateEntityStorageMechanism(object Entity);
        void CreateEntity(object Entity);
        void DeleteEntity(object Entity);
        void UpdateEntity(object Entity);
        object GetEntity(object Entity, List<string> propertiesToUseInFilter);
        object GetEntities(object Entity, List<string> propertiesToUseInFilter);

    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alchemint.Client.JsonAccess
{
    public interface IFabricJsonAccess
    {

        Task<object> GetEntity(object Entity, List<string> propertiesToFilterOn);
        Task<List<object>> GetEntities(object Entity, List<string> propertiesToFilterOn);
        Task<object> CreateEntity(object Entity);
        Task<object> DeleteEntity(object Entity);
        Task<object> UpdateEntity(object Entity);

    }

}

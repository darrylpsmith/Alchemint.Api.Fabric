using Sam.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Alchemint.Core;
namespace Sam.Api
{
    public class Storage<T> where T : class
    {
        private Dictionary<Guid, T> storage = new Dictionary<Guid, T>();
        FabricController _fab = new FabricController();

        public ActionResult<dynamic> GetAll()
        {
            return _fab.GetAll("APIKEY", typeof(T).Name, null);
        }

        public ActionResult<dynamic> GetById(string Id)
        {
            var obj = EntityFactory.GetEmptyTypedObect(typeof(T).Name);
            EntityDescriber ed = new EntityDescriber(obj);
            if (ed.IdField() != null)
            {
                EntityFactory.SetPropertyByName(obj, ed.IdField().Name, Id);
                return _fab.Get("APIKEY", typeof(T).Name, $"{ed.IdField().Name}={Id}");
            }
            else
            {
                throw new Exception($"{typeof(T).Name} does not have a PrimaryKey defined");
            }
        }
        
        public ActionResult<dynamic> Add(T item)
        {
            return _fab.Post("APIKEY", typeof(T).Name, item);
        }

        public ActionResult<dynamic> Update(T item)
        {
            return _fab.Put("APIKEY", typeof(T).Name, item);
        }

        public ActionResult Delete(T item)
        {
            return _fab.Delete("APIKEY", typeof(T).Name, item);
        }

    }
}

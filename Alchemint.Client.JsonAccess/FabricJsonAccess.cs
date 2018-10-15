using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Alchemint.Core;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Alchemint.Client.JsonAccess
{
    public class FabricJsonAccess : IFabricJsonAccess
    {
        //public IBarDatabaseTenant Tenant { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private DataService _dataService = new DataService();
        //private string _webApiUri = "http://localhost:51925/api/";
        //private string _webApiUri = "http://DESKTOP-OLP9HIG:51925/api/"; // IIS Express connection Local

        private string _webApiUri; // = "http://localhost:51926/api/"; //Kestrel Connection Local
        

        //private string _webApiUri = "http://alchemint.azurewebsites.net/api/";

        private string _apiKey = "";

        public FabricJsonAccess(string apiUrl, string apiKey)
        {
            _apiKey = apiKey;
            _webApiUri = apiUrl;
        }

        private bool IsValidJson(dynamic json)
        {
            return (json != null); 
            // && (json != ""));
        }

        private string EntityFabricUri() => _webApiUri + "Fabric";

        public List<string> BuildFilterList(string propertyNames)
        {
            List<string> propertiesToFilterOn = new List<string>();
            string [] arrPropertyNames = propertyNames.Split(',');
            foreach (var prop in arrPropertyNames)
            {
                propertiesToFilterOn.Add(prop);
            }
            return propertiesToFilterOn;
        }
        public async Task<object> GetEntity(object Entity, List<string> propertiesToFilterOn)
        {

            string entityTypeName = Entity.GetType().Name;

            JObject json = await DataService.GetAsync(
                EntityFabricUri(),
                _apiKey,
                Entity,
                propertiesToFilterOn
                );

            if (json != null)
            {
                object entityTyped = EntityFactory.GetEmptyTypedObect(entityTypeName);
                entityTyped = EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(json, entityTyped);
                return entityTyped;
            }
            else
            {
                return null;
            }
        }


        public async Task<List<object>> GetEntities(object Entity, List<string> propertiesToFilterOn)
        {

            string entityTypeName = Entity.GetType().Name;
            List<object> ret = new List<object>();
            JArray arr = await DataService.GetAsync(
                EntityFabricUri(),
                _apiKey,
                Entity,
                propertiesToFilterOn,
                true
                );

            foreach(var jo in arr)
            {
                object o =  JsonConvert.DeserializeObject<object>(jo.ToString());

                object entityTyped = EntityFactory.GetEmptyTypedObect(entityTypeName);
                entityTyped = EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(jo, entityTyped);

                ret.Add(entityTyped);
            }
            return ret;

        }

        public async Task<object> CreateEntity(object Entity)
        {
            string EntityType = Entity.GetType().Name;

            JObject json = await DataService.PostAsync(
                EntityFabricUri(),
                _apiKey,
                Entity
                );

            if (json != null)
            {
                object entityTyped = EntityFactory.GetEmptyTypedObect(EntityType);
                EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(json, entityTyped);
                return entityTyped;
            }
            else
            {
                return null;
            }
        }

        public async Task<object> DeleteEntity(object Entity)
        {

            string EntityType = Entity.GetType().Name;

            JObject json = await DataService.DeleteAsync (
                EntityFabricUri(),
                _apiKey,
                Entity
                );

            if (json != null)
            {
                object entityTyped = EntityFactory.GetEmptyTypedObect(EntityType);
                EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(json, entityTyped);
                return entityTyped;
            }
            else
            {
                return null;
            }
        }

        public async Task<object> UpdateEntity(object Entity)
        {
            string EntityType = Entity.GetType().Name;

            JObject json = await DataService.PutAsync(
                EntityFabricUri(),
                _apiKey,
                Entity
                );

            if (json != null)
            {
                object entityTyped = EntityFactory.GetEmptyTypedObect(EntityType);
                EntityFactory.CopyPropertiesFromDynamicObjectToTypedObject(json, entityTyped);
                return entityTyped;
            }
            else
            {
                return null;
            }
        }


        //private object GetEmptyTypedObect(string EntityType)
        //{
        //    object reflectedEntityObject = CreateInstance<object>(EntityType);
        //    return reflectedEntityObject;
        //}


        //private object CopyPropertiesFromDynamicObjectToTypedObject(dynamic DynamicObject, object TypedObject)
        //{
        //    var properties = TypedObject.GetType().GetProperties();
        //    foreach (var p in properties)
        //    {
        //        string pName = p.Name;
        //        object pValue = DynamicObject[pName].Value;
        //        p.SetValue(TypedObject, pValue);
        //    }
        //    return TypedObject;
        //}


        //private string GetApplicationRoot()
        //{
        //    var exePath = Path.GetDirectoryName(System.Reflection
        //                      .Assembly.GetExecutingAssembly().CodeBase);
        //    Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*"); //(?=\\+bin)
        //    var appRoot = appPathMatcher.Match(exePath).Value;
        //    return appRoot;
        //}
        //private I CreateInstance<I>(string EntityType) where I : class
        //{
        //    //string assemblyPath = Environment.CurrentDirectory + "\\BarClasses.dll";
        //    string assemblyPath = GetApplicationRoot() + "\\BarClasses.dll";

        //    assemblyPath  = this.GetType().Assembly.Location.Replace(this.GetType().Assembly.GetName().Name + ".dll", "") + "BarClasses.dll";

        //    Assembly assembly;
        //    assembly = Assembly.LoadFrom(assemblyPath);
        //    Type type = assembly.GetType($"Alchemint.Bar.{EntityType}");
        //    return Activator.CreateInstance(type) as I;
        //}

    }
}

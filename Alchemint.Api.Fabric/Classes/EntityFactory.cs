using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Alchemint.Core.Fabric
{
    public class EntityFactory
    {
        public static I CreateInstance<I>(string EntityType) where I : class
        {
            string assemblyPath = GetApplicationRoot() + "/BarClasses.dll";
            Assembly assembly;
            assembly = Assembly.LoadFrom(assemblyPath);
            Type type = assembly.GetType($"Alchemint.Bar.{EntityType}");
            return Activator.CreateInstance(type) as I;
        }

        public static object GetEmptyTypedObect(string EntityType)
        {
            object reflectedEntityObject = CreateInstance<object>(EntityType);
            return reflectedEntityObject;
        }


        private static string GetApplicationRoot()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static EntitySearchObject GetSearchEntity(string EntityType, string UniqueKeyQuery)
        {
            EntitySearchObject search = new EntitySearchObject();

            var typedObject = EntityFactory.GetEmptyTypedObect(EntityType);
            search.TypedObject = typedObject;


            if (UniqueKeyQuery != null)
            {
                string[] uniqueKeys = UniqueKeyQuery.Split(",");
                Dictionary<string, string> uqs = new Dictionary<string, string>();

                List<string> propertiesToUseInFilter = new List<string>();

                foreach (var uqpair in uniqueKeys)
                {
                    var uq = uqpair.Split("=");
                    uqs.Add(uq[0], uq[1]);
                }

                var properties = typedObject.GetType().GetProperties();

                foreach (var p in properties)
                {
                    if (uqs.ContainsKey(p.Name))
                    {
                        p.SetValue(typedObject, uqs[p.Name]);
                        propertiesToUseInFilter.Add(p.Name);
                    }
                }

                search.PropertiesToSearch = propertiesToUseInFilter;
            }
            else
            {
                search.PropertiesToSearch = new List<string>();
            }

            return search;

        }


        public static object CopyPropertiesFromDynamicObjectToTypedObject(dynamic DynamicObject, object TypedObject)
        {
            var properties = TypedObject.GetType().GetProperties();
            foreach (var p in properties)
            {
                string pName = p.Name;
                object pValue = DynamicObject[pName].Value;
                p.SetValue(TypedObject, pValue);
            }
            return TypedObject;
        }

    }
}

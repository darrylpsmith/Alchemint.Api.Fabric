using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Alchemint.Core
{
    public class EntityFactory
    {
        private static string _dataModelClassLibrary = "Sam.DataModel.dll";
        private static string _dataModelNameSpace = "Sam.DataModel";

        public static I CreateInstance<I>(string EntityType) where I : class
        {
            Assembly assembly = GetAssembly(GetAssemblyFilePath());
            Type type = assembly.GetType($"{_dataModelNameSpace}.{EntityType}");
            return Activator.CreateInstance(type) as I;
        }


        public static string GetAssemblyFilePath ()
        {
             return GetApplicationRoot() + "/" + _dataModelClassLibrary;
        }
        public static Assembly GetAssembly(string dllName)
        {
            Assembly assembly;
            assembly = Assembly.LoadFrom(dllName);
            return assembly;
        }

        public static object GetEmptyTypedObect(string EntityType)
        {
            object reflectedEntityObject = CreateInstance<object>(EntityType);
            return reflectedEntityObject;
        }

        public static List<string> GetClassNames()
        {
            return (from t in GetAssembly(GetAssemblyFilePath()).GetTypes()
                    where t.GetConstructor(Type.EmptyTypes) != null
                    select t.Name).ToList();
        }



        public static string GetApplicationRoot()
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
                string[] uniqueKeys = UniqueKeyQuery.Split(',');
                Dictionary<string, string> uqs = new Dictionary<string, string>();

                List<string> propertiesToUseInFilter = new List<string>();

                foreach (var uqpair in uniqueKeys)
                {
                    var uq = uqpair.Split('=');
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

        public static void SetPropertyByName (object Entity, string PropertyName, object Value)
        {
            var properties = Entity.GetType().GetProperties();
            foreach (var p in properties)
            {
                if (p.Name == PropertyName)
                {
                    p.SetValue(Entity, Value);
                }
            }
        }
        public static object GetPropertyByName(object Entity, string PropertyName)
        {
            var properties = Entity.GetType().GetProperties();
            object ret = null;

            foreach (var p in properties)
            {
                if (p.Name == PropertyName)
                {
                    ret = p.GetValue(Entity); 
                }
            }

            return ret;
        }


        public static object CopyPropertiesFromDynamicObjectToTypedObject(dynamic DynamicObject, object TypedObject)
        {
            var properties = TypedObject.GetType().GetProperties();
            foreach (var p in properties)
            {
                string pName = p.Name;

                if (DynamicObject[pName] != null)
                {
                    object pValue = DynamicObject[pName].Value;


                    if (pValue != null)
                    {
                        if (pValue.GetType() != p.PropertyType)
                        {

                            if (pValue.GetType().Name.StartsWith("Int"))
                            {
                                pValue = Convert.ToInt32(pValue);
                            }
                            else
                            {
                                throw new Exception($"Property types for property {pName} of object type {TypedObject.GetType().Name} do not match: {pValue.GetType()} ==> {p.PropertyType}");
                            }
                            //{ System.Int64}
                            //{ System.Int32}
                        }
                    }

                    p.SetValue(TypedObject, pValue);

                }
            }
            return TypedObject;
        }

    }
}

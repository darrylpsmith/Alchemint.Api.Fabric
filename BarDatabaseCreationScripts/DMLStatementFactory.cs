using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;
using System.Data;
using System.Linq;

namespace Alchemint.Core
{
    public static class DMLStatementFactory
    {

        internal static SQLDMLStatement GetDMLStatementForGenericEntity(IDatabaseTenant Tenant, object Entity, DMLStatemtType dMLStatemtType, List<string> querProperyPArametersToUse)
        {

            List<object> propValues = GetObjectPropertyValues(Entity);
            List<string> propNames = GetObjectPropertyNames(Entity);

            List<ISQLDMLStatementVariable> uniqueKeys = null;

            if (dMLStatemtType == DMLStatemtType.SelectAll && querProperyPArametersToUse == null)
            {
                uniqueKeys = new List<ISQLDMLStatementVariable>
                {
                    new SQLDMLStatementVariable { Name = "@Tenant", Value = "TR1" }
                };
            }
            else if (querProperyPArametersToUse == null)
            {
                uniqueKeys = GetUniqueKeyNameValuePairs(Entity, dMLStatemtType == DMLStatemtType.Update || dMLStatemtType == DMLStatemtType.Delete);
            }
            else
            {
                uniqueKeys = GetFilterNameValuePairs(Entity);
            }

            List<ISQLDMLStatementVariable> uniqueKeysFiltered = new List<ISQLDMLStatementVariable>();

            if (querProperyPArametersToUse != null)
            {
                querProperyPArametersToUse.Add("Tenant");
                foreach (var key in uniqueKeys)
                {
                    if (querProperyPArametersToUse.Contains(key.Name.Replace("@","")))
                    {
                        uniqueKeysFiltered.Add(key);
                    }
                }
            }
            else
            {
                uniqueKeysFiltered = uniqueKeys;
            }


            SQLDMLScripts dmlScripts = new SQLDMLScripts();
            return dmlScripts.GetInsertScriptForTypedEntity(Tenant, Entity, propNames, propValues, dMLStatemtType , uniqueKeysFiltered);
        }


        internal static SQLDMLStatement GetDDLStatementGenericEntityStorageCreation(IDatabaseTenant Tenant, object Entity)
        {
            var statement = new SQLDMLStatement
            {
                PreparedStatement = BuildTableCreateForEntitySql(Entity),
                StatemtType = DMLStatemtType.Create
            };

            return statement;
        }


        internal static SQLDMLStatement GetDMLStatement(IDatabaseTenant Tenant, BarDMLScript ScriptId , object [] Parameters)
        {
            SQLDMLScripts dmlScripts = new SQLDMLScripts();

            if (ScriptId == BarDMLScript.eCreateUser)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eCreateUser, Parameters);

            if (ScriptId == BarDMLScript.eCreateInstitution)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eCreateInstitution, Parameters);

            if (ScriptId == BarDMLScript.eCreateToken)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eCreateToken, Parameters);

            if (ScriptId == BarDMLScript.eCreateWallet)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eCreateWallet, Parameters);

            if (ScriptId == BarDMLScript.eCreateTransaction)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eCreateTransaction, Parameters);

            if (ScriptId == BarDMLScript.eDeletUser)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eDeletUser, Parameters);

            if (ScriptId == BarDMLScript.eDeleteInstitution)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eDeleteInstitution, Parameters);

            if (ScriptId == BarDMLScript.eDeleteToken)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eDeleteToken, Parameters);

            if (ScriptId == BarDMLScript.eDeleteWallet)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eDeleteWallet, Parameters);

            if (ScriptId == BarDMLScript.eDeleteTransaction)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eDeleteTransaction, Parameters);

            if (ScriptId == BarDMLScript.eGetUser)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetUser, Parameters);

            if (ScriptId == BarDMLScript.eGetUserByLoginDetails)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetUserByLoginDetails, Parameters);

            if (ScriptId == BarDMLScript.eGetAllUsers)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetAllUsers, Parameters);

            if (ScriptId == BarDMLScript.eGetInstitution)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetInstitution, Parameters);

            if (ScriptId == BarDMLScript.eGetInstitutionByLoginDetails)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetInstitutionByLoginDetails, Parameters);

            if (ScriptId == BarDMLScript.eGetAllInstitutions)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetAllInstitutions, Parameters);

            if (ScriptId == BarDMLScript.eGetToken)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetToken, Parameters);

            if (ScriptId == BarDMLScript.eGetWallet)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetWallet, Parameters);

            if (ScriptId == BarDMLScript.eGetTransaction)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetTransaction, Parameters);

            if (ScriptId == BarDMLScript.eUpdateUser)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eUpdateUser, Parameters);

            if (ScriptId == BarDMLScript.eUpdateInstitution)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eUpdateInstitution, Parameters);

            if (ScriptId == BarDMLScript.eUpdateToken)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eUpdateToken, Parameters);

            if (ScriptId == BarDMLScript.eUpdateWallet)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eUpdateWallet, Parameters);

            if (ScriptId == BarDMLScript.eUpdateTransaction)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eUpdateTransaction, Parameters);

            if (ScriptId == BarDMLScript.eSendTokens)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eSendTokens, Parameters);

            if (ScriptId == BarDMLScript.eGetBalance)
                return dmlScripts.GetScript(Tenant, BarDMLScript.eGetBalance, Parameters);

            throw new Exception("CODE LOGIC ERROR : " + ScriptId.ToString() + " not in statement list.");
        }

        private static List<object> GetObjectPropertyValues(object Entity)
        {
            EntityDescriber describer = new EntityDescriber(Entity);
            return describer.AllPropertyValues().Select(x => x.Value).ToList<object>();
        }

        private static List<string> GetObjectPropertyNames(object Entity)
        {
            EntityDescriber describer = new EntityDescriber(Entity);
            return describer.AllPropertyValues().Select(x => x.Name).ToList<string>();
        }

        /// <summary>
        /// Inspects the properties of an an object looking for those that make up the unique identifier
        /// and from this and returns a list of variables usable in a prepared sql statement 
        /// with names and values for these
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public static string BuildExistenceCheckSql(object Entity)
        {
            string sql = $"select count(*) from {Entity.GetType().Name} " + BuildUniqueKeyWhereClause(Entity);
            return sql;
        }

        /// <summary>
        /// Inspects the properties of an an object looking for those that make up the unique identifier
        /// and from this and returns a list of variables usable in a prepared sql statement 
        /// with names and values for these
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public static string BuildTableCreateForEntitySql(object Entity)
        {
            EntityDescriber describer = new EntityDescriber(Entity);

            string sql = $"create table {Entity.GetType().Name} (";
            sql += "Tenant varchar(100), ";

            var type = Entity.GetType();
            string fieldListSql = "";
            int i = 0;

            var properties = describer.AllPropertyValues();
            foreach (var property in properties)
            {
                fieldListSql += (i > 0 ? ", " : "") + property.Name + " " + MapPropertyTypeToDBType(property.Type.Name);
                i++;
            }

            List<EntityProperty> primaryKeys = null;
            primaryKeys = describer.PrimaryKeys();
            int primKeyCount = 0;
            string primaryKey = "PRIMARY KEY(";
            primaryKey += "Tenant, ";

            foreach (var pk in primaryKeys)
            {
                primaryKey += (primKeyCount > 0 ? ", " : "") + pk.Name;
                primKeyCount++;
            }

            List<EntityProperty> uniqueKeys = null;
            uniqueKeys = describer.UniqueKeys();
            int uniqueKeyCount = 0;
            string uniqueKey = $"CONSTRAINT UQ_{Entity.GetType().Name} UNIQUE(";
            uniqueKey += "Tenant, ";

            foreach (var uk in uniqueKeys)
            {
                uniqueKey += (uniqueKeyCount > 0 ? ", " : "") + uk.Name;
                uniqueKeyCount++;
            }

            sql += fieldListSql;

            if (primKeyCount > 0)
                sql += ", " + primaryKey + ")";

            if (uniqueKeyCount > 0)
                sql += ", " + uniqueKey + ")";

            sql += ")";

            return sql;
        }

        private static string MapPropertyTypeToDBType (string propertyType)
        {
            propertyType = propertyType.Replace("String", "varchar(255)");
            propertyType = propertyType.Replace("Int16", "int");
            propertyType = propertyType.Replace("Int32", "int");
            propertyType = propertyType.Replace("Int64", "int");
            propertyType = propertyType.Replace("DateTime", "datetime");
            propertyType = propertyType.Replace("Double", "double");
            propertyType = propertyType.Replace("Decimal", "decimal");
            return propertyType;
        }


        public static string BuildUniqueKeyWhereClause(object Entity)
        {
            EntityDescriber describer = new EntityDescriber(Entity);
            List<EntityProperty> keys = null;
            keys = describer.UniqueKeys();

            string whereClause = "";
            int i = 0;

            foreach (var ky in keys)
            {
                whereClause += (i > 0 ? " and " : $" where Tenant = @Tenant and ") + ky.Name + $" = @{ky.Name}";
                i++;
            }

            return whereClause;

        }

        /// <summary>
        /// Inspects the properties of an an object looking for those that make up the unique identifier
        /// and from this returns a list of variables usable in a prepared sql statement 
        /// with names and values for these
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public static List<ISQLDMLStatementVariable> GetUniqueKeyNameValuePairs(object Entity, bool includePrimaryKey)
        {
            List<ISQLDMLStatementVariable> vars = new List<ISQLDMLStatementVariable>
            {
                new SQLDMLStatementVariable { Name = "@Tenant", Value = "TR1" }
            };

            EntityDescriber describer = new EntityDescriber(Entity);
            List<EntityProperty> keys = null;

            if (includePrimaryKey)
                keys = describer.PrimaryAndUniqueKeys();
            else
                keys = describer.UniqueKeys();

            foreach (var key in keys)
            {
                vars.Add(new SQLDMLStatementVariable { Name = "@" + key.Name, Value = key.Value });
            }

            return vars;

        }


        /// <summary>
        /// Inspects the properties of an an object looking for those that make up the unique identifier
        /// and from this returns a list of variables usable in a prepared sql statement 
        /// with names and values for these
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public static List<ISQLDMLStatementVariable> GetFilterNameValuePairs(object Entity)
        {
            List<ISQLDMLStatementVariable> vars = new List<ISQLDMLStatementVariable>
            {
                new SQLDMLStatementVariable { Name = "@Tenant", Value = "TR1" }
            };

            EntityDescriber describer = new EntityDescriber(Entity);
            foreach (var property in describer.AllPropertyValues())
            {
                var value = property.Value;
                if (value != null)
                {
                    vars.Add(new SQLDMLStatementVariable { Name = "@" + property.Name, Value = value });
                }
            }

            return vars;
        }

    }
}

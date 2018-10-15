using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Data;
using Alchemint.Core;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;


namespace Alchemint.Core
{

    public class DatabaseAccess : IDatabaseAccess
    {
        IExecuteDML _dbaccess;
        IDatabaseTenant _tenant = null;

        public event SqlStatementExecuted SqlStatementExecuted;

        public IDatabaseTenant Tenant
        {
            get { return _tenant; }
            set { _tenant = value; }
        }

        public DatabaseAccess(IExecuteDML dmlExecutionProvider, IDatabaseTenant BarTenant)
        {
            _dbaccess = dmlExecutionProvider;
            
            Tenant = BarTenant;
            if (Tenant == null)
                throw new Exception("CODE LOGIC ERROR: Tenant may not be null");
        }


        public List<string> BuildFilterList(string propertyNames)
        {
            List<string> propertiesToFilterOn = new List<string>();
            string[] arrPropertyNames = propertyNames.Split(',');
            foreach (var prop in arrPropertyNames)
            {
                propertiesToFilterOn.Add(prop);
            }
            return propertiesToFilterOn;
        }

        public void CreateEntity(object Entity)
        {
            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.Insert, null);
            ((IExecuteDML)_dbaccess).Execute(Statement);

            //this.sqlStatementExecuted(Statement.PreparedStatement);


            SqlStatementExecuted?.Invoke(Statement.PreparedStatement, Statement.Variables);
        }

        public void CreateEntityStorageMechanism(object Entity)
        {
           
            var Statement = DMLStatementFactory.GetDDLStatementGenericEntityStorageCreation(_tenant,Entity);
            ((IExecuteDML)_dbaccess).Execute(Statement);

            //this.sqlStatementExecuted(Statement.PreparedStatement);

            SqlStatementExecuted?.Invoke(Statement.PreparedStatement, Statement.Variables);

        }

        public long DeleteEntity(object Entity)
        {

            EntityDescriber ed = new EntityDescriber(Entity);
            if (ed.UniqueKeyProvidedOnEntity() == false && ed.PrimaryKeyProvidedOnEntity() == false)
            {
                throw new Exception("Neither a Primary Key nor UniqueKey was provided for the object.");
            }

            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.Delete, null);
            var ret = (Int32)((IExecuteDML)_dbaccess).Execute(Statement);
            SqlStatementExecuted?.Invoke(Statement.PreparedStatement, Statement.Variables);
            return ret;
        }

        public long UpdateEntity(object Entity)
        {
            EntityDescriber ed = new EntityDescriber(Entity);
            if (ed.UniqueKeyProvidedOnEntity() == false && ed.PrimaryKeyProvidedOnEntity() == false)
            {
                throw new Exception("Neither a Primary Key nor UniqueKey was provided for the object.");
            }

            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.Update, null);
            var ret = (Int32) ((IExecuteDML)_dbaccess).Execute(Statement);
            SqlStatementExecuted?.Invoke(Statement.PreparedStatement, Statement.Variables);
            return ret;
        }

        public object GetEntity(object Entity, List<string> propertiesToUseInFilter)
        {
            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.Select, propertiesToUseInFilter);
            this.SqlStatementExecuted(Statement.PreparedStatement, Statement.Variables);
            return ((IExecuteDML)_dbaccess).Execute(Statement);
        }


        public object GetEntities(object Entity, List<string> propertiesToUseInFilter = null)
        {
            var Statement = DMLStatementFactory.GetDMLStatementForGenericEntity(_tenant, Entity, DMLStatemtType.SelectAll, propertiesToUseInFilter);
            this.SqlStatementExecuted(Statement.PreparedStatement, Statement.Variables);
            return ((IExecuteDML)_dbaccess).Execute(Statement);

        }

        public bool TableExists(string Name)
        {
            return _dbaccess.TableExists(Name);
        }

        public bool DoesEntityWithSameUniqueKeyExist(object Entity)
        {
            EntityDescriber ed = new EntityDescriber(Entity);
            if (ed.UniqueKeys().Count > 0)
            {
                var sql = DMLStatementFactory.BuildExistenceCheckSql(Entity, false);
                List<ISQLDMLStatementVariable> vars = DMLStatementFactory.GetUniqueKeyNameValuePairs(Entity, false, true);
                SQLDMLStatement dml = new SQLDMLStatement
                {
                    PreparedStatement = sql,
                    StatemtType = DMLStatemtType.Select,
                    Variables = vars
                };

                var results = (DataTable)((IExecuteDML)_dbaccess).Execute(dml);
                var count = Convert.ToInt32(results.Rows[0].ItemArray[0]);
                return count > 0;
            }
            else
            {
                return false;
            }
        }


        public bool DoesEntityWithSamePrimaryKeyExist(object Entity)
        {
            EntityDescriber ed = new EntityDescriber(Entity);
            if (ed.PrimaryKeys().Count > 0)
            {
                var sql = DMLStatementFactory.BuildExistenceCheckSql(Entity, true);
                List<ISQLDMLStatementVariable> vars = DMLStatementFactory.GetUniqueKeyNameValuePairs(Entity, true, false);
                SQLDMLStatement dml = new SQLDMLStatement
                {
                    PreparedStatement = sql,
                    StatemtType = DMLStatemtType.Select,
                    Variables = vars
                };

                var results = (DataTable)((IExecuteDML)_dbaccess).Execute(dml);
                var count = Convert.ToInt32(results.Rows[0].ItemArray[0]);
                return count > 0;
            }
            else
            {
                return false;
            }
        }
    }
}


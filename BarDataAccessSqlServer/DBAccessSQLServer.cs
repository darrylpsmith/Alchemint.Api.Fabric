using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Alchemint.Core
{
    public class DBAccessSQServer : ICreateDatabase, IExecuteDDL, IExecuteDML
    {
        readonly string _conn = "";

        public DBAccessSQServer(string connString)
        {
            _conn = connString;
        }

        void ICreateDatabase.Create()
        {
        }

        void IExecuteDDL.Execute(ISQLDDLStatement Statement)
        {
            bool returnDataSet = false;
            DbConnection connection = new SqlConnection(_conn);
            DbCommand command = GetSQLServerCommandFromDDLScript(Statement);
            StaticDatabaseFunctions.ExecSqlCommand(connection, command, returnDataSet, null);
        }

        object IExecuteDML.Execute(ISQLDMLStatement Statement)
        {
            DbConnection connection = new SqlConnection(_conn);
            DbCommand command = GetSQLServerCommandFromDMLScript(Statement);
            bool returnDataSet = (Statement.StatemtType == DMLStatemtType.Select || Statement.StatemtType == DMLStatemtType.SelectAll);
            DbDataAdapter adapter = new SqlDataAdapter((SqlCommand)command);
            return StaticDatabaseFunctions.ExecSqlCommand(connection, command, returnDataSet, adapter);
        }

        private DbCommand GetSQLServerCommandFromDMLScript(ISQLDMLStatement Statement)
        {
            DbCommand command = new SqlCommand(Statement.PreparedStatement);

            if (Statement.Variables != null)
            {
                foreach (var variable in Statement.Variables)
                {
                    command.Parameters.Add(new SqlParameter { Value = variable.Value, ParameterName = variable.Name });
                }
            }

            return command;
        }

        private DbCommand GetSQLServerCommandFromDDLScript(ISQLDDLStatement Statement)
        {
            SqlCommand command = new SqlCommand(Statement.PreparedStatement);

            return command;
        }

        public bool TableExists(string EntityType)
        {
            var sql = "select count(*) From sysobjects where type='U' and name = @name;";
            List<ISQLDMLStatementVariable> vars = new List<ISQLDMLStatementVariable>
            {
                new SQLDMLStatementVariable { Name = "@name", Value = EntityType }
            };

            SQLDMLStatement dml = new SQLDMLStatement
            {
                PreparedStatement = sql,
                StatemtType = DMLStatemtType.Select,
                Variables = vars
            };

            var results = (DataTable)(((IExecuteDML)this)).Execute(dml);
            var count = Convert.ToInt32(results.Rows[0].ItemArray[0]);

            return count > 0;
        }
    }
}

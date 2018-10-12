using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace Alchemint.Core
{
    public class DBAccessSqlite : ICreateDatabase, IExecuteDDL, IExecuteDML
    {
        readonly string _dataFile;

        public DBAccessSqlite(string DataFile)
        {
            _dataFile = DataFile;
        }

        void ICreateDatabase.Create()
        {
            if (File.Exists(_dataFile))
                File.Delete(_dataFile);

            SQLiteConnection.CreateFile(_dataFile);
        }

        void IExecuteDDL.Execute(ISQLDDLStatement Statement)
        {
            bool returnDataSet = false;
            DbConnection connection = new SQLiteConnection($"Data Source={_dataFile};Version=3;");
            DbCommand command = GetSQLiteCommandFromDDLScript(Statement);
            StaticDatabaseFunctions.ExecSqlCommand(connection, command, returnDataSet, null);
        }

        object IExecuteDML.Execute(ISQLDMLStatement Statement)
        {
            DbConnection connection = new SQLiteConnection($"Data Source={_dataFile};Version=3;");
            DbCommand command = GetSQLLiteCommandFromDMLScript(Statement);
            bool returnDataSet = (Statement.StatemtType == DMLStatemtType.Select) || (Statement.StatemtType == DMLStatemtType.SelectAll);
            var adapter = new SQLiteDataAdapter ((SQLiteCommand)command);
            return StaticDatabaseFunctions.ExecSqlCommand(connection, command, returnDataSet, adapter);
        }
        private DbCommand GetSQLLiteCommandFromDMLScript(ISQLDMLStatement Statement)
        {
            DbCommand command = new SQLiteCommand(Statement.PreparedStatement);
            if (Statement.Variables != null)
            {
                foreach (var variable in Statement.Variables)
                {
                    command.Parameters.Add(new SQLiteParameter { Value = variable.Value, ParameterName = variable.Name });
                }
            }

            return command;
        }

        private SQLiteCommand GetSQLiteCommandFromDDLScript(ISQLDDLStatement Statement)
        {
            SQLiteCommand command = new SQLiteCommand(Statement.PreparedStatement);

            return command;
        }

        public bool TableExists(string EntityType)
        {
            var sql = "select count(*) From sqlite_master where type='table' and name = @name;";
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

            var results =  (DataTable)(((IExecuteDML)this)).Execute(dml);
            var count = Convert.ToInt32(results.Rows[0].ItemArray[0]);

            return count > 0;
        }

    }
}

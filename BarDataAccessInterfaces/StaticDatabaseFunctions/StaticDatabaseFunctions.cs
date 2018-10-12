using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace Alchemint.Core
{
    public static class StaticDatabaseFunctions
    {

        public static object ExecSqlCommand(IDbConnection Connection, IDbCommand Command, bool ReturnDataSet, IDataAdapter DataAdapter)
        {

            Connection.Open();
            int rows;
            object returnValue = null;

            Command.Connection = Connection;
            if (ReturnDataSet)
            {
                var ds = new DataSet();
                DataAdapter.Fill(ds);
                returnValue = ds;
                returnValue = ds.Tables[0];

                //if (ds.Tables.Count >= 1)
                //{
                //    if (ds.Tables[0].Rows.Count >= 1)
                //    {
                //        //returnValue = DataSetToJsonString(ds);
                //        returnValue = ds.Tables[0].Rows[0].ItemArray;
                //    }
                //}

            }
                else
            {
                rows = Command.ExecuteNonQuery();
                returnValue = rows;
            }

            Connection.Close();
            return returnValue;

        }


        //private static string DataSetToJsonString (DataTable dt)
        //{
        //    string json = new JObject(
        //        dt.Columns.Cast<DataColumn>()
        //          .Select(c => new JProperty(c.ColumnName, JToken.FromObject(dt.Rows[0][c])))
        //    ).ToString(Formatting.Indented);

        //    json = JValue.Parse(json).ToString(Formatting.Indented);

        //    //string json = JsonConvert.SerializeObject(Row, Formatting.Indented);
        //    return json;
        //}


    }
}

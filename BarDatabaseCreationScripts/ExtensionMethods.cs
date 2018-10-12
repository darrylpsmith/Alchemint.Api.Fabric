using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Alchemint.Core
{
    //Extension methods must be defined in a static class
    public static class DataRowExtension
    {
        //// This is the extension method.
        //// The first parameter takes the "this" modifier
        //// and specifies the type for which the method is defined.
        //public static int ToJson(this DataRow row)
        //{
        //    return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        //}


        private static string DataSetFirstRowToJsonString(this DataTable dt)
        {

            string json;

            if (dt.Rows.Count > 0)
            {
                json = new JObject(
                    dt.Columns.Cast<DataColumn>()
                      .Select(c => new JProperty(c.ColumnName, JToken.FromObject(dt.Rows[0][c])))
                ).ToString(Formatting.Indented);

                json = JValue.Parse(json).ToString(Formatting.Indented);
            }
            else
            {
                json= "";
            }
            //string json = JsonConvert.SerializeObject(Row, Formatting.Indented);
            return json;
        }



        public static object DataSetRowToBarBusinessObject(this DataTable dt, System.Type type)
        {
            string json = dt.DataSetFirstRowToJsonString();
            var outputObject = JsonConvert.DeserializeObject(json, type);

            return outputObject; 

        }


        private static List<string> DataSetToListOfJsonString(this DataTable dt)
        {

            List<string> jsonList;
            string json;

            if (dt.Rows.Count > 0)
            {
                jsonList = new List<string> ();
                foreach (DataRow row in dt.Rows)
                {
                    json = new JObject(
                        dt.Columns.Cast<DataColumn>()
                          .Select(c => new JProperty(c.ColumnName, JToken.FromObject(row[c])))
                    ).ToString(Formatting.Indented);

                    jsonList.Add(JValue.Parse(json).ToString(Formatting.Indented));
                }
            }
            else
            {
                jsonList = null;
            }
            //string json = JsonConvert.SerializeObject(Row, Formatting.Indented);
            return jsonList;
        }


        public static List<object> DataSetToBarBusinessObjectList(this DataTable dt, System.Type type)
        {
            List<string> jsonObjects = dt.DataSetToListOfJsonString();
            List<object> objects = new List<object>();

            if (jsonObjects != null)
            {
                foreach (var obj in jsonObjects)
                {
                    objects.Add(JsonConvert.DeserializeObject(obj, type));
                }
            }

            return objects; 

        }

    }
}

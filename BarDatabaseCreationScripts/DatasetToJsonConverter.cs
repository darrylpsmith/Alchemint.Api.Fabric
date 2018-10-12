using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using Newtonsoft.Json;

namespace Alchemint.Core
{
    public static class DatasetToJsonConverter
    {
        public static string DataSetToJsonString(DataTable dt)
        {
            string json = new JObject(
                dt.Columns.Cast<DataColumn>()
                  .Select(c => new JProperty(c.ColumnName, JToken.FromObject(dt.Rows[0][c])))
            ).ToString(Formatting.Indented);

            json = JValue.Parse(json).ToString(Formatting.Indented);

            //string json = JsonConvert.SerializeObject(Row, Formatting.Indented);
            return json;
        }

    }


}

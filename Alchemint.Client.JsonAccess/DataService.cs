using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Alchemint.Core;

namespace Alchemint.Client.JsonAccess
{
    public class DataService
    {
        public static async Task<dynamic> GetAsync(string queryString, List<string> propertiesToFilterOn)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            response.EnsureSuccessStatusCode();

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }

        private static string GetQueryStringForEntity(object Entity, List<string> propertiesToFilterOn)
        {
            EntityDescriber describer = new EntityDescriber(Entity);
            var properties = describer.AllPropertyValues();

            string queryString = "UniqueKeyQuery=";
            int i = 0;
            bool filterSpecified = false;
            if (propertiesToFilterOn != null)
            {
                filterSpecified = true;
            }


            foreach (var property in properties)
            {
                var value = property.Value;
                bool includeThisProperty = false;

                if (filterSpecified)
                    includeThisProperty = propertiesToFilterOn.Contains(property.Name);
                else
                    includeThisProperty = true;

                if (value != null && includeThisProperty)
                {
                    queryString += i > 0 ? "," : "";
                    queryString += $"{property.Name}={value}";
                    i++;
                }
            }

            return queryString;
        }
        public static async Task<dynamic> GetAsync(string uri, string ApiKey, object entity, List<string> propertiesToFilterOn, bool returnsAlllist = false)
        {
            string strBody = JsonConvert.SerializeObject(entity);
            string entityType = entity.GetType().Name;
            string query = GetQueryStringForEntity(entity, propertiesToFilterOn);

            uri += "/" + ApiKey;
            uri += "/" + entityType + (returnsAlllist ? "/ALL" : "") + "?" + query;


            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            //request.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            //JObject data = null;
            object data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
                //data = (JObject)JsonConvert.DeserializeObject(json);
            }

            return data;
        }
        //public static async Task<dynamic> PostAsync(string uri, string body)
        //{

        //    throw new Exception("Depracated");

        //    StringContent bd = new StringContent(body);
        //    bd.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        //    HttpClient client = new HttpClient();
        //    var response = await client.PostAsync(uri, bd);

        //    response.EnsureSuccessStatusCode();

        //    dynamic data = null;
        //    if (response != null)
        //    {
        //        string json = response.Content.ReadAsStringAsync().Result;
        //        data = JsonConvert.DeserializeObject(json);
        //    }

        //    return data;
        //}

        public static async Task<JObject> PostAsync(string uri, string ApiKey, object entity = null)
        {

            string strBody = JsonConvert.SerializeObject(entity);
            string entityType = entity.GetType().Name;

            uri += "/" + ApiKey;
            uri += "/" + entityType;

            HttpResponseMessage response = null;

            if (entity != null)
            {
                StringContent bd = new StringContent(strBody);
                bd.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpClient client = new HttpClient();
                response = await client.PostAsync(uri, bd);
            }
            else
            {
                StringContent bd = new StringContent("");
                bd.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpClient client = new HttpClient();
                response = await client.PostAsync(uri, bd);
            }

            response.EnsureSuccessStatusCode();

            JObject data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = (JObject) JsonConvert.DeserializeObject(json);
            }

            return data;
        }



        //public static async Task<dynamic> PutAsync(string uri, string body)
        //{
        //    StringContent bd = new StringContent(body);
        //    bd.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        //    HttpClient client = new HttpClient();
        //    var response = await client.PutAsync(uri, bd);

        //    response.EnsureSuccessStatusCode();

        //    dynamic data = null;
        //    if (response != null)
        //    {
        //        string json = response.Content.ReadAsStringAsync().Result;
        //        data = JsonConvert.DeserializeObject(json);
        //    }

        //    return data;
        //}


        public static async Task<dynamic> PutAsync(string uri, string apiKey, object entity)
        {
            string strBody = JsonConvert.SerializeObject(entity);
            string entityType = entity.GetType().Name;

            uri += "/" + apiKey;
            uri += "/" + entityType;

            StringContent bd = new StringContent(strBody);
            bd.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            var response = await client.PutAsync(uri, bd);

            response.EnsureSuccessStatusCode();

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }
        public static async Task<dynamic> DeleteAsync(string uri, string apiKey, object entity = null)
        {

            string strBody = JsonConvert.SerializeObject(entity);
            string entityType = entity.GetType().Name;

            uri += "/" + apiKey;
            uri += "/" + entityType;

            HttpClient client = new HttpClient();
            //var response = await client.DeleteAsync(uri,);

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }


    }





}

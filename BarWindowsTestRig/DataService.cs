using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace BarWindowsTestRig
{
    public class DataService
    {
        public static async Task<dynamic> getDataFromService(string queryString)
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

        public static async Task<dynamic> PostAsync(string uri, string body)
        {
            StringContent bd = new StringContent(body);
            bd.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(uri, bd);

            response.EnsureSuccessStatusCode();

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }


        public static async Task<dynamic> PutAsync(string uri, string body)
        {
            StringContent bd = new StringContent(body);
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

        public static async Task<dynamic> DeleteAsync(string uri)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(uri);

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

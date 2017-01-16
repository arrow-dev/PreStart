using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PreStart.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PreStart.Services
{
    class SalesforceDataService
    {
        public async Task<SaleForceResponse<T>> GetData<T>(string query)
        {
            var client = new HttpClient();

            Dictionary<string, string> myContent = new Dictionary<string, string>();
            myContent.Add("grant_type", "password");
            myContent.Add("client_id", Config.ConsumerKey);
            myContent.Add("client_secret", Config.ConsumerSecret);
            myContent.Add("username", Config.Username);
            myContent.Add("password", Config.Password + Config.Token);

            HttpContent content = new FormUrlEncodedContent(myContent);

            HttpResponseMessage message =
                await client.PostAsync("https://test.salesforce.com/services/oauth2/token", content);

            string responseString = await message.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(responseString);

            string oauthToken = (string) obj["access_token"];
            string serviceUrl = (string) obj["instance_url"];

            
            var salesforceQuery = serviceUrl + "/services/data/v25.0/query?q=" + query;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, salesforceQuery);
            request.Headers.Add("Authorization", "Bearer " + oauthToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response= await client.SendAsync(request);
            string result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SaleForceResponse<T>>(result);

            return data;
        }
    }
}

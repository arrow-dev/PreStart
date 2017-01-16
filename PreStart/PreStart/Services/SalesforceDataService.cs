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
        public async Task<SaleForceResponse<Location>> GetLocationData()
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

            string locationQuery = "select+id,name+from+location__c";
            locationQuery = serviceUrl + "/services/data/v25.0/query?q=" + locationQuery;
            HttpRequestMessage requestRegion = new HttpRequestMessage(HttpMethod.Get, locationQuery);
            requestRegion.Headers.Add("Authorization", "Bearer " + oauthToken);
            requestRegion.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseLocations = await client.SendAsync(requestRegion);
            string resultLocations = await responseLocations.Content.ReadAsStringAsync();
            var test = JsonConvert.DeserializeObject<SaleForceResponse<Location>>(resultLocations);

            return test;
        }
    }
}

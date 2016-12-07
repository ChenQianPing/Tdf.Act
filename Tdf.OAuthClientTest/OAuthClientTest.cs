using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.OAuthClientTest
{
    public class OAuthClientTest
    {
        private HttpClient _httpClient;

        public OAuthClientTest()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(30);

            _httpClient.BaseAddress = new Uri("http://localhost:7667");
        }

        public void Get_Accesss_Token_By_Client_Credentials_Grant()
        {
            var parameters = new Dictionary<string, string>();
            // get access_token
            parameters.Add("UserName", "Bobby");
            parameters.Add("Password", "123456");
            parameters.Add("grant_type", "password");

            // refresh_token
            //parameters.Add("grant_type", "refresh_token");
            //parameters.Add("refresh_token", "ED27C4E0-86B0-4149-8BB4-8637DA20D574");

            Console.WriteLine(_httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters))
                .Result.Content.ReadAsStringAsync().Result);
        }

    }
}

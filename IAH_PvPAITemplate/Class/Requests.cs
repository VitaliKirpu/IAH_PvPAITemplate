using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IAH_PvPAITemplate.Class
{
    public abstract class Requests
    {
        public string apiPassword;
        public string remoteBotIp;
        public HttpClient httpClientReference;

        public async Task<bool> GetApiPassword()
        {
            // API Password is used to authenticate your APILicense with the game and provide
            // you with a password that you have to protect during your gameplay session.
            // API Password gives you ownership of the bots that are associated with your RemoteUserBot.
            // When you perform Bot Functions you need always supply API Password.
            // API Password is always regenerated when your RemoteUserBot respawns. if this function is not called every 15-24 seconds game will exit AI Mode.
            // In multiplayer certain bots can crack API Passwords giving you or enemies ability to hjack bots.
            // API Key and API Password are not the same thing, never reveal your API Key.

            if (!string.IsNullOrEmpty(apiPassword))
            {
                return true;
            }

            var jsonData = JsonConvert.SerializeObject(new Dictionary<string, object>
            {
                {"ip", "127.0.0.1"},
                {"apiKey", "JnJ3ykpbEUxE-160il"} // HTTPS://IAMHACKER.CC -> Get API Key
            });

            var postResponse = await SendPostRequestAsync("/v1/apipassword", jsonData);
            if (postResponse.isSuccessStatusCode)
            {
                var passwordResponse = JsonConvert.DeserializeObject<ApiPasswordResponse>(postResponse.responseString);
                apiPassword = passwordResponse.apiPassword;
                remoteBotIp = passwordResponse.ip;


                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<PostResponse> SendPostRequestAsync(string endpoint, string jsonData)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await httpClientReference.SendAsync(request).ConfigureAwait(false);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return new PostResponse
                {responseString = responseContent, isSuccessStatusCode = response.IsSuccessStatusCode};
        }

        public async Task<List<Entity>> GetEntities()
        {
            var getResponse = await httpClientReference.GetAsync("/v1/entities");
            var responseContent = await getResponse.Content.ReadAsStringAsync();
            if (getResponse.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<EntitiesResponse>(responseContent);
                return data.entities;
            }


            return new List<Entity>();
        }

        public async Task<MatchStateResponse> GetMatchState()
        {
            var getResponse = await httpClientReference.GetAsync("/v1/matchstate");
            var responseContent = await getResponse.Content.ReadAsStringAsync();
            if (getResponse.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<MatchStateResponse>(responseContent);
                return data;
            }


            return new MatchStateResponse();
        }
        
        public async Task<bool> RayCast(string uniqueID, string targetUniqueID)
        {
            var jsonData = JsonConvert.SerializeObject(new Dictionary<string, object>
            {
                {"uniqueID", uniqueID},
                {"targetUniqueID", targetUniqueID}
            });

            var postResponse = await SendPostRequestAsync("/v1/raycast", jsonData);
            if (postResponse.isSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> BotAction(string entityUniqueID, string action, object actionValue)
        {
            var jsonData = JsonConvert.SerializeObject(new Dictionary<string, object>
            {
                {"ip", remoteBotIp},
                {"apiPassword", apiPassword},
                {"entityUniqueID", entityUniqueID},
                {"actionType", action},
                {"actionValue", actionValue}
            });

            var postResponse = await SendPostRequestAsync("/v1/botaction", jsonData);
            if (postResponse.isSuccessStatusCode) return true;


            return false;
        }


    }
}
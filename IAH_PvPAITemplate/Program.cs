using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IAH_PvPAITemplate.Class;
using IAH_PvPAITemplate.Class.AI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IAH_PvPAITemplate
{
    internal class Program
    {
        const string JoinMatchEndpointUrl = "http://localhost:6880/V1/JOIN_MATCH";
        const string MatchKey = "<matchkey>";
        const string AccountKey = "<account-key>";

        public static async Task Main(string[] args)
        {
            await GetMatchData();

            Debugger.WriteLog("Press any Key to Close...");
            Console.ReadKey();
        }

        static async Task GetMatchData()
        {
            HttpClient client = new HttpClient();

            dynamic data = new { matchKey = MatchKey, accountKey = AccountKey };

            // Serialize the data to JSON
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var responseBody = await JoinMatch(client, JoinMatchEndpointUrl, content);
                JObject deserializeObject = JsonConvert.DeserializeObject<dynamic>(responseBody);

                if (deserializeObject == null)
                {
                    await LocalFallback();
                    return;
                }

                JToken matchJoinDataToken = deserializeObject.GetValue("matchJoinData");

                if (matchJoinDataToken == null || string.IsNullOrEmpty(matchJoinDataToken.ToString()))
                {
                    await LocalFallback();
                    return;
                }

                dynamic matchJoinData = JsonConvert.DeserializeObject<dynamic>(matchJoinDataToken.ToString());
                string port = matchJoinData.Value<string>("apiPort");
                string ip = matchJoinData.Value<string>("ip");

                await RunAI($"http://{ip}", int.Parse(port));
            }
            catch (HttpRequestException e)
            {
                await LocalFallback();
            }
        }

        private static async Task LocalFallback()
        {
            // failed to join match, use fallback port, we assume we don't have any match so we just test AI locally.
            await RunAI("http://127.0.0.1", 6800);
        }

        static async Task RunAI(string apiHttp, int port)
        {
            ApiConnector apiConnector = new ApiConnector(apiHttp + ":" + port);

            using (HttpClient httpClient = apiConnector.GetHttpClient())
            {
                Debugger.WriteLog("Starting PvP (HelloWorldAI) Template...");

                MatchInstance instance = new MatchInstance(httpClient);
                instance.AssignAI<HelloWorldAI>();
                instance.AssignRequests();

                await instance.LoopMatchInstance();
            }
        }

        static async Task<string> JoinMatch(HttpClient client, string url, StringContent content)
        {
            var response = await client.PostAsync(url, content);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return "";
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
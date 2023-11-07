using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IAH_PvPAITemplate
{
    internal class Program
    {
        public static HttpClient _httpClient;

        public static List<Entity> entities = new List<Entity>();
        public static MatchStateResponse matchState;

        public static async Task Main(string[] args)
        {
            using (_httpClient = new HttpClient())
            {
                // you can specify different port in the game launch parameters (default is 6800) -> for example: -apiPort 6900
                _httpClient.BaseAddress = new Uri("http://127.0.0.1:6800");
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine("Starting PvP AI Template...");
                await Task.Delay(2000); // wait 2 seconds before attempting call player state.

                while (true)
                {
                    await Requests.GetApiPassword();
                    await Requests.GetEntities();
                    await Requests.GetMatchState();
                    
                    await RunAiLogic();

                    RenderConsole();
                }
            }
        }

        private static void RenderConsole()
        {
            Console.Clear();


            Console.WriteLine("Entities: " + Program.entities.Count);

            Console.WriteLine(
                "API Password: " + Requests._apiPassword + " | RemoteBot IP: " + Requests._remoteBotIp);

            if (matchState != null)
            {
                Console.WriteLine(
                    "Red Team ("+matchState.redTeamName+"): " + matchState.redTeamLIFE + " | Blue Team ("+matchState.blueTeamName+"): " +  matchState.blueTeamLIFE + " | STATUS: " +  matchState.status); 
            }
        }

        public static List<Entity> GetOppositeTeam(string ourIP)
        {
            var filteredEntities = entities.Where(e => e.ip != ourIP).ToList();

            return filteredEntities;
        }

        public static async Task RunAiLogic()
        {
            for (var i = 0; i < Program.entities.Count; i++)
                if (Program.entities[i].ip == Requests._remoteBotIp)
                    await Program.entities[i].RunAi();

            // 0.2 sec delay..
            await Task.Delay(200);
        }
    }
}
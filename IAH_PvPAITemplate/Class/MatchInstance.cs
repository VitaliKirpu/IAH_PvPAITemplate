using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IAH_PvPAITemplate.Class
{
    public class MatchInstance : Requests
    {
        private AIBase assignedAIProgram;
        private MatchStateResponse matchStateResponse;
        private List<Entity> entities = new List<Entity>();

        public MatchInstance(HttpClient httpClient)
        {
            httpClientReference = httpClient;
        }

        public void AssignAI<T>() where T : AIBase, new()
        {
            assignedAIProgram = new T();
        }

        public void AssignRequests()
        {
            assignedAIProgram.SetMatchRequests(this as Requests);
        }

        public async Task<bool> LoopMatchInstance()
        {
            await Task.Delay(1000);

            while (await Loop())
            {
                await LoopMatchInstance();
            }

            return false;
        }

        private async Task<bool> Loop()
        {
            try
            {
                await GetApiPassword();
                entities = await GetEntities();
                matchStateResponse = await GetMatchState();

                RenderConsole();

                for (var i = 0; i < entities.Count; i++)
                    if (entities[i].ip == remoteBotIp)
                        await assignedAIProgram.Run(entities[i], GetTeam(false), GetTeam(true));

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        void RenderConsole()
        {
            Console.Clear();

            Debugger.WriteLog("Entities: " + entities.Count);

            Debugger.WriteLog(
                "API Password: " + apiPassword + " | RemoteBot IP: " + remoteBotIp);

            if (matchStateResponse != null)
            {
                Console.WriteLine(
                    "Red Team (" + matchStateResponse.redTeamName + "): " + matchStateResponse.redTeamLIFE +
                    " | Blue Team (" +
                    matchStateResponse.blueTeamName + "): " + matchStateResponse.blueTeamLIFE + " | STATUS: " +
                    matchStateResponse.status);
            }
        }

        public List<Entity> GetTeam(bool ourTeam)
        {
            var filteredEntities = entities.Where(e => ourTeam ? e.ip == remoteBotIp : e.ip != remoteBotIp).ToList();

            return filteredEntities;
        }
    }
}
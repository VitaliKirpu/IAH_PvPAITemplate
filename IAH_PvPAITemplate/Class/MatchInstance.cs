using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IAH_PvPAITemplate.Class
{
    public class MatchInstance : Requests
    {
        int loopCycles = 0;

        AIBase assignedAIProgram;
        MatchStateResponse matchStateResponse;
        List<Entity> entities = new List<Entity>();

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
            await Task.Delay(100);

            while (await Loop())
                await LoopMatchInstance();
            
            return false;
        }

        private async Task<bool> Loop()
        {
            try
            {
                RenderConsole();

                await GetApiPassword();
                entities = await GetEntities();
                matchStateResponse = await GetMatchState();

                for (var i = 0; i < entities.Count; i++)
                    if (entities[i].initData.ip == remoteBotIp)
                        await assignedAIProgram.Run(entities[i], GetTeam(false), GetTeam(true));

                loopCycles++;

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

            Debugger.WriteLog($"Entities: {entities.Count} | Cycles: {loopCycles}");
            Debugger.WriteLog($"API Password: {apiPassword} | RemoteBot IP: {remoteBotIp}");

            if (matchStateResponse != null)
            {
                string redTeam = $"Red Team ({matchStateResponse.redTeamName}): {matchStateResponse.redTeamLIFE}";
                string blueTeam = $"Blue Team ({matchStateResponse.blueTeamName}): {matchStateResponse.blueTeamLIFE}";
                Console.WriteLine($"{redTeam} | {blueTeam} | STATUS: {matchStateResponse.status}");
            }
        }

        public List<Entity> GetTeam(bool ourTeam)
        {
            var filteredEntities = entities
                .Where(e => ourTeam ? e.initData.ip == remoteBotIp : e.initData.ip != remoteBotIp).ToList();

            return filteredEntities;
        }
    }
}
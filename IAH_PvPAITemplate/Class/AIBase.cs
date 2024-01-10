using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IAH_PvPAITemplate.Interface;
using Newtonsoft.Json;

namespace IAH_PvPAITemplate.Class
{
    public abstract class AIBase : IAIProgram
    {
        private Requests matchInstanceRequestsReference;
        
        public virtual async Task<bool> Run(Entity currentEntity, List<Entity> opposingTeam, List<Entity> friendlyTeam)
        {
            return true;
        }

        public void SetMatchRequests(Requests matchInstanceRequests)
        {
            matchInstanceRequestsReference = matchInstanceRequests;
        }

        public Requests GetMatchRequests()
        {
            return matchInstanceRequestsReference;
        }
    }
}
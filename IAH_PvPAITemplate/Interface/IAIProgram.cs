using System.Collections.Generic;
using System.Threading.Tasks;
using IAH_PvPAITemplate.Class;

namespace IAH_PvPAITemplate.Interface
{
    public interface IAIProgram
    {
        void SetMatchRequests(Requests requests);

        Task<bool> Run(Entity currentEntity, List<Entity> opposingTeam, List<Entity> friendlyTeam);
    }
}
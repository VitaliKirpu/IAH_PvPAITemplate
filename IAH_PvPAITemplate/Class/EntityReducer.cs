using System.Collections.Generic;
using System.Linq;
using IAH_PvPAITemplate.Interface;

namespace IAH_PvPAITemplate.Class
{
    public static class EntityReducer
    {
        public static List<Entity> RemoveCreeps(List<Entity> entities)
        {
            entities = entities
                .Where(entity => !entity.tags.Contains("CREEP") && !entity.tags.Contains("NON-COMBAT"))
                .ToList();
            return entities;
        }
    }
}
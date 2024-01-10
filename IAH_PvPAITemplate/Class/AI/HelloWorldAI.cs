using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using IAH_PvPAITemplate.Interface;

namespace IAH_PvPAITemplate.Class.AI
{
    public class HelloWorldAI : AIBase
    {
        public override async Task<bool> Run(Entity currentEntity, List<Entity> opposingTeam, List<Entity> friendlyTeam)
        {
            //remove bots that have creep flag or non-combat
            opposingTeam = EntityReducer.RemoveCreeps(opposingTeam);

            // we attack closest enemy.
            opposingTeam = opposingTeam.OrderBy(entity => Vector3.Distance(currentEntity.position, entity.position))
                .ToList();

            if (opposingTeam.Count > 0) // battle mode.
            {
                var distance = Vector3.Distance(currentEntity.position, opposingTeam[0].position);

                var blocked = await GetMatchRequests().RayCast(currentEntity.uniqueID, opposingTeam[0].uniqueID);

                if (distance < currentEntity.attackRange && blocked == false)
                {
                    await GetMatchRequests().BotAction(currentEntity.uniqueID, "rotate", opposingTeam[0].position);
                    await GetMatchRequests().BotAction(currentEntity.uniqueID, "stop", "");
                }
                else
                {
                    await GetMatchRequests().BotAction(currentEntity.uniqueID, "move", opposingTeam[0].position);
                    await GetMatchRequests().BotAction(currentEntity.uniqueID, "rotate", opposingTeam[0].position);
                }

                await GetMatchRequests().BotAction(currentEntity.uniqueID, "attack", opposingTeam[0].uniqueID);
            }
            else
            {
                // no enemy bots. reload weapon and spin 360.
                if (currentEntity.ammo != currentEntity.maxAmmo && currentEntity.reloading == false)
                {
                    await GetMatchRequests().BotAction(currentEntity.uniqueID, "reload", "");
                }

                await GetMatchRequests().BotAction(currentEntity.uniqueID, "rotate",
                    currentEntity.position + currentEntity.right);
            }

            /*
             * other actions:  cancelAttack, stop, these don't have actionValue.
             * skill: value int from 0 to 3. bot will use skill if has any.
             */

            return true;
        }
    }
}
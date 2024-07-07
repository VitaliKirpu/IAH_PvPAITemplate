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
            opposingTeam = opposingTeam.OrderBy(entity => Vector3.Distance(currentEntity.vectors.pos, entity.vectors.pos))
                .ToList();

            if (opposingTeam.Count > 0) // battle mode.
            {
                var distance = Vector3.Distance(currentEntity.vectors.pos, opposingTeam[0].vectors.pos);

                var blocked = await GetMatchRequests().RayCast(currentEntity.id, opposingTeam[0].id);

                if (distance < currentEntity.combat.atkRange && blocked == false)
                {
                    await GetMatchRequests().BotAction(currentEntity.id, "rotate", opposingTeam[0].vectors.pos);
                    await GetMatchRequests().BotAction(currentEntity.id, "stop", "");
                }
                else
                {
                    await GetMatchRequests().BotAction(currentEntity.id, "move", opposingTeam[0].vectors.pos);
                    await GetMatchRequests().BotAction(currentEntity.id, "rotate", opposingTeam[0].vectors.pos);
                }

                await GetMatchRequests().BotAction(currentEntity.id, "attack", opposingTeam[0].id);
            }
            else
            {
                // no enemy bots. reload weapon and spin 360.
                if (currentEntity.combat.ammo != currentEntity.combat.maxAmmo && currentEntity.combat.reloading == false)
                {
                    await GetMatchRequests().BotAction(currentEntity.id, "reload", "");
                }

                await GetMatchRequests().BotAction(currentEntity.id, "rotate",
                    currentEntity.vectors.pos + currentEntity.vectors.right);
            }

            /*
             * other actions:  cancelAttack, stop, these don't have actionValue.
             * skill: value int from 0 to 3. bot will use skill if has any.
             */

            return true;
        }
    }
}
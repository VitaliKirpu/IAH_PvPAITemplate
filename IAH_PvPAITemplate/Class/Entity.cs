using System.Collections.Generic;
using System.Numerics;

namespace IAH_PvPAITemplate.Class
{
    public class Entity
    {
        public List<string> equips = new List<string>();
        public List<string> skills = new List<string>();
        public List<string> tags = new List<string>();

        public float attackDelay;
        public float attackRange;
        public Vector3 forward;

        public string targetUniqueID;

        public string team;
        public string teamCustom;
        public string type;
        public string uniqueID;
        public string ip;

        public int ammo;
        public int maxAmmo;

        public int maxHp;
        public int maxSp;
        public int sp;
        public int hp;
        public int xp;
        public int xpNeeded;

        public Vector3 position;

        public bool reloading;
        public Vector3 right;
    }
}
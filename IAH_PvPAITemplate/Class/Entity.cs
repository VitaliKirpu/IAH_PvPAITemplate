using System.Collections.Generic;
using System.Numerics;

namespace IAH_PvPAITemplate.Class
{
    public class Entity
    {
        public string id;
        public TransformedEntityInitials initData;
        public TransformedEntityVitals vitals;
        public TransformedEntityStates states;
        public TransformedEntityVectors vectors;
        public TransformedEntityCombat combat;
    }

    public class TransformedEntityVitals
    {
        public short lvl;
        public short hp;
        public short maxHp;
        public short sp;
        public short maxSp;
        public int xp;
        public int xpReq;
        public short armor;
        public short maxArmor;
        public short kills;
    }

    public class TransformedEntityCombat
    {
        public short ammo;
        public short maxAmmo;
        public float atkRange;
        public bool reloading;
        public float atkDelay;
        public string targetId;
    }
    
    public class TransformedEntityVectors
    {
        public Vector3 pos;
        public Vector3 gotoPos;
        public Vector3 fwd;
        public Vector3 right;
    }

    public class TransformedEntityStates
    {
        public bool lookAtEnabled;
        public Vector3 lookAt;
        public bool playerForcedMovement;
        public short splState;
        public bool shootAt;
        public Vector3 shootAtPos;
        public short state;
    }

    public class TransformedEntityInitials
    {
        public List<string> tags = new List<string>();

        public short scopeId;
        public short ammoId;
        public short barrelId;
        public short charmId;
        public short auraId;
        public short scarfId;
        public short hatId;
        public short bracersId;
        public short jointsId;
        public short maskId;

        public float atkArcAngle;
        public float frontArcAngle;
        public float sideArcAngle;
        public float backArcAngle;

        public List<string> equips = new List<string>();
        public List<string> skills = new List<string>();
        public List<GenericDyeData> dyes = new List<GenericDyeData>();

        public string team;
        public string teamCustom;
        public string type;
        public string ip;
    }

    public class GenericDyeData
    {
        public ushort dyeType;
        public ushort equipItemType;
    }
}
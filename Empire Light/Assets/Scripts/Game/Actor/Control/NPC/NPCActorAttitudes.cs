using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class NPCActorAttitudes
{
    public List<NPCActorAttitudeItem> items = new List<NPCActorAttitudeItem>()
    {
        new NPCActorAttitudeItem(NPCActorAttitudeType.FleeChance, 0),
        new NPCActorAttitudeItem(NPCActorAttitudeType.FireTolerance, 0)
    };

    public NPCActorAttitudeItem this[NPCActorAttitudeType type]
    {
        get { return items.Find((a) => { if (a.type == type) return true; else return false; }); }
    }
}

[Serializable]
public enum NPCActorAttitudeType
{
    FleeChance, // If the attacker is stronger, how big is the chance of this NPC to flee from fight?
    FireTolerance // If a friendly unit is in the way between this NPC and the attacker how willingly does this NPC shoot anyway?
}
[Serializable]
public class NPCActorAttitudeItem
{
    public NPCActorAttitudeType type;
    public int value;

    public NPCActorAttitudeItem()
    {
    }
    public NPCActorAttitudeItem(NPCActorAttitudeType type, int value)
    {
        this.type = type;
        this.value = value;
    }
}

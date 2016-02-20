using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class ActorAttributes
{
    public int initialMaxHealth, initialMovementSpeed, initialPhysicalResistance, initialPoisonResistance,
        initialFootstepHearRange, initialFistDamage;

    private readonly Dictionary<ActorAttributeType, int> attributes;

    public int this[ActorAttributeType type]
    {
        get { return attributes[type]; }
    }

    public int TotalMaxHealth
    {
        get { return this[ActorAttributeType.BaseMaxHealth] + this[ActorAttributeType.BonusMaxHealth]; }
    }
    public int TotalMovementSpeed
    {
        get { return this[ActorAttributeType.BaseMovementSpeed] + this[ActorAttributeType.BonusMovementSpeed]; }
    }
    public int TotalPhysicalResistance
    {
        get { return this[ActorAttributeType.BasePhysicalResistance] + this[ActorAttributeType.BonusPhysicalResistance]; }
    }
    public int TotalPoisonResistance
    {
        get { return this[ActorAttributeType.BasePoisonResistance] + this[ActorAttributeType.BonusPoisonResistance]; }
    }
    public int TotalFistDamage
    {
        get { return this[ActorAttributeType.BasePunchDamage] + this[ActorAttributeType.BonusPunchDamage]; }
    }

    public ActorAttributes()
    {
    }

    public void Initialize()
    {
        attributes.Add(ActorAttributeType.BonusMaxHealth, 0);
        attributes.Add(ActorAttributeType.BonusMovementSpeed, 0);
        attributes.Add(ActorAttributeType.BonusPhysicalResistance, 0);
        attributes.Add(ActorAttributeType.BonusPoisonResistance, 0);
        attributes.Add(ActorAttributeType.BonusFootstepHearRange, 0);
        attributes.Add(ActorAttributeType.BonusPunchDamage, 0);
    }
}

[Serializable]
public enum ActorAttributeType
{
    BaseMaxHealth,
    BonusMaxHealth,

    BaseMovementSpeed,
    BonusMovementSpeed,

    BasePhysicalResistance,
    BonusPhysicalResistance,

    BasePoisonResistance,
    BonusPoisonResistance,

    BaseFootstepHearRange,
    BonusFootstepHearRange,

    BasePunchDamage,
    BonusPunchDamage,

    BasePunchSpeed,
    BonusPunchSpeed
}

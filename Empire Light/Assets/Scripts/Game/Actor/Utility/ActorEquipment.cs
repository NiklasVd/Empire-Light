using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActorArmorEquipment
{
    private List<ActorEquipmentPart> initialArmorParts = new List<ActorEquipmentPart>()
    {
        new ActorEquipmentPart(ActorArmorType.Head),
        new ActorEquipmentPart(ActorArmorType.Body),
        new ActorEquipmentPart(ActorArmorType.LeftArm),
        new ActorEquipmentPart(ActorArmorType.RightArm),
        new ActorEquipmentPart(ActorArmorType.LeftLeg),
        new ActorEquipmentPart(ActorArmorType.RightLeg),
        new ActorEquipmentPart(ActorArmorType.LeftFoot),
        new ActorEquipmentPart(ActorArmorType.RightFoot)
    };
    private readonly Dictionary<ActorArmorType, ActorEquipmentPart> armorParts;

    public ActorEquipmentPart this[ActorArmorType type]
    {
        get { return armorParts[type]; }
    }

    public void EquipItem(ActorArmorType type, ArmorItem item)
    {
        var equipItem = this[type];
        if (equipItem != null)
            equipItem.item = item;
    }

    public void WriteSaveStream(ObjectSaveStream stream)
    {
    }
    public void ReadSaveStream(ObjectSaveStream stream)
    {

    }
}

[Serializable]
public enum ActorArmorType
{
    Head,
    Body,

    LeftArm,
    RightArm,

    LeftLeg,
    RightLeg,

    LeftFoot,
    RightFoot
}
[Serializable]
public class ActorEquipmentPart
{
    public ActorArmorType type;
    public ArmorItem item;

    public ActorEquipmentPart()
    {
    }
    public ActorEquipmentPart(ActorArmorType type, ArmorItem item = null)
    {
        this.type = type;
        this.item = item;
    }
}

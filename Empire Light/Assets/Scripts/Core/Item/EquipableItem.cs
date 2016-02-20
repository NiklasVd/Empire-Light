using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Equipable Item", menuName = "Game Resources/Items/Equipable")]
public class ArmorItem : Item
{
    public Sprite equippedSprite;
    public ActorArmorType armorType;
    // Add status effect for (un)equipping
}

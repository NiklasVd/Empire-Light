using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Game Resources/Consumable")]
public class ConsumableItem : Item
{
    public bool removeOnConsume;

    public virtual void Consume(Actor consumer)
    {

    }
}

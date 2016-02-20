using UnityEngine;
using System.Collections;

//[CreateAssetMenu(fileName = "New Item", menuName = "Game Resources/Item")]
public abstract class Item : ScriptableObject
{
    public int resourceId;

    public string itemName, description;
    public int moneyWorth;
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceHelper : MonoBehaviour
{
    public const string resourcesFolderPath = "Assets/Resources/";
    public static ResourceHelper Instance { get; private set; }

    public List<Ability> availableAbilities;
    public List<Item> avaiableItems;

    public T NewAbility<T>(int resourceId) where T : Ability
    {
        var newAbility = Instantiate<T>((T)availableAbilities.Find((a) =>
        {
            if (a.resourceId == resourceId)
                return true;
            else return false;
        }));
        return newAbility;
    }
    public T NewItem<T>(int resourceId) where T : Item
    {
        var newItem = Instantiate<T>((T)avaiableItems.Find((i) =>
        {
            if (i.resourceId == resourceId)
                return true;
            else return false;
        }));
        return newItem;
    }

    void Awake()
    {
        Instance = this;
    }
}

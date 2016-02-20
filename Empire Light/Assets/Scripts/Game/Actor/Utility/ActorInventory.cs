using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class ActorInventory
{
    public List<Item> initializationItems;
    public List<Item> items;

    public int money;

    public void AddItem(string name)
    {
        AddItem(Resources.Load<Item>("Assets/Resources/Items/" + name));
    }
    public void AddItem(Item cloneBase)
    {
        var item = UnityEngine.Object.Instantiate(cloneBase);
        items.Add(item);
    }
    public void AddExistingItem(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void Initialize()
    {
        initializationItems.ForEach((ii) => { AddItem(ii); });
    }
}

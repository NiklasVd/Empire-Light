using UnityEngine;
using System.Collections;
using System;

public class EntitySaveObject
{
    public int permanentId;
    public ObjectSaveStream saveStream;

    public EntitySaveObject()
    {
    }
    public EntitySaveObject(int permanentId)
    {
        this.permanentId = permanentId;
        this.saveStream = new ObjectSaveStream();
    }
}

public class AbilitySaveState
{
    public int resourceId;
    public float currentCooldown;
}
public class ItemSaveState
{
    public int resourceId;
}
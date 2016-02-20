using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SaveState
{
    public string appVersion;
    public List<EntitySaveObject> entities;

    public SaveState()
    {
    }
    public SaveState(string appVersion, List<EntitySaveObject> entities)
    {
        this.appVersion = appVersion;
        this.entities = entities;
    }
}

// A stream containing all data saved when iterating through all virtual entity save/load stream methods
public class ObjectSaveStream
{
    private Dictionary<string, object> content;

    public ObjectSaveStream()
    {
        content = new Dictionary<string, object>();
    }

    public void Write<T>(string key, T obj)
    {
        content.Add(key, obj);
    }
    public T Read<T>(string key)
    {
        var item = (T)content[key];
        content.Remove(key);

        return item;
    }
    public bool TryRead<T>(string key, out T obj)
    {
        object result = null;
        var worked = content.TryGetValue(key, out result);
        obj = (T)result;

        return worked;
    }
}

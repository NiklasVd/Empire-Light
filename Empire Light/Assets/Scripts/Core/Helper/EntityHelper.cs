using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EntityHelper : MonoBehaviour, IHelper<EntityHelper>
{
    public static EntityHelper Instance { get; private set; }

    public List<Actor> availableActors;
    public List<Entity> allEntities { get; private set; }

    void Awake()
    {
        Instance = this;
        allEntities = new List<Entity>();
    }

    public Actor InstantiateActor(int resourceId)
    {
        var actor = availableActors.Find((a) => a.ResourceID == resourceId);
        var instantiatedActor = Instantiate(actor);

        return instantiatedActor;
    }

    public void AddEntity(Entity entity)
    {
        allEntities.Add(entity);
    }
    public void RemoveEntity(Entity entity)
    {
        allEntities.Remove(entity);
    }
}

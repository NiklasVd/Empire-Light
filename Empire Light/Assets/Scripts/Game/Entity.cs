using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    private int resourceId;
    public int ResourceID
    {
        get { return resourceId; }
    }

    [SerializeField]
    private string entityName;
    public string EntityName
    {
        get { return entityName; }
    }

    protected virtual void Awake()
    {
        EntityHelper.Instance.AddEntity(this);
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
    protected virtual void FixedUpdate()
    {

    }
    protected virtual void OnDestroy()
    {
        EntityHelper.Instance.RemoveEntity(this);
    }

    public virtual void OnWriteSaveStream(ObjectSaveStream stream)
    {
        stream.Write<Vector2>("pos", transform.position);
        stream.Write("rot", transform.rotation.z);
    }
    public virtual void OnReadSaveStream(ObjectSaveStream stream)
    {
        transform.position = stream.Read<Vector2>("pos");

        var currentRot = transform.rotation;
        currentRot.z = stream.Read<float>("rot");
        transform.rotation = currentRot;
    }
}

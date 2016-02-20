using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

public class SaveHelper : MonoBehaviour, IHelper<SaveHelper>
{
    public static SaveHelper Instance { get; private set; }
    public const string defaultSavePath = @"Saves\", saveFileExtension = ".save";

    void Awake()
    {
        Instance = this;
    }

    public void SaveCurrent(string name)
    {
        var saveFilePath = Path.Combine(defaultSavePath, name + saveFileExtension);

        var saveStreams = LoadCurrenEntitySaveObjects();
        var save = new SaveState(Game.version, saveStreams);

        // TODO: Serialize "save" to "saveFilePath"
    }
    public void OpenFrom(string name)
    {
        var saveFilePath = Path.Combine(defaultSavePath, name + saveFileExtension);

        SaveState save = null; // TODO: Deserialize from "saveFilePath"
        ApplySaveStreams(save.entities);
    }

    private List<EntitySaveObject> LoadCurrenEntitySaveObjects()
    {
        var entitySaveObjects = new List<EntitySaveObject>();
        var allEntities = new List<Entity>(EntityHelper.Instance.allEntities); // Copy the Entity.allEntities list because if it changes, this long task will throw an exception/give unexpected behaviour

        foreach (var entity in allEntities)
        {
            var entitySaveObject = new EntitySaveObject(entity.ResourceID);
        }

        return entitySaveObjects;
    }

    private void ApplySaveStreams(List<EntitySaveObject> entitySaveObjects)
    {
        var allEntities = EntityHelper.Instance.allEntities;
        foreach (var entity in allEntities)
        {
            Destroy(entity.gameObject);
        }

        foreach (var entitySaveObject in entitySaveObjects)
        {
            CreateEntityFromSaveObject(entitySaveObject);
        }
    }
    private void CreateEntityFromSaveObject(EntitySaveObject entitySaveObject)
    {
        var actor = EntityHelper.Instance.InstantiateActor(entitySaveObject.permanentId);
        actor.OnReadSaveStream(entitySaveObject.saveStream);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    [SerializeField] private List<ListItemScriptableObject> items;

    private Dictionary<SerializedMechanicObject, MechanicBehaviour> loadedGameObjects = new Dictionary<SerializedMechanicObject, MechanicBehaviour>();
    private int idCounter;

    public void Save(string filename)
    {
        GridGenerator grid = GameObject.Find(Names.Grid).GetComponent<GridGenerator>(); // TODO: Can this be a singleton?
        Vector3Int gridDimension = new Vector3Int(grid.Width, grid.Height, grid.Depth);

        MechanicBehaviour[] mechanicBehaviours = FindObjectsOfType<MechanicBehaviour>();

        List<SerializedMechanicObject> serializedMechanicObjects = new List<SerializedMechanicObject>();

        foreach (MechanicBehaviour behaviour in mechanicBehaviours)
        {
            SerializedMechanicObject smo = behaviour.GetSerializedMechanicObject();
            serializedMechanicObjects.Add(smo);
        }
        
        DataSerializer.SaveGame(filename, gridDimension, serializedMechanicObjects);
    }

    public void Load(string filename)
    {
        GameData data = DataSerializer.LoadGame(filename);

        GridGenerator grid = GameObject.Find(Names.Grid).GetComponent<GridGenerator>(); // TODO: Can this be a singleton?
        grid.Width = data.width;
        grid.Height = data.height;
        grid.Depth = data.depth;

        // Load all objects
        foreach (SerializedMechanicObject serializedMechanicObject in data.mechanicBehaviours)
        {
            MechanicObjectType type;
            Enum.TryParse(serializedMechanicObject.type, out type);

            MechanicBehaviour mb = LoadMechanicalObject(type, serializedMechanicObject);
            mb.ID = serializedMechanicObject.id;
            loadedGameObjects.Add(serializedMechanicObject, mb);
        }
        
        // Call load on all objects
        foreach (KeyValuePair<SerializedMechanicObject,MechanicBehaviour> pair in loadedGameObjects)
        {
            pair.Value.OnLoad(pair.Key);
            pair.Value.OnEnterEditor();
        }
        
        // Default 
        SelectedItemsTracker.Instance.Initialise();
    }

    public List<string> GetCustomLevels()
    {
        List<string> levels = new List<string>();
        
        DirectoryInfo info = new DirectoryInfo(DataSerializer.LevelFolder);

        if (!info.Exists)
        {
            Directory.CreateDirectory(DataSerializer.LevelFolder);
            return new List<string>();
        }
        
        FileInfo[] fileInfos = info.GetFiles();
        foreach (FileInfo fileInfo in fileInfos)
        {
            levels.Add(fileInfo.Name);
        }

        return levels;
    }

    private MechanicBehaviour LoadMechanicalObject(MechanicObjectType type, SerializedMechanicObject smo)
    {
        Transform t = SerializedTransform.SerializedTransformToTransform(smo.serializedTransform);
        GameObject go = Instantiate(GetPrefab(type), t.position, t.rotation);
        return go.GetComponent<MechanicBehaviour>();
    }

    private GameObject GetPrefab(MechanicObjectType type)
    {
        return items.Find(item => item.type == type).prefab;
    }

    public int GetNewID()
    {
        idCounter++;
        return idCounter;
    }

    public Dictionary<SerializedMechanicObject, MechanicBehaviour> LoadedGameObjects
    {
        get => loadedGameObjects;
        set => loadedGameObjects = value;
    }
}

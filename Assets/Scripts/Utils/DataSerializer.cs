using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataSerializer
{
    public static void SaveGame(string filename, Vector3Int gridDimensions, List<SerializedMechanicObject> mechanicBehaviours)
    {
        string path = Application.persistentDataPath + "/" + filename + ".data";
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);

        GameData data = new GameData();
        data.width = gridDimensions.x;
        data.height = gridDimensions.y;
        data.depth = gridDimensions.z;
        data.mechanicBehaviours = mechanicBehaviours.ToArray();

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public static GameData LoadGame(string filename)
    {
        string path = Application.persistentDataPath + "/" + filename + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            GameData data = (GameData) bf.Deserialize(file);
            file.Close();
            Debug.Log("Game data loaded!");

            return data;
        }
        else
            Debug.LogError("There is no save data!");

        return null;
    }
}

// Serialized classes are below
[Serializable]
public class GameData
{
    public int width, height, depth;
    public SerializedMechanicObject[] mechanicBehaviours;
}

[Serializable]
public class SerializedMechanicObject
{
    public int id;
    public String type;
    public SerializedTransform serializedTransform;
    public int[] childIds;
}

[Serializable]
public class SerializedTransform
{
    public float[] position = new float[3];
    public float[] rotation = new float[4];
    public float[] scale = new float[3];

    public static SerializedTransform TransformToSerializedTransform(Transform transform)
    {
        SerializedTransform serializedTransform = new SerializedTransform();

        serializedTransform.position[0] = transform.localPosition.x;
        serializedTransform.position[1] = transform.localPosition.y;
        serializedTransform.position[2] = transform.localPosition.z;

        serializedTransform.rotation[0] = transform.localRotation.w;
        serializedTransform.rotation[1] = transform.localRotation.x;
        serializedTransform.rotation[2] = transform.localRotation.y;
        serializedTransform.rotation[3] = transform.localRotation.z;

        serializedTransform.scale[0] = transform.localScale.x;
        serializedTransform.scale[1] = transform.localScale.y;
        serializedTransform.scale[2] = transform.localScale.z;

        return serializedTransform;
    }

    public static Transform SerializedTransformToTransform(SerializedTransform serializedTransform)
    {
        Transform transform = new GameObject().transform;

        transform.position = new Vector3(serializedTransform.position[0], serializedTransform.position[1],
            serializedTransform.position[2]);
        transform.rotation = new Quaternion(serializedTransform.rotation[1], serializedTransform.rotation[2],
            serializedTransform.rotation[3], serializedTransform.rotation[0]);
        transform.localScale = new Vector3(serializedTransform.scale[0], serializedTransform.scale[1],
            serializedTransform.scale[2]);

        return transform;
    }
}
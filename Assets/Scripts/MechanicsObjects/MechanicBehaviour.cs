using System;
using UnityEngine;

/// <summary>
/// Parent class for all game mechanics related objects
/// The offered methods take care of executing logic based on context
/// This was intended to be implemented as interfaces, but the way Unity works it was easier to use this master class
/// </summary>
public abstract class MechanicBehaviour : MonoBehaviour
{
    protected int id;
    protected MechanicObjectType type;

    protected SerializedMechanicObject mechanicObject = new SerializedMechanicObject();

    public virtual SerializedMechanicObject GetSerializedMechanicObject()
    {
        if (mechanicObject.id == 0)
        {
            id = SaveSystem.Instance.GetNewID();
        }
        
        mechanicObject.id = id;
        mechanicObject.type = type.ToString();
        mechanicObject.serializedTransform = SerializedTransform.TransformToSerializedTransform(transform);

        return mechanicObject;
    }

    public abstract void OnLoad(SerializedMechanicObject smo);
    
    public abstract void OnEnterEditor();
    public abstract void OnEnterPlayMode();

    public int ID
    {
        get => id;
        set => id = value;
    }
}

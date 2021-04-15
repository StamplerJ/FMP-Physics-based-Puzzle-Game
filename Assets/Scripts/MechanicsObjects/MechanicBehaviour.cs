using UnityEngine;

/// <summary>
/// Parent class for all game mechanics related objects
/// The offered methods take care of executing logic based on context
/// This was intended to be implemented as interfaces, but the way Unity works it was easier to use this master class
/// </summary>
public abstract class MechanicBehaviour : MonoBehaviour
{
    public abstract void OnEnterEditor();
    public abstract void OnEnterPlayMode();
}

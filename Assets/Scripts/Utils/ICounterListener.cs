using UnityEngine;

/// <summary>
/// This is supposed to be an interface, but Unity has problems with interfaces inside the inspector.
/// That's why I named this class as an interface.
/// </summary>
public interface ICounterListener
{
    void OnPlusClick();
    void OnMinusClick();
}
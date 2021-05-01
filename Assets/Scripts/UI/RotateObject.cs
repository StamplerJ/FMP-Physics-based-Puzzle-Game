using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private IRotationListener listener;
    
    public void OnRotateObject()
    {
        target.transform.Rotate(new Vector3(0f, 90f, 0f));

        if (listener != null)
        {
            listener.OnRotated();
        }
    }

    public IRotationListener Listener
    {
        get => listener;
        set => listener = value;
    }
}

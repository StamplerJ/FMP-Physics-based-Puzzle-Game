using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private GameObject target;
    
    public void OnRotateObject()
    {
        target.transform.Rotate(new Vector3(0f, 90f, 0f));
    }
}

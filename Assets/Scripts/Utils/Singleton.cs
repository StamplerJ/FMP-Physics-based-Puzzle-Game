using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> 
{
    [SerializeField] private bool isPersistant;

    private static T instance;

    public virtual void Awake() 
    {
        if(isPersistant) 
        {
            if(!instance)
            {
                instance = this as T;
            }
            else 
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            instance = this as T;
        }
    }

    public static T Instance => instance;
}
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] private Transform rocketSpawnPoint;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private bool spawnRocket = true;

    private GameObject rocket;
    
    private void Start()
    {
        if (spawnRocket)
        {
            rocket = Instantiate(rocketPrefab, rocketSpawnPoint.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 100f));
            
            CameraController.Instance.SetupRocket(rocket);
        }
    }
}
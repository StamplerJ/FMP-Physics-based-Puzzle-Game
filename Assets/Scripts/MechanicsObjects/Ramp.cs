using UnityEngine;

public class Ramp : MechanicBehaviour
{
    [SerializeField] private Transform rocketSpawnPoint;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private bool spawnRocket = true;

    private GameObject rocket;

    public override void OnEnterEditor()
    {
        //noop
    }

    public override void OnEnterPlayMode()
    {
        rocket = Instantiate(rocketPrefab, rocketSpawnPoint.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 100f));
            
        CameraController.Instance.SetupRocket(rocket);
    }
}
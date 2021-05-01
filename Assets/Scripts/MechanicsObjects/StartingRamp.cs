using System;
using UnityEngine;

public class StartingRamp : MechanicBehaviour
{
    [SerializeField] private Transform rocketSpawnPoint;
    [SerializeField] private GameObject rocketPrefab;

    private GameObject rocket;

    private void Awake()
    {
        type = MechanicObjectType.StartRamp;
    }

    public override void OnLoad(SerializedMechanicObject smo)
    {
        //noop
    }

    public override void OnEnterEditor()
    {
        if (rocket != null)
        {
            Destroy(rocket);
        }
    }

    public override void OnEnterPlayMode()
    {
        rocket = Instantiate(rocketPrefab, rocketSpawnPoint.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 100f));
            
        CameraController.Instance.SetupRocket(rocket);
    }
    
    
}
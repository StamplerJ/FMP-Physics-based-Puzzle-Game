using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private CinemachineVirtualCamera topDownCamera;
    [SerializeField] private CinemachineVirtualCamera thirdPersonCamera;

    private Camera camera;

    public override void Awake()
    {
        base.Awake();

        camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            ShowTopDown();
        
        if(Input.GetKeyDown(KeyCode.D))
            ShowThirdPerson();
    }

    public void SetupRocket(GameObject rocket)
    {
        thirdPersonCamera.Follow = rocket.transform;
        thirdPersonCamera.LookAt = rocket.transform;
    }
    
    public void ShowTopDown()
    {
        topDownCamera.Priority = 1;
        thirdPersonCamera.Priority = 0;
    }
    
    public void ShowThirdPerson()
    {
        topDownCamera.Priority = 0;
        thirdPersonCamera.Priority = 1;
    }

    public Camera GetCamera()
    {
        return camera;
    }
}

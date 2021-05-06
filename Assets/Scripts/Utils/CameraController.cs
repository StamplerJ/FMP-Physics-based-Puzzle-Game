using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private CinemachineVirtualCamera topDownCamera;
    [SerializeField] private CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField] private CinemachineVirtualCamera sidewaysCamera;

    private new Camera camera;

    private bool isSideways;

    public override void Awake()
    {
        base.Awake();

        camera = GetComponentInChildren<Camera>();
    }

    public void SetupRocket(GameObject rocket)
    {
        thirdPersonCamera.Follow = rocket.transform;
        thirdPersonCamera.LookAt = rocket.transform;
    }
    
    public void ShowTopDown()
    {
        topDownCamera.Priority = 2;
        sidewaysCamera.Priority = 1;
        thirdPersonCamera.Priority = 0;
    }
    
    public void ShowThirdPerson()
    {
        topDownCamera.Priority = 0;
        sidewaysCamera.Priority = 1;
        thirdPersonCamera.Priority = 2;
    }

    public void ShowSideways()
    {
        topDownCamera.Priority = 1;
        sidewaysCamera.Priority = 2;
        thirdPersonCamera.Priority = 0;
    }

    public void ToggleCamera()
    {
        int temp = topDownCamera.Priority;
        topDownCamera.Priority = sidewaysCamera.Priority;
        sidewaysCamera.Priority = temp;
    }

    public Camera GetCamera()
    {
        return camera;
    }
}

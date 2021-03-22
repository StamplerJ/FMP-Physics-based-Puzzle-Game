using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ensure that the canvas always faces the camera
/// </summary>
[ExecuteInEditMode]
public class FaceCamera : MonoBehaviour
{
    public Camera camera;

    void Start()
    {
        camera = CameraController.Instance.GetCamera();
    }

    void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }
}
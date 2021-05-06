using System;
using UnityEngine;

public class StartingRamp : MechanicBehaviour, IRotationListener
{
    [SerializeField] private Transform rocketSpawnPoint;
    [SerializeField] private GameObject rocketPrefab;

    private SnapToGrid snapToGrid;
    private Vector3 orgSnapOffset;
    
    private GameObject mesh;
    private GameObject rocket;

    private void Awake()
    {
        type = MechanicObjectType.StartRamp;
        mesh = transform.Find(Names.Walls).gameObject;

        snapToGrid = GetComponent<SnapToGrid>();
        orgSnapOffset = snapToGrid.Offset;
        GetComponentInChildren<RotateObject>().Listener = this;
    }

    public override void OnLoad(SerializedMechanicObject smo)
    {
        isEditable = false;
    }

    public override void OnEnterEditor()
    {
        if (rocket != null)
        {
            rocket.GetComponent<MechanicBehaviour>().OnEnterEditor();
            Destroy(rocket);
        }
    }

    public override void OnEnterPlayMode()
    {
        rocket = Instantiate(rocketPrefab, rocketSpawnPoint.transform.position, mesh.transform.rotation * Quaternion.Euler(0f, 0f, 100f));
        rocket.GetComponent<MechanicBehaviour>().OnEnterPlayMode();
        
        CameraController.Instance.SetupRocket(rocket);
    }

    public void OnRotated()
    {
        float yAngle = mesh.transform.rotation.eulerAngles.y;
        if (Math.Abs(yAngle) < 5f)
        {
            Vector3 off = snapToGrid.Offset;
            off.x = orgSnapOffset.x;
            off.z = orgSnapOffset.z;
            snapToGrid.Offset = off;
            
            snapToGrid.CorrectPosition();
        }
        else if (Math.Abs(yAngle - 180f) < 5f)
        {
            Vector3 off = snapToGrid.Offset;
            off.x = -orgSnapOffset.x;
            off.z = orgSnapOffset.z;
            snapToGrid.Offset = off;
            
            snapToGrid.CorrectPosition();
        }
        else if (Math.Abs(yAngle - 90f) < 5f)
        {
            Vector3 off = snapToGrid.Offset;
            off.x = orgSnapOffset.z;
            off.z = orgSnapOffset.x;
            snapToGrid.Offset = off;
            
            snapToGrid.CorrectPosition();
        }
        else if (Math.Abs(yAngle - 270f) < 5f)
        {
            Vector3 off = snapToGrid.Offset;
            off.x = -orgSnapOffset.z;
            off.z = -orgSnapOffset.x;
            snapToGrid.Offset = off;
            
            snapToGrid.CorrectPosition();
        }
    }
}
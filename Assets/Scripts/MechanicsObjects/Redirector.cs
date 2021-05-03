using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Redirector : MechanicBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private GameObject mesh;

    private void Awake()
    {
        type = MechanicObjectType.Redirector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
            TurnRocket(other.gameObject);
        }
    }
    
    private void TurnRocket(GameObject rocket)
    {
        Vector3 centerPosition = GetComponent<SnapToGrid>().GetCenterPosition();
        rocket.transform.position = new Vector3(centerPosition.x, rocket.transform.position.y, centerPosition.z);
    
        Vector3 tempDirection = mesh.transform.rotation * direction;
        Vector3 rot = new Vector3(rocket.transform.eulerAngles.x, Mathf.Atan2(tempDirection.x, tempDirection.z) * Mathf.Rad2Deg - 90f, rocket.transform.eulerAngles.z);
        rocket.transform.eulerAngles = rot;
        
        Rigidbody rb = rocket.GetComponent<Rigidbody>();
        
        Vector3 velocity = rb.velocity;
        velocity.x = Math.Abs(velocity.x) < 1f ? 0f : velocity.x;
        velocity.y = Math.Abs(velocity.y) < 1f ? 0f : velocity.y;
        velocity.z = Math.Abs(velocity.z) < 1f ? 0f : velocity.z;
        
        RocketEngine engine = rocket.GetComponent<RocketEngine>();
        
        Vector3 dir = engine.GetDirection();
        dir.x = Math.Abs(dir.x) < 0.9f ? 0f : dir.x;
        dir.y = Math.Abs(dir.y) < 0.9f ? 0f : dir.y;
        dir.z = Math.Abs(dir.z) < 0.9f ? 0f : dir.z;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        rb.AddForce(dir * velocity.magnitude, ForceMode.VelocityChange);
    }
    
    public override void OnLoad(SerializedMechanicObject smo)
    {
        //noop
    }

    public override void OnEnterEditor()
    {
        //noop
    }

    public override void OnEnterPlayMode()
    {
        //noop
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}

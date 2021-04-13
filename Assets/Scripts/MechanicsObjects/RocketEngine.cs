using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MechanicBehaviour
{
    private new Rigidbody rigidbody;

    [SerializeField] private float speed = 50f;
    [SerializeField] private GameObject engine;
    [SerializeField] private GameObject front;

    private float deltaPos;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddForce(GetDirection() * speed);
        }

        // Freeze rotation manually
        transform.eulerAngles = new Vector3(0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public Vector3 GetDirection()
    {
        return (front.transform.position - engine.transform.position).normalized;
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
        Gizmos.DrawLine(front.transform.position, front.transform.position + GetDirection());
    }
}

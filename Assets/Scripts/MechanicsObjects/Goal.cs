using System;
using UnityEngine;

public class Goal : MechanicBehaviour
{
    private FloatingItem floatingItem;

    private void Awake()
    {
        type = MechanicObjectType.Goal;
        
        floatingItem = GetComponentInChildren<FloatingItem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Goal reached!");
    }

    public override void OnLoad(SerializedMechanicObject smo)
    {
        //noop
    }
    
    public override void OnEnterEditor()
    {
        floatingItem.enabled = true;
    }

    public override void OnEnterPlayMode()
    {
        floatingItem.enabled = true;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MechanicBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("Goal reached!");
    }
    
    public override void OnEnterEditor()
    {
        GetComponentInChildren<FloatingItem>().enabled = true;
    }

    public override void OnEnterPlayMode()
    {
        GetComponentInChildren<FloatingItem>().enabled = true;
    }
}

using UnityEngine;

public class Goal : MechanicBehaviour
{
    private FloatingItem floatingItem;

    private void OnTriggerEnter(Collider other)
    {
        print("Goal reached!");
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
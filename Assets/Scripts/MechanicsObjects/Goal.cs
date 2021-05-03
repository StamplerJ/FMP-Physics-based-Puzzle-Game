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
        if (!LevelTracker.Instance.IsLevelFinished)
        {
            LevelTracker.Instance.IsLevelFinished = true;
            MenuVictory.Instance.ShowMenu();   
        }
    }

    public override void OnLoad(SerializedMechanicObject smo)
    {
        isEditable = false;
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
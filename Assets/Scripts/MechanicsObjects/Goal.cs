using UnityEngine;

public class Goal : MechanicBehaviour
{
    private FloatingItem floatingItem;
    private AudioSource audioSource;
    
    private void Awake()
    {
        type = MechanicObjectType.Goal;
        
        floatingItem = GetComponentInChildren<FloatingItem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
            if (!LevelTracker.Instance.IsLevelFinished)
            {
                LevelTracker.Instance.IsLevelFinished = true;
                FuelBar.Instance.Hide();
                MenuVictory.Instance.ShowMenu();

                other.gameObject.GetComponent<RocketEngine>().Fuel = 0f;
                
                audioSource.Play();
            }
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
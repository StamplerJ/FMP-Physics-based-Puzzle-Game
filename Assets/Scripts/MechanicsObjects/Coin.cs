using System;
using UnityEngine;

public class Coin : MechanicBehaviour
{
    [SerializeField] private GameObject mesh;
    
    private FloatingItem floatingItem;
    private AudioSource audioSource;
    private BoxCollider hitBox;

    private bool isInEditor;
    
    private void Awake()
    {
        type = MechanicObjectType.Coin;
        
        floatingItem = GetComponentInChildren<FloatingItem>();
        audioSource = GameObject.Find(Names.CoinAudioSource)?.GetComponent<AudioSource>();
        hitBox = GetComponent<BoxCollider>();

        isInEditor = GameObject.Find(Names.LevelEditorCanvas) != null; // TODO: Find a better solution
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
            LevelTracker.Instance.OnCoinPickup();
            PlayPickupAnimation();
        }
    }

    private void PlayPickupAnimation()
    {
        audioSource.Play();
        
        if (isInEditor)
        {
            mesh.SetActive(false);
            hitBox.enabled = false;
        }
        else
        {
            Destroy(gameObject, 0.1f);   
        }
    }

    public override void OnLoad(SerializedMechanicObject smo)
    {
        //noop
    }

    public override void OnEnterEditor()
    {
        mesh.SetActive(true);
        hitBox.enabled = true;
        
        floatingItem.enabled = true;
    }

    public override void OnEnterPlayMode()
    {
        floatingItem.enabled = true;
    }
}

using System;
using UnityEngine;

public class Coin : MechanicBehaviour
{
    private FloatingItem floatingItem;

    private void Awake()
    {
        type = MechanicObjectType.Coin;
        
        floatingItem = GetComponentInChildren<FloatingItem>();
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
        // TODO: Play pickup sound
        Destroy(gameObject, 0.1f);
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

using UnityEngine;

public class Coin : MechanicBehaviour
{
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

    public override void OnEnterEditor()
    {
        GetComponentInChildren<FloatingItem>().enabled = true;
    }

    public override void OnEnterPlayMode()
    {
        GetComponentInChildren<FloatingItem>().enabled = true;
    }
}

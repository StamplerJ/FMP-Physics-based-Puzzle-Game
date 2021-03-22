using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
            print("Coin picked up");
            LevelTracker.Instance.OnCoinPickup();
            PlayPickupAnimation();
        }
    }

    private void PlayPickupAnimation()
    {
        // TODO: Play pickup sound
        Destroy(gameObject, 0.1f);
    }
}

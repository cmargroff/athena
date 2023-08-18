using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollectorBehavior : AthenaMonoBehavior
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var pickup = other.GetComponent<PickupBehavior>();
        if (pickup != null)
        {
            pickup.Pickup();
        }
    }
}
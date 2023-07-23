using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollectionBehavior : AthenaMonoBehavior
{

  private void OnTriggerStay2D(Collider2D other)
  {
    var pickup = other.GetComponent<PickupBehavior>();
    if (pickup == null) return;
    _gameManager.CollectPickup(pickup);
  }
}
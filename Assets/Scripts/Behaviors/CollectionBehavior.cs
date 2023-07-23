using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollectionBehavior : AthenaMonoBehavior
{

  void OnTriggerStay2D(Collider2D other)
  {
    Debug.Log("Triggered");
    var pickup = other.GetComponent<PickupBehavior>();
    // decide what to do with the pickup
    other.gameObject.SetActive(false);
  }
}
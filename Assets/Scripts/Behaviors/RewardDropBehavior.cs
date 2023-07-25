using UnityEngine;
using System.Collections.Generic;

public class RewardDropBehavior : AthenaMonoBehavior
{
  [SerializeField]
  public List<Reward> Rewards = new();

  public void DropRewards()
  {
    // spawn rewards from list
    foreach (var reward in Rewards)
    {
            if (Random.value < reward.Chance)
            {
                var pickup = _gameManager.Pool.GetPooledObject(reward.Pickup.PreFab, transform.position, Quaternion.identity);
                var pickupBehavior = pickup.GetComponent<PickupBehavior>();
                pickupBehavior.Color = reward.Pickup.Color;
                pickupBehavior.Name = reward.Pickup.name;
            }
    }
  }
}
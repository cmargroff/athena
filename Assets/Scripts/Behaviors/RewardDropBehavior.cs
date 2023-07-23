using UnityEngine;
using System.Collections.Generic;

public class RewardDropBehavior : AthenaMonoBehavior
{
  [SerializeField]
  public List<Reward> Rewards = new List<Reward>();

  public void DropRewards()
  {
    // spawn rewards from list
    foreach (var reward in Rewards)
    {
      if(UnityEngine.Random.value > reward.Chance) continue;
      _gameManager.SpawnPickup(reward.Type, 1, transform.position, Quaternion.identity);
    }
  }
}
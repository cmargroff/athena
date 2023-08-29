using System.Collections.Generic;
using UnityEngine;

public class RewardDropBehavior : AthenaMonoBehavior
{
    [SerializeField]
    public List<Reward> Rewards = new();
    private static readonly int ColorProperty = Shader.PropertyToID("_Color");
    public void DropRewards()
    {
        // spawn rewards from list
        foreach (var reward in Rewards)
        {
            if (Random.value < reward.Chance)
            {
                var pickup = _gameManager.Pool.GetPooledObject(reward.Pickup.PreFab, transform.position, Quaternion.identity);
                pickup.transform.localScale = Vector3.one * 0.5f;
                var pickupBehavior = pickup.GetComponent<PickupBehavior>();
                pickupBehavior.Color = reward.Pickup.Color;
                pickupBehavior.GetComponentInChildren<Renderer>().material.SetColor(ColorProperty, reward.Pickup.Color);
                pickupBehavior.Type = reward.Pickup.Type;
            }
        }
    }
}
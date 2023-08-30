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
            int drops = Rand(reward.Chance);
            //Debug.Log($"chance: {reward.Chance} drops:{drops}");
            for (var i=0 ;i<= drops; i++)
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
    private static int Rand(float input)
    {
        return (int)Mathf.Round((float)(input + Random.value - .5 + Random.value * input - input / 2));

    }

}
using System;
using UnityEngine;
[Serializable]
public class Reward
{
    public PickupSO Pickup;
    [Range(0f, 1f)]
    public float Chance = 0;
}
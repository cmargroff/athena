using System;
using UnityEngine;
[Serializable]
public class Reward {
  public PickupType Type = PickupType.Coin;
    [Range(0f, 1f)]
    public float Chance = 0;
}
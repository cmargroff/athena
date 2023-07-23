using UnityEngine;

public enum PickupType
{
  Health,
  Ammo,
  Weapon,
  Coin,
  Tech,
  Powerup
}

[RequireComponent(typeof(Collider2D))]
public class PickupBehavior : AthenaMonoBehavior
{
  public int Amount = 1;
  public PickupType Type = PickupType.Coin;
  
  private void OnEnable() {
    var Material = GetComponentInChildren<SpriteRenderer>().material;
    // clean up this switch statement to something more readable
    switch (Type)
    {
      case PickupType.Health:
        Material.color = Color.green;
        break;
      case PickupType.Ammo:
        Material.color = Color.blue;
        break;
      case PickupType.Weapon:
        Material.color = Color.red;
        break;
      case PickupType.Coin:
        Material.color = Color.yellow;
        break;
      case PickupType.Tech:
        Material.color = Color.cyan;
        break;
      case PickupType.Powerup:
        Material.color = Color.blue;
        break;
    }
  }
}
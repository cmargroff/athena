using UnityEngine;

public enum PickupTypeEnum
{
    Ammo,

    Coin,
    Health,
    PowerUp,
    Tech,
    Weapon
}
[CreateAssetMenu(fileName = "Tile", menuName = "athena/Pickup", order = 0)]
public class PickupSO : ScriptableObject
{
    public PickupTypeEnum Type;
    public string Description;
    public Color Color;
    public GameObject PreFab;
}
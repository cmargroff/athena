using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "athena/Pickup", order = 0)]
public class PickupSO : ScriptableObject
{
    public string FriendlyName;
    public string Description;
    public Color Color;
    public GameObject PreFab;
}
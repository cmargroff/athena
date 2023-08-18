using UnityEngine;


public class BaseShopItemSO : ScriptableObject
{
    public string FriendlyName;
    [Multiline]
    public string Description;
    public int Cost;
    public Texture2D Icon;
    public Color Color;
    public int NumberInStore = 1;
}


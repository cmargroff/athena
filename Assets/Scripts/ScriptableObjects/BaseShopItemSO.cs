using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


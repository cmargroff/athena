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
    public Sprite Icon;
}


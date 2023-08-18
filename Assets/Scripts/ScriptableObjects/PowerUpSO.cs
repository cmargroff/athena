using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "athena/PowerUp", order = 0)]
public class PowerUpSO : BaseShopItemSO
{


    [Range(0f, 10)]
    public float Damage;
    [Range(0f, 10)]
    public float Knockback;
    [Range(0f, 10)]
    public float Speed;
    [Range(0f, 10)]
    public float Armor;
    //public float AttackSpeed = 1f;
    //public float AttackDuration = 1f;
    [Range(0f, 10)]
    public float AttackFrequency;
}
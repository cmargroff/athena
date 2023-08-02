using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PowerUpSO : ScriptableObject
{
    [Range(0.9f, 10)]
    public float Damage = 1f;
    [Range(0.9f, 10)]
    public float Knockback = 1f;
    [Range(0.9f, 10)]
    public float Speed = 1f;
    [Range(0.9f, 10)]
    public float Armor = 1f;
    //public float AttackSpeed = 1f;
    //public float AttackDuration = 1f;
    [Range(0.9f, 10)]
    public float AttackFrequency = 1f;
}


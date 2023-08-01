using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponSO", menuName = "athena/Weapon", order = 0)]
public class WeaponSO : ScriptableObject
{
    public string FriendlyName;
    [Multiline]
    public string Description;
    public int Cost;
    public Sprite Icon;

    public Color Color;
    public int Damage;
    public float Pierce;
    public MinMax Speed;
    public int Range;
    public MinMax Duration;
    public int Scatter;
    public int Number;
    public float Rate;
    public float Knockback;
    public float Scale;

    public FireAngleEnum FireAngle = FireAngleEnum.Random;
    public BehaviorEnum Behavior= BehaviorEnum.Weapon;

    public GameObject Bullet;
    public bool ParentedToPlayer;
    public bool Orbit;

    public GameSound FireSound;

    public GameSound HitSound;


    public enum FireAngleEnum
    {
        Random = 0,
        MovementDirection = 1,
        ClosestEnemy = 2
    }

    public enum BehaviorEnum
    {
        Weapon,
        Orbital
    }


}

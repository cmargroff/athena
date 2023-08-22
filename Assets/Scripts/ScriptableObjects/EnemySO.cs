using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Enemy", menuName = "athena/Enemy", order = 0)]
public class EnemySO : ScriptableObject
{
    public string FriendlyName;
    public float Speed;
    public int Health;
    public int Weight;
    public int Friction;
    public int TouchDamage;   
    public GameObject Prefab;
    public List<Reward> Rewards;

    public WeaponSO Weapon;
}
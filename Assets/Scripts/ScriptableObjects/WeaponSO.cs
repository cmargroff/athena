using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponSO", menuName = "athena/Weapon", order = 0)]
public class WeaponSO : ScriptableObject
{
    public string FriendlyName;
    public Sprite Icon;
    public Color Color;
    public int Damage;
    public MinMax Speed;
    public int Range;
    public MinMax Duration;
    public int Scatter;
    public int Number;
    public float Rate;

    public GameObject Bullet;
    public bool ParentedToPlayer;
}

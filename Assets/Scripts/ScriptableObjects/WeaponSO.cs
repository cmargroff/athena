using Assets.Scripts.Utils;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponSO", menuName = "athena/Weapon", order = 0)]
public class WeaponSO : BaseShopItemSO
{
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
    //[Dropdown("GetBehaviorValues")]
    //public string Behavior;
    public BehaviorEnum Behavior = BehaviorEnum.WeaponBehavior;
    public GameObject Bullet;
    public bool ParentedToPlayer;
    public bool Orbit;
    public GameSound FireSound;
    public GameSound HitSound;
    public LayerMask Enemies;
    public enum FireAngleEnum
    {
        Random = 0,
        MovementDirection = 1,
        ClosestEnemy = 2
    }
    public enum BehaviorEnum
    {
        WeaponBehavior,
        OrbitalBehavior
    }
    //[UsedImplicitly]
    //private DropdownList<string> GetBehaviorValues()
    //{
    //    return new DropdownList<string>()
    //    {
    //        { "Weapon",   nameof(WeaponBehavior) },
    //        { "Orbital",  nameof(OrbitalBehavior)},
    //    };
    //}
}

using Assets.Scripts.Utils;
using System;
using UnityEngine;

public abstract  class BaseBulletManager
{
    public enum BulletTypesEnum
    { 
        Standard,Orbiting
    }

    public abstract void CreateBullet(GameObject parent, WeaponSO weaponConfig, PoolBehavior pool, AudioSource audioSource);

    public static BaseBulletManager GetBulletManager(BulletTypesEnum bulletType)
    {
        switch (bulletType)
        {
            case BulletTypesEnum.Standard:
                return new StandardBulletManager();
            case BulletTypesEnum.Orbiting:
                return new OrbitingBulletManager();
            default:
               throw new ArgumentOutOfRangeException();
        }
    }
    
}
public class StandardBulletManager : BaseBulletManager
{
    public override void CreateBullet(GameObject parent,  WeaponSO weaponConfig, PoolBehavior pool, AudioSource audioSource )
    {
     
        var fireAngle = UnityEngine.Random.insideUnitCircle.normalized;
        for (var i = 0; i <= weaponConfig.Number; i++)
        {

            var random = UnityEngine.Random.value * weaponConfig.Scatter;

            var newAngle = fireAngle.Rotate(random - weaponConfig.Scatter);

            Create(parent, newAngle, weaponConfig, pool);
        }
        if (weaponConfig.FireSound != null)
        {
            weaponConfig.FireSound.Play(audioSource);
        }


    }
    public  void Create(GameObject parent, Vector2 fireAngle, WeaponSO weaponConfig, PoolBehavior pool)
    {
        float angle = Mathf.Atan2(fireAngle.y, fireAngle.x);

        var bullet = pool.GetPooledObject(weaponConfig.Bullet, parent.transform.position, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg));
        if (weaponConfig.ParentedToPlayer)
        {
            bullet.transform.SetParent(parent.transform, true);
        }
        bullet.transform.localScale = Vector3.one * weaponConfig.Scale;
        var damaging = bullet.GetComponent<DamagingBehavior>();
        damaging.Damage = weaponConfig.Damage;
        damaging.Knockback = weaponConfig.Knockback;
        damaging.HitSound = weaponConfig.HitSound;
        damaging.Pierce = weaponConfig.Pierce;
        //var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        var behavior = bullet.GetComponent<BulletBehavior>();
        behavior.Speed = weaponConfig.Speed.GetRandomValue();
        behavior.MoveAngle = fireAngle;
        behavior.Duration = weaponConfig.Duration.GetRandomValue();
    }
}

public class OrbitingBulletManager : BaseBulletManager
{
    public override void CreateBullet(GameObject parent, WeaponSO weaponConfig, PoolBehavior pool, AudioSource audioSource)
    {
        for (var i = 0; i < weaponConfig.Number; i++)
        {
            var orbit = pool.GetPooledObject(weaponConfig.Bullet, parent.transform.position, Quaternion.identity);
            orbit.transform.parent = parent.transform;
            orbit.transform.localScale = Vector3.one * weaponConfig.Scale;
            var damaging = orbit.GetComponent<DamagingBehavior>();
            damaging.Damage = weaponConfig.Damage;
            damaging.Knockback = weaponConfig.Knockback;
            damaging.HitSound = weaponConfig.HitSound;
            damaging.Pierce = weaponConfig.Pierce;
            var behavior = orbit.GetComponent<BulletBehavior>();
            behavior.Duration = 1f / (float)weaponConfig.Speed.min;
            behavior.IsLooped = weaponConfig.Duration.min == 0;
            behavior.TimeOffset = i / (float)weaponConfig.Number * behavior.Duration;
            float offset = (i / (float)weaponConfig.Number);

            behavior.customTravelMode = (t) =>
            {
                var angle = Mathf.Lerp(0, Mathf.PI * 2, t);
                var x = Mathf.Cos(angle) * weaponConfig.Range;
                var y = Mathf.Sin(angle) * weaponConfig.Range;
                orbit.transform.localPosition = new Vector3(x, y, 0f);
            };
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OrbitalBehavior : BaseWeaponBehavior, IAlive
{
    


    protected override void Start()
    {
        base.Start();

        for (var i = 0; i < WeaponConfig.Number; i++)
        {
            var orbit = _gameManager.Pool.GetPooledObject(WeaponConfig.Bullet, transform.position, Quaternion.identity);
            orbit.transform.parent = transform;
            orbit.transform.localScale = Vector3.one * WeaponConfig.Scale;
            var damaging = orbit.GetComponent<DamagingBehavior>();
            damaging.Damage = WeaponConfig.Damage;
            damaging.Knockback = WeaponConfig.Knockback;
            var behavior = orbit.GetComponent<BulletBehavior>();
            behavior.Duration = 1f / (float)WeaponConfig.Speed.min;
            behavior.IsLooped = WeaponConfig.Duration.min == 0;
            behavior.TimeOffset = i / (float)WeaponConfig.Number * behavior.Duration;
            float offset = (i / (float)WeaponConfig.Number);

            behavior.customTravelMode = (t) =>
            {
                var angle = Mathf.Lerp(0, Mathf.PI * 2, t);
                var x = Mathf.Cos(angle) * WeaponConfig.Range;
                var y = Mathf.Sin(angle) * WeaponConfig.Range;
                orbit.transform.localPosition = new Vector3(x, y, 0f);
            };
        }

    }
}

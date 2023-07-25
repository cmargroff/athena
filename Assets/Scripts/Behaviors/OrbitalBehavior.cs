using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalBehavior : AthenaMonoBehavior, IAlive
{
    [SerializeField]
    private WeaponSO _weaponConfig;


    protected override void Start()
    {
        base.Start();

        for (var i = 0; i < _weaponConfig.Number; i++)
        {
            var orbit = _gameManager.Pool.GetPooledObject(_weaponConfig.Bullet, transform.position, Quaternion.identity);
            orbit.transform.parent = transform;
            orbit.transform.localScale = Vector3.one * _weaponConfig.Scale;
            var damaging = orbit.GetComponent<DamagingBehavior>();
            damaging.Damage = _weaponConfig.Damage;
            damaging.Knockback = _weaponConfig.Knockback;
            var behavior = orbit.GetComponent<BulletBehavior>();
            behavior.Duration = 1f / (float)_weaponConfig.Speed.min;
            behavior.IsLooped = _weaponConfig.Duration.min == 0;
            behavior.TimeOffset = i / (float)_weaponConfig.Number * behavior.Duration;
            float offset = (i / (float)_weaponConfig.Number);

            behavior.customTravelMode = (t) =>
            {
                var angle = Mathf.Lerp(0, Mathf.PI * 2, t);
                var x = Mathf.Cos(angle) * _weaponConfig.Range;
                var y = Mathf.Sin(angle) * _weaponConfig.Range;
                orbit.transform.localPosition = new Vector3(x, y, 0f);
            };
        }

    }
}

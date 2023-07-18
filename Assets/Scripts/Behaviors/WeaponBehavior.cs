using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : AthenaMonoBehavior
{
    [SerializeField]
    private WeaponSO _weaponConfig;

    private GameManagerBehavior.TimedEvent _timedEvent;
    protected override void Start()
    {
        base.Start();
        SafeAssigned(_weaponConfig);


    }

    protected override void PausibleFixedUpdate()
    {
        SetFireRate(_weaponConfig.Rate);
    }
    public void SetFireRate(float fireRate)
    {
        if (_timedEvent == null)
        {
            _timedEvent = _gameManager.AddTimedEvent(fireRate, () =>
            {
                var fireAngle= Random.insideUnitCircle.normalized;
                float angle = Mathf.Atan2(fireAngle.y, fireAngle.x);

                var bullet = _gameManager.Pool.GetPooledObject(_weaponConfig.Bullet, transform.position, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg));
                if (_weaponConfig.ParentedToPlayer)
                { 
                    bullet.transform.SetParent(transform, true);
                }
                var damaging = bullet.GetComponent<DamagingBehavior>();
                damaging.Damage=_weaponConfig.Damage;
                //var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                var behavior = bullet.GetComponent<BulletBehavior>();
                behavior.Speed = _weaponConfig.Speed;
                behavior.MoveAngle = fireAngle;
                behavior.Duration=_weaponConfig.Duration;

            }, gameObject);
        }
        else
        {
            _timedEvent.SetFramesInSeconds(fireRate);
        }
    }
}

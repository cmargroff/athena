using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class WeaponBehavior : AthenaMonoBehavior
{
    [SerializeField]
    private WeaponSO _weaponConfig;

    private GameManagerBehavior.TimedEvent _timedEvent;
    
    private AudioSource _audioSource;

    protected override void Start()
    {
        base.Start();
        SafeAssigned(_weaponConfig);
        if (_weaponConfig.Orbit)
        {
            CreateOrbits();
        }
        else
        {
            SetFireRate(_weaponConfig.Rate);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    protected override void PausibleFixedUpdate()
    {

    }
 
    public void SetFireRate(float fireRate)
    {
        if (_timedEvent == null)
        {
            _timedEvent = _gameManager.AddTimedEvent(fireRate, () =>
            {
                var fireAngle = Random.insideUnitCircle.normalized;
                for (var i = 0; i <= _weaponConfig.Number; i++)
                {

                    var random = Random.value * _weaponConfig.Scatter;

                    var newAngle = fireAngle.Rotate(random - _weaponConfig.Scatter);

                    CreateBullet(newAngle);
                }
                if (_weaponConfig.FireSound != null)
                {
                    _weaponConfig.FireSound.Play(_audioSource);
                }
            }, gameObject);
        }
        else
        {
            _timedEvent.SetFramesInSeconds(fireRate);
        }
    }

    private void CreateBullet(Vector2 fireAngle)
    {
        float angle = Mathf.Atan2(fireAngle.y, fireAngle.x);

        var bullet = _gameManager.Pool.GetPooledObject(_weaponConfig.Bullet, transform.position, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg));
        if (_weaponConfig.ParentedToPlayer)
        {
            bullet.transform.SetParent(transform, true);
        }
        bullet.transform.localScale = Vector3.one * _weaponConfig.Scale;
        var damaging = bullet.GetComponent<DamagingBehavior>();
        damaging.Damage = _weaponConfig.Damage;
        damaging.Knockback = _weaponConfig.Knockback;
        damaging.HitSound = _weaponConfig.HitSound;
        //var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        var behavior = bullet.GetComponent<BulletBehavior>();
        behavior.Speed = _weaponConfig.Speed.GetRandomValue();
        behavior.MoveAngle = fireAngle;
        behavior.Duration = _weaponConfig.Duration.GetRandomValue();
    }
    private void CreateOrbits()
    {
        for (var i = 0; i < _weaponConfig.Number; i++)
        {
            var orbit = _gameManager.Pool.GetPooledObject(_weaponConfig.Bullet, transform.position, Quaternion.identity);
            orbit.transform.parent = transform;
            orbit.transform.localScale = Vector3.one * _weaponConfig.Scale;
            var damaging = orbit.GetComponent<DamagingBehavior>();
            damaging.Damage = _weaponConfig.Damage;
            damaging.Knockback = _weaponConfig.Knockback;
            damaging.HitSound = _weaponConfig.HitSound;
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

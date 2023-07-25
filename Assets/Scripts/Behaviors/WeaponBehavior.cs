using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class WeaponBehavior : AthenaMonoBehavior, IAlive
{
    [SerializeField]
    private WeaponSO _weaponConfig;

    private GameManagerBehavior.TimedEvent _timedEvent;

    private AudioSource _audioSource;

    protected override void Start()
    {
        base.Start();
        _audioSource = GetComponent<AudioSource>();
        SafeAssigned(_weaponConfig);

        if (_timedEvent == null)
        {
            _timedEvent = _gameManager.AddTimedEvent(_weaponConfig.Rate, () =>
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
            _timedEvent.SetFramesInSeconds(_weaponConfig.Rate);
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
        //var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        var behavior = bullet.GetComponent<BulletBehavior>();
        behavior.Speed = _weaponConfig.Speed.GetRandomValue();
        behavior.MoveAngle = fireAngle;
        behavior.Duration = _weaponConfig.Duration.GetRandomValue();
    }
}
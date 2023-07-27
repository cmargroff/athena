using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
                var flying = _gameManager.Player.GetComponent<FlyingBehavior>();
                Vector2 fireAngle;
                if (_weaponConfig.FireAngle == WeaponSO.FireAngleEnum.MovementDirection && flying.MoveAngle != Vector2.zero)
                {
                    fireAngle = flying.MoveAngle;
                }
                else if(_weaponConfig.FireAngle == WeaponSO.FireAngleEnum.ClosestEnemy)
                {
                    var closest = FindClosestEnemy(_gameManager.Player.transform.position);
                    if (closest != null)
                    {
                        fireAngle= (closest.transform.position-_gameManager.Player.transform.position).normalized;
                    }
                    else
                    {
                        fireAngle = Random.insideUnitCircle.normalized;
                    }
                }
                else
                {
                    fireAngle = Random.insideUnitCircle.normalized;
                }

                for (var i = 0; i <= _weaponConfig.Number; i++)
                {
                    var random = Random.value * _weaponConfig.Scatter;

                    var newAngle = fireAngle.Rotate(random - _weaponConfig.Scatter/2f);

                    CreateBullet(newAngle);
                }

                _weaponConfig.FireSound?.Play(_audioSource);

            }, gameObject);

        }
        else
        {
            _timedEvent.SetFramesInSeconds(_weaponConfig.Rate);
        }

    }

    private Collider2D FindClosestEnemy(Vector2 point)
    {
        Collider2D[] result= new Collider2D[1];
        for (int i = 1; i <= 32; i *= 2)
        {
            Physics2D.OverlapCircleNonAlloc(point, i, result, 1 << 7);
            if (result[0] != null)
            {
                return result[0];
            }
        }
        return null;
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
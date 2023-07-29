using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Assets.Scripts.Utils;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(StatAdjust))]
public class WeaponBehavior : AthenaMonoBehavior, IAlive
{
    private StatAdjust _statAdjust;
    [SerializeField]
    private WeaponSO _weaponConfig;

    private TimedEvent _timedEvent;

    private AudioSource _audioSource;


    protected override void Start()
    {
        base.Start();
        _statAdjust = GetComponent<StatAdjust>();
        _statAdjust.OnStatsChanged.AddListener(StatsChanged);

        _audioSource = GetComponent<AudioSource>();
        SafeAssigned(_weaponConfig);


        _timedEvent = _gameManager.AddTimedEvent(_weaponConfig.Rate / _statAdjust.GetAttackFrequency(), () =>
        {
            var flying = _gameManager.Player.GetComponent<FlyingBehavior>();
            Vector2? fireAngle = null;
            switch (_weaponConfig.FireAngle)
            {
                case WeaponSO.FireAngleEnum.MovementDirection:
                    {
                        if (flying.LastMoveAngle != Vector2.zero)
                        {
                            fireAngle = flying.LastMoveAngle;
                        }

                        break;
                    }
                case WeaponSO.FireAngleEnum.ClosestEnemy:
                    {
                        var closest = FindClosestEnemy(_gameManager.Player.transform.position);
                        if (closest != null)
                        {
                            fireAngle = (closest.transform.position - _gameManager.Player.transform.position).normalized;

                        }

                        break;
                    }
            }
            fireAngle ??= Random.insideUnitCircle.normalized;

            for (var i = 0; i < _weaponConfig.Number; i++)
            {
                var random = Random.value * _weaponConfig.Scatter;

                var newAngle = fireAngle.Value.Rotate(random - _weaponConfig.Scatter / 2f);

                CreateBullet(newAngle);
            }

            _weaponConfig.FireSound?.Play(_audioSource);

        }, gameObject);



    }

    private void StatsChanged()
    {
        _timedEvent?.SetFramesInSeconds(_weaponConfig.Rate / _statAdjust.GetAttackFrequency());
    }


    private Collider2D FindClosestEnemy(Vector2 point)
    {
        Collider2D[] result = new Collider2D[1];
        for (float f = 1; f <= 32; f += 1)
        {
            Physics2D.OverlapCircleNonAlloc(point, f, result, _gameManager.Enemies);
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
        damaging.Damage = _weaponConfig.Damage * _gameManager.PlayerCharacter.Damage;
        damaging.Knockback = _weaponConfig.Knockback * _gameManager.PlayerCharacter.Knockback;
        //var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        var behavior = bullet.GetComponent<BulletBehavior>();
        behavior.Speed = _weaponConfig.Speed.GetRandomValue();
        behavior.MoveAngle = fireAngle;
        behavior.Duration = _weaponConfig.Duration.GetRandomValue();
    }
}
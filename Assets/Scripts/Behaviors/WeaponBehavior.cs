using System;
using Assets.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(StatAdjust))]
public class WeaponBehavior : BaseWeaponBehavior, IAlive
{
    private StatAdjust _statAdjust;
    private TimedEvent _timedEvent;
    private AudioSource _audioSource;
    protected override void Start()
    {


        base.Start();
        _statAdjust = GetComponent<StatAdjust>();
        _statAdjust.OnStatsChanged.AddListener(StatsChanged);

        _audioSource = GetComponent<AudioSource>();
        SafeAssigned(WeaponConfig);

        if (_statAdjust.GetAttackFrequency() > 0)
        {
            _timedEvent = _gameManager.AddTimedEvent(WeaponConfig.Rate / _statAdjust.GetAttackFrequency(), () =>
            {
                var flying = _gameManager.Player.GetComponent<FlyingBehavior>();
                Vector2? fireAngle = null;
                switch (WeaponConfig.FireAngle)
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
                            var closest = FindClosestEnemy(transform.position);
                            if (closest != null)
                            {
                                fireAngle = (closest.transform.position - transform.position)
                                    .normalized;

                            }
                            break;
                        }
                }

                fireAngle ??= Random.insideUnitCircle.normalized;

                for (var i = 0; i < WeaponConfig.Number; i++)
                {
                    var random = Random.value * WeaponConfig.Scatter;

                    var newAngle = fireAngle.Value.Rotate(random - WeaponConfig.Scatter / 2f);

                    CreateBullet(newAngle);

                    
                }

                WeaponConfig.FireSound?.Play(_audioSource);

            }, gameObject);
        }
    }
    private void StatsChanged()
    {
        if (_statAdjust.GetAttackFrequency() > 0)
        {
            _timedEvent?.SetFramesInSeconds(WeaponConfig.Rate / _statAdjust.GetAttackFrequency(),_gameManager.FrameCount);
        }
        else
        {
            if (_timedEvent != null)
            {
                _gameManager.RemoveTimedEvent(_timedEvent);
            }
        }
    }
    private Collider2D FindClosestEnemy(Vector2 point)
    {
        Collider2D[] result = new Collider2D[1];
        for (float f = 1; f <= 32; f += 1)//32 is about the size of the stage
        {
            Physics2D.OverlapCircleNonAlloc(point, f, result, WeaponConfig.Enemies);
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

        var bullet = _gameManager.Pool.GetPooledObject(WeaponConfig.Bullet, transform.position, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg));
        if (WeaponConfig.ParentedToPlayer)
        {
            bullet.transform.SetParent(transform, true);
        }
        bullet.transform.localScale = Vector3.one * WeaponConfig.Scale;
        var damaging = bullet.GetComponent<DamagingBehavior>();
        damaging.Damage = WeaponConfig.Damage * _gameManager.PlayerCharacter.Damage;
        damaging.Knockback = WeaponConfig.Knockback * _gameManager.PlayerCharacter.Knockback;
        damaging.Pierce = WeaponConfig.Pierce;
        //var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        var behavior = bullet.GetComponent<BulletBehavior>();
        behavior.Speed = WeaponConfig.Speed.GetRandomValue();
        behavior.MoveAngle = fireAngle;
        behavior.Duration = WeaponConfig.Duration.GetRandomValue();
        bullet.transform.localScale*=_gameManager.PlayerCharacter.BulletSize;
    }
    private void OnDestroy()
    {
        if (_timedEvent != null)
        {
            _gameManager.RemoveTimedEvent(_timedEvent);
        }
    }

    //private void OnDisable() //taking this out. Not sure why it breaks the player, or if there is an issue where dead enemies shoot back
    //{
    //    if (_timedEvent != null)
    //    {
    //        _gameManager.RemoveTimedEvent(_timedEvent);
    //    }
    //}
}
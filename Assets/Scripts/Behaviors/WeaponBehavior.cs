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
        _audioSource = GetComponent<AudioSource>();
        SafeAssigned(_weaponConfig);
        if (_weaponConfig.Orbit)
        {
            BaseBulletManager.GetBulletManager(BaseBulletManager.BulletTypesEnum.Orbiting).CreateBullet(gameObject, _weaponConfig, _gameManager.Pool, _audioSource);
        }
        else
        {
            _timedEvent = _gameManager.AddTimedEvent(_weaponConfig.Rate, () =>
            {
                BaseBulletManager.GetBulletManager(BaseBulletManager.BulletTypesEnum.Standard).CreateBullet(gameObject, _weaponConfig, _gameManager.Pool, _audioSource);
            }, gameObject);

        }
    }
}
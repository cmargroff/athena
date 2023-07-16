using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I don't know if this needs it's own behavior yet
public class WeaponizedBehavior : AthenaMonoBehavior
{
    public GameObject BulletPrefab;
    protected override void Start()
    {
        base.Start();
        _gameManager.AddTimedEvent(1, () =>
        {
            var bullet = _gameManager.Pool.GetPooledObject(BulletPrefab, transform.position, Quaternion.identity);
            var behavior = bullet.GetComponent<BulletBehavior>();
            behavior.Speed = 15f;
            behavior.MoveAngle = Random.insideUnitCircle.normalized;

        });
    }

}

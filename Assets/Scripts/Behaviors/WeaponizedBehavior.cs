using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I don't know if this needs it's own behavior yet
public class WeaponizedBehavior : AthenaMonoBehavior
{
    public GameObject BulletPrefab;
    [SerializeField]
    private float _fireRate;

    private GameManagerBehavior.TimedEvent _timedEvent;

    protected override void Start()
    {
        base.Start();
        SetFire(_fireRate);
    }
    public void SetFire(float fireRate )
    {
        if (_timedEvent == null)
        {
            _timedEvent = _gameManager.AddTimedEvent(fireRate, () =>
            {
                var bullet = _gameManager.Pool.GetPooledObject(BulletPrefab, transform.position, Quaternion.identity);
                //var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                var behavior = bullet.GetComponent<BulletBehavior>();
                behavior.Speed = 15f;
                behavior.MoveAngle = Random.insideUnitCircle.normalized;

            }, gameObject);
        }
        else
        { 
            _timedEvent.SetFramesInSeconds(fireRate);
        }
    }
}

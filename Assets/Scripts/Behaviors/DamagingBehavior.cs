
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DamagingBehavior : AthenaMonoBehavior
{
    [SerializeField]
    [ReadOnly]
    public float Damage = 1;    
    public float Knockback = 1;

    private FlyingBehavior _parent;
    private BulletBehavior _bullet;

    protected override void Start()
    {
        base.Start();

        _parent = GetComponentInParent<FlyingBehavior>();
        _bullet = GetComponentInParent<BulletBehavior>();


    }

    public Vector2 GetKnockbackAngle()
    {
        if (_parent != null)
        {
            return (transform.position - _parent.transform.position).normalized;
        }
        else if (_bullet != null)
        {
            return _bullet.MoveAngle;
        }
        else
        {
            return Vector2.zero;
        }
    }
}

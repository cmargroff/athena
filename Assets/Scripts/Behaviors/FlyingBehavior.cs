using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBehavior : AthenaMonoBehavior
{
    Rigidbody _rigidBody;

    public Vector2 MoveAngle;

    public Collider collider;

    [SerializeField]
    private float speed;

    protected override void Start()
    {
        base.Start();
        _rigidBody = SafeGetComponent<Rigidbody>();
        SafeAssigned(collider);
    }

    // Update is called once per frame
    protected override void PausibleFixedUpdate()
    {
        MoveAngle = MoveAngle.normalized;
        var moveDir = new Vector3(MoveAngle.x*-1, 0f, MoveAngle.y*-1);
        var newPosition=_rigidBody.position+(moveDir * speed);
        if (ColliderUtils.IsPointInsideCollider(collider, newPosition))
        {
            _rigidBody.position = newPosition;
        }
        else
        {
            //moveDir = new Vector3(MoveAngle.x * -1, 0f, 0f);
            newPosition = _rigidBody.position + (Quaternion.AngleAxis(-45, Vector3.up) *moveDir * speed/1.5f);
            if (ColliderUtils.IsPointInsideCollider(collider, newPosition))
            {
                _rigidBody.position = newPosition;
            }
            else
            {
                //moveDir = new Vector3(0f * -1, 0f, MoveAngle.y * -1);
                newPosition = _rigidBody.position + (Quaternion.AngleAxis(45, Vector3.up) * moveDir * speed/1.5f);
                if (ColliderUtils.IsPointInsideCollider(collider, newPosition))
                {
                    _rigidBody.position = newPosition;
                }
            }
        }
    }


}

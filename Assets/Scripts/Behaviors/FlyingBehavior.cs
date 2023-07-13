using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBehavior : AthenaMonoBehavior
{
    Rigidbody _rigidBody;

    public Vector2 MoveAngle;

    [SerializeField]
    private float speed;

    protected override void Start()
    {
        base.Start();
        _rigidBody = SafeGetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected override void PausibleFixedUpdate()
    {
        MoveAngle = MoveAngle.normalized;
        var moveDir = new Vector3(MoveAngle.x*-1, 0f, MoveAngle.y*-1);
        _rigidBody.position+=(moveDir * speed);
    }
}

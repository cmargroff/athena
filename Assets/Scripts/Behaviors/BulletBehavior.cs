using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : AthenaMonoBehavior
{
    // Start is called before the first frame update
    public Vector2 MoveAngle;
    public float Speed;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void PausibleUpdate()
    {
        MoveAngle = MoveAngle.normalized;

        var deltaSpeed = Speed * Time.deltaTime;

        var moveDir = new Vector3(MoveAngle.x, MoveAngle.y, 0f);
        var newPosition = transform.position + (moveDir * deltaSpeed);

        if (!ColliderUtils.IsPointInsideCollider2D(_gameManager.Bounds, transform.position))
        {
            Destroy(this.gameObject);
        }

        transform.position = newPosition;


    }

}


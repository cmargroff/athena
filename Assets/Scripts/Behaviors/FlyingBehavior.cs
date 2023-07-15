using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class FlyingBehavior : AthenaMonoBehavior
{
    private Rigidbody _rigidBody;


    public Vector2 MoveAngle;
    
    public bool FacesRight=true;
    
    
    private  Collider _bounds;
    

    [SerializeField]
    private float speed;

    protected override void Start()
    {
        base.Start();
        _rigidBody = GetComponent<Rigidbody>();
        SafeAssigned(_gameManager);
        _bounds = _gameManager.Bounds;
    }

    [SerializeField]
    private float OFFSET_ANGLE= 90;

    [SerializeField]
    private float SLOWDOWN = 2f;
    // Update is called once per frame
    protected override void PausibleUpdate()
    {
        MoveAngle = MoveAngle.normalized;

        var deltaSpeed=speed*Time.deltaTime;

        var moveDir = new Vector3(MoveAngle.x, 0f, MoveAngle.y);
        var newPosition=_rigidBody.position+(moveDir * deltaSpeed);
        if (ColliderUtils.IsPointInsideCollider(_bounds, newPosition))
        {
            _rigidBody.position = newPosition;
        }
        else
        {
            //moveDir = new Vector3(MoveAngle.x * -1, 0f, 0f);
            newPosition = _rigidBody.position + (Quaternion.AngleAxis(-OFFSET_ANGLE, Vector3.up) *moveDir * deltaSpeed / SLOWDOWN);
            if (ColliderUtils.IsPointInsideCollider(_bounds, newPosition))
            {
                _rigidBody.position = newPosition;
            }
            else
            {
                //moveDir = new Vector3(0f * -1, 0f, MoveAngle.y * -1);
                newPosition = _rigidBody.position + (Quaternion.AngleAxis(OFFSET_ANGLE, Vector3.up) * moveDir * deltaSpeed / SLOWDOWN);
                if (ColliderUtils.IsPointInsideCollider(_bounds, newPosition))
                {
                    _rigidBody.position = newPosition;
                }
            }
        }

        if (moveDir.x <= 0 == FacesRight)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); ;
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

    }


}

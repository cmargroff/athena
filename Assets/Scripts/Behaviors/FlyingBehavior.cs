
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatAdjust))]
public class FlyingBehavior : AthenaMonoBehavior, IAlive
{
    private StatAdjust _statAdjust;

    public Vector2 MoveAngle;
    
    public Vector2 LastMoveAngle;

    public bool FacesRight = true;

    private Collider2D _bounds;
    
    [SerializeField]
    private GameObject _visual;
    
    [SerializeField]
    public  float Speed;

    [SerializeField]
    private LayerMask _bumpsInto;

    protected override void Start()
    {
        base.Start();
        _bounds = _gameManager.Bounds;
        _statAdjust = GetComponent<StatAdjust>();
    }

    [SerializeField]
    private float OFFSET_ANGLE = 90;

    [SerializeField]
    private float SLOWDOWN = 2f;
    // Update is called once per frame
    protected override void PlausibleUpdate()
    {
        MoveAngle = MoveAngle.normalized;
        if (MoveAngle != Vector2.zero)
        {
            LastMoveAngle = MoveAngle;
        }


        var deltaSpeed = Speed * _statAdjust.GetSpeedAdjust()* Time.deltaTime;

        var moveDir = new Vector3(MoveAngle.x, MoveAngle.y,0f);
        var newPosition = transform.position + (moveDir * deltaSpeed);



        if (!ColliderUtils.IsPointInsideCollider2D(_bounds, newPosition))
        {
            //moveDir = new Vector3(MoveAngle.x * -1, 0f, 0f);
            newPosition = transform.position + (Quaternion.Euler(new Vector3(0, 0, -OFFSET_ANGLE)) * moveDir * deltaSpeed / SLOWDOWN);
            if (!ColliderUtils.IsPointInsideCollider2D(_bounds, newPosition))
            {
                //moveDir = new Vector3(0f * -1, 0f, MoveAngle.y * -1);
                newPosition = transform.position + (Quaternion.Euler(new Vector3(0, 0, OFFSET_ANGLE)) * moveDir * deltaSpeed / SLOWDOWN);
                if (!ColliderUtils.IsPointInsideCollider2D(_bounds, newPosition))
                {
                    newPosition = transform.position;
                }
            }
        }

        transform.position = newPosition;

        if (moveDir.x < 0 == FacesRight)
        {
            if (_visual == null)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                _visual.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
        else if (moveDir.x > 0 == FacesRight)
        {
            if (_visual == null)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
            else
            {
                _visual.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
           
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((_bumpsInto.value & (1 << other.gameObject.layer)) != 0)
        {
            // Calculate the separation distance based on the size of the objects
            float separationDistance = Speed * Time.deltaTime * .25f;  // (boundsThis.extents.magnitude + boundsOther.extents.magnitude) * 1.2f;

            // Calculate the separation direction
            Vector3 separationDirection = transform.position - other.transform.position;
            separationDirection.Normalize();

            // Move both objects away from each other
            transform.position += separationDirection * separationDistance * 0.5f;
            other.transform.position -= separationDirection * separationDistance * 0.5f;
        }
    }
}


    

using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlyingBehavior : AthenaMonoBehavior
{

    public Vector2 MoveAngle;

    public bool FacesRight = true;


    private Collider _bounds;


    [SerializeField]
    private float _speed;

    [SerializeField]
    private LayerMask _bumpsInto;

    protected override void Start()
    {
        base.Start();
        SafeAssigned(_gameManager);
        _bounds = _gameManager.Bounds;
    }

    [SerializeField]
    private float OFFSET_ANGLE = 90;

    [SerializeField]
    private float SLOWDOWN = 2f;
    // Update is called once per frame
    protected override void PausibleUpdate()
    {
        MoveAngle = MoveAngle.normalized;

        var deltaSpeed = _speed * Time.deltaTime;

        var moveDir = new Vector3(MoveAngle.x, MoveAngle.y,0f);
        var newPosition = transform.position + (moveDir * deltaSpeed);

        //if (Physics.Raycast(origin: _rigidBody.position, direction: moveDir, out RaycastHit hit, maxDistance: _speed * deltaSpeed, layerMask: _bumpsInto.value))
        //{
        //    var target = _rigidBody.position - hit.transform.position;
        //    var moveAngle = new Vector2(target.x, target.z);
        //    moveDir = new Vector3(moveAngle.x, 0f, moveAngle.y);
        //    newPosition = _rigidBody.position + (moveDir * deltaSpeed * SLOWDOWN);
        //}


        if (!ColliderUtils.IsPointInsideCollider(_bounds, newPosition))
        {
            //moveDir = new Vector3(MoveAngle.x * -1, 0f, 0f);
            newPosition = transform.position + (Quaternion.AngleAxis(-OFFSET_ANGLE, Vector3.up) * moveDir * deltaSpeed / SLOWDOWN);
            if (!ColliderUtils.IsPointInsideCollider(_bounds, newPosition))
            {
                //moveDir = new Vector3(0f * -1, 0f, MoveAngle.y * -1);
                newPosition = transform.position + (Quaternion.AngleAxis(OFFSET_ANGLE, Vector3.up) * moveDir * deltaSpeed / SLOWDOWN);
                if (!ColliderUtils.IsPointInsideCollider(_bounds, newPosition))
                {
                    newPosition = transform.position;
                }
            }
        }
        transform.position = newPosition;

        if (moveDir.x < 0 == FacesRight)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); ;
        }
        else if (moveDir.x > 0 == FacesRight)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }

    }

    private void OnTriggerStay(Collider other)
    {


        // Calculate the separation distance based on the size of the objects
        float separationDistance = _speed * Time.deltaTime * .25f;  // (boundsThis.extents.magnitude + boundsOther.extents.magnitude) * 1.2f;

        // Calculate the separation direction
        Vector3 separationDirection = transform.position - other.transform.position;
        separationDirection.Normalize();

        // Move both objects away from each other
        transform.position += separationDirection * separationDistance * 0.5f;
        other.transform.position -= separationDirection * separationDistance * 0.5f;

    }
}


    

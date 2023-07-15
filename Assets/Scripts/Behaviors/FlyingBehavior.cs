using Assets.Scripts.Utils;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class FlyingBehavior : AthenaMonoBehavior
{
  private Rigidbody _rigidBody;


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
    _rigidBody = GetComponent<Rigidbody>();
    SafeAssigned(_gameManager);
    _bounds = _gameManager.Bounds;
  }

  [SerializeField]
  private float OFFSET_ANGLE = 90;

  [SerializeField]
  private float SLOWDOWN = 2f;
  // Update is called once per frame
  float _minDistance = 1f;
  protected override void PausibleUpdate()
  {
    MoveAngle = MoveAngle.normalized;

    var deltaSpeed = _speed * Time.deltaTime;

    var moveDir = new Vector3(MoveAngle.x, 0f, MoveAngle.y);
    var newPosition = _rigidBody.position + (moveDir * deltaSpeed);

    // if (
    //     Physics.Raycast(
    //         origin: _rigidBody.position,
    //         direction: moveDir,
    //         out RaycastHit hit,
    //         maxDistance: _speed * deltaSpeed,
    //         layerMask: _bumpsInto.value
    //     )
    // )
    // {
    //     var target = _rigidBody.position - hit.transform.position;
    //     var moveAngle = new Vector2(target.x, target.z);
    //     moveDir = new Vector3(moveAngle.x, 0f, moveAngle.y);
    //     newPosition = _rigidBody.position + (moveDir * deltaSpeed * SLOWDOWN);
    // }


    if (!ColliderUtils.IsPointInsideCollider(_bounds, newPosition))
    {
      //moveDir = new Vector3(MoveAngle.x * -1, 0f, 0f);
      newPosition = _rigidBody.position + (
          Quaternion.AngleAxis(-OFFSET_ANGLE, Vector3.up)
          * moveDir * deltaSpeed / SLOWDOWN
      );
      if (!ColliderUtils.IsPointInsideCollider(_bounds, newPosition))
      {
        //moveDir = new Vector3(0f * -1, 0f, MoveAngle.y * -1);
        newPosition = _rigidBody.position + (
            Quaternion.AngleAxis(OFFSET_ANGLE, Vector3.up)
            * moveDir * deltaSpeed / SLOWDOWN
        );
        if (!ColliderUtils.IsPointInsideCollider(_bounds, newPosition))
        {
          newPosition = _rigidBody.position;
        }
      }
    }
    _rigidBody.position = newPosition;

    if (moveDir.x < 0 == FacesRight)
    {
      transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    else if (moveDir.x > 0 == FacesRight)
    {
      transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }


  }

  public void OnTriggerEnter(Collider other)
  {
    var flying = other.gameObject.GetComponent<FlyingBehavior>();
    if (flying != null)
    {
        var angle = Vector3.Angle(other.transform.position, transform.position );
        // set both objects a minimum distance apart
        var distance = Vector3.Distance(other.transform.position, transform.position);
        var delta = distance - _minDistance;
        var deltaVector = (other.transform.position - transform.position).normalized * delta;
        other.transform.position += deltaVector;
        transform.position -= deltaVector;


    }
  }
}

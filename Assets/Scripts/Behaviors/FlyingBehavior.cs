using Assets.Scripts.Utils;
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
    private Renderer _visual;
    [SerializeField]
    public float Speed;
    [SerializeField]
    public float SpeedModifier=1;
    [SerializeField]
    private LayerMask _bumpsInto;
    [SerializeField]
    private Vector3 _flipVector3 = new Vector3(0, 0, 180);
    private float OffsetAngle = 90;
    private float Slowdown = 2f;
    private static readonly int Flip = Shader.PropertyToID("_Flip");
    protected override void Start()
    {
        base.Start();
        _bounds = _gameManager.Bounds;
        _statAdjust = GetComponent<StatAdjust>();
    }
    // Update is called once per frame
    protected override void PlausibleUpdate()
    {
        MoveAngle = MoveAngle.normalized;
        if (MoveAngle != Vector2.zero)
        {
            LastMoveAngle = MoveAngle;
        }


        var deltaSpeed = Speed* SpeedModifier * _statAdjust.GetSpeedAdjust() * Time.deltaTime;
        var moveDir = new Vector3(MoveAngle.x, MoveAngle.y, 0f);
        if (ColliderUtils.IsPointInsideCollider2D(_bounds, transform.position)) //if we are inside the bounds, do normal movement
        {
            var newPosition = transform.position + (moveDir * deltaSpeed);
            if (!ColliderUtils.IsPointInsideCollider2D(_bounds, newPosition))
            {
                //moveDir = new Vector3(MoveAngle.x * -1, 0f, 0f);
                newPosition = transform.position +
                              (Quaternion.Euler(new Vector3(0, 0, -OffsetAngle)) * moveDir * deltaSpeed / Slowdown);
                if (!ColliderUtils.IsPointInsideCollider2D(_bounds, newPosition))
                {
                    //moveDir = new Vector3(0f * -1, 0f, MoveAngle.y * -1);
                    newPosition = transform.position + (Quaternion.Euler(new Vector3(0, 0, OffsetAngle)) * moveDir *
                        deltaSpeed / Slowdown);
                    if (!ColliderUtils.IsPointInsideCollider2D(_bounds, newPosition))
                    {
                        newPosition = transform.position;
                    }
                }
            }

            transform.position = newPosition;
        }
        else //otherwise push the object into bounds
        {
            moveDir = (_bounds.transform.position - transform.position).normalized;
            transform.position += moveDir * deltaSpeed;
        }

        if (moveDir.x != 0)
        {
            _visual.material.SetFloat(Flip, moveDir.x < 0 == FacesRight ? 0f : 1f);
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
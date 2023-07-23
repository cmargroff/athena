
using Assets.Scripts.Utils;
using Unity.Collections;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class DamagingBehavior : AthenaMonoBehavior
{
  [SerializeField]
  [ReadOnly]
  public float Damage = 1;
  public float Knockback = 1;
  public float Pierce = float.PositiveInfinity;

  private FlyingBehavior _parent;
  private BulletBehavior _bullet;

  public GameSound HitSound;

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
  private void OnTriggerStay2D(Collider2D other)
  {
    if (_bullet != null)
      Pierce--;
    if (Pierce < 1)
    {
      gameObject.SetActive(false);
    }
  }
}

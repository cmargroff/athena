using UnityEngine;
using DG.Tweening;
public class BulletBehavior : AthenaMonoBehavior
{
  // Start is called before the first frame update
  public Vector2 MoveAngle;
  public float Speed;
  public float Duration;

  Sequence _seq;
  protected override void OnActive()
  {
    _seq = DOTween.Sequence(); //new Sequence()
    _seq.SetUpdate(UpdateType.Manual);
    if (Speed > 0)
    {
      var tween = DOTween.To(
        (t) =>
        {
          transform.position += new Vector3(MoveAngle.x, MoveAngle.y, 0f) * Speed;
        },
        0, 1, Duration
      )
        .OnComplete(() => gameObject.SetActive(false))
        .SetEase(Ease.Linear);
      _seq.Append(tween);
    }
    else
    {
      _seq
      .AppendInterval(Duration)
      .AppendCallback(() => gameObject.SetActive(false));
    }
  }
  protected override void PausibleFixedUpdate()
  {
    base.PausibleFixedUpdate();
    _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
  }
}


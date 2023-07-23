using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class BulletBehavior : AthenaMonoBehavior
{
  // Start is called before the first frame update
  public Vector2 MoveAngle;
  public float Speed;
  public float Duration;
  public bool IsLooped;
  public float TimeOffset;
  public DOSetter<float> customTravelMode;

  Sequence _seq;

  protected override void OnActive()
  {
    _seq = DOTween.Sequence(); //new Sequence()
    _seq.SetUpdate(UpdateType.Manual);
    if (customTravelMode is not null)
    {
      var tween = DOTween.To(
        customTravelMode, 0, 1, Duration
      )
        .SetEase(Ease.Linear); // map travel mode to ease
      _seq.Append(tween);
    }
    else
    {
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
          .SetEase(Ease.Linear); // map travel mode to ease
        _seq.Append(tween);
      }
      else
      {
        _seq
        .AppendInterval(Duration);
      }
    }
    // infinite loop with no duration
    if (IsLooped)
    {
      _seq.SetLoops(-1);
    }
    else
    {
      _seq.AppendCallback(() => gameObject.SetActive(false));
    }
    if (TimeOffset > 0)
    {
      _seq.Goto(TimeOffset, true);
    }
  }
  protected override void PausibleFixedUpdate()
  {
    base.PausibleFixedUpdate();
    _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
  }


}


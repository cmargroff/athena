using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
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
      var tween = transform.DOMove(
          transform.position + new Vector3(MoveAngle.x, MoveAngle.y, 0f) * Speed,
          Duration
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


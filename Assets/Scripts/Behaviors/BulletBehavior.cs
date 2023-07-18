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

    TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> _bulletTween;
    protected override void OnActive()
    {
        if (Speed > 0)
            _bulletTween = transform.DOMove(transform.position + new Vector3(MoveAngle.x, MoveAngle.y, 0f) * Speed, Duration).OnComplete(() =>
                gameObject.SetActive(false)
            ).SetUpdate(UpdateType.Manual).SetEase(Ease.Linear);
        else
        {
            var seq = DOTween.Sequence(); //new Sequence()
            seq.AppendInterval(Duration);
            seq.AppendCallback(() => gameObject.SetActive(false));       
        }
    }

    protected override void PausibleFixedUpdate()
    {
        base.PausibleFixedUpdate();
        _bulletTween.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }

}


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

    TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> _bulletTween;
    protected override void OnActive()
    {
        _bulletTween = transform.DOMove(transform.position + new Vector3(MoveAngle.x, MoveAngle.y, 0f) * 10, 1).OnComplete(() => {
            //Destroy(this.gameObject);

            gameObject.SetActive(false);
        }
        ).SetUpdate(UpdateType.Manual).SetEase(Ease.Linear);

    }

    protected override void PausibleFixedUpdate()
    {
        base.PausibleFixedUpdate();
        _bulletTween.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }

}


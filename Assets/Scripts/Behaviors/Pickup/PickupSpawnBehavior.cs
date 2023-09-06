using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BaseStateMachineBehavior))]
public  class PickupSpawnBehavior:AthenaMonoBehavior, ISpawn
{
    private Sequence _seq;
    public override void OnActive()
    {
        base.OnActive();
        _seq = DOTween.Sequence();

        _seq.SetUpdate(UpdateType.Manual);
        _seq.Append(transform.DOMove(transform.position+ (Vector3)Random.insideUnitCircle, 1f)).SetEase(Ease.OutQuad);
        _seq.AppendCallback(() =>
            _stateMachine.SetState(typeof(IAlive))
        );
    }
    protected override void PlausibleFixedUpdate()
    {
        base.PlausibleFixedUpdate();
        _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }

}

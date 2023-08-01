using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(BaseStateMachineBehavior))]
internal class Spawn : AthenaMonoBehavior, ISpawn
{

    private Sequence _seq;
    public override void OnActive()
    {
        base.OnActive();
        _seq = DOTween.Sequence();
        transform.localScale = Vector3.zero;

        _seq.SetUpdate(UpdateType.Manual);
        _seq.Append(transform.DOScale(1, .5f));
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
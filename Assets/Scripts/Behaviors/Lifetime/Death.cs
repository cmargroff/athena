using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(BaseStateMachineBehavior))]
public  class Death: AthenaMonoBehavior,IDeath
{
    private Sequence _seq;
    public override void OnActive()
    {
        base.OnActive();
        _seq = DOTween.Sequence();
        _seq.SetUpdate(UpdateType.Manual);
        _seq.Append(transform.DOScale(0, 1));
        _seq.AppendCallback(
            () => gameObject.SetActive(false)
        );
    }
    protected override void PlausibleFixedUpdate()
    {
        base.PlausibleFixedUpdate();
        _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }
}


using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BaseStateMachineBehavior))]
public class Death : AthenaMonoBehavior, IDeath
{
    public UnityEvent DeathComplete; 
    protected Sequence _seq;

    protected override void Start()
    {
        base.Start();
        DeathComplete=new UnityEvent();
    }

    public override void OnActive()
    {
        base.OnActive();
        _seq = DOTween.Sequence();
        _seq.SetUpdate(UpdateType.Manual);
        _seq.Append(transform.DOScale(0, 1));
        _seq.AppendCallback(
            () =>
            {
                DeathComplete?.Invoke();
                gameObject.SetActive(false);
            });
    }
    protected override void PlausibleFixedUpdate()
    {
        base.PlausibleFixedUpdate();
        _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }
}
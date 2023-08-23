using System;
using DG.Tweening;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine.Serialization;

[RequireComponent(typeof(FlyingBehavior))]
public class ChaseBehavior : AthenaMonoBehavior, IAlive
{
    private FlyingBehavior _flying;
    
    public AnimationCurve SpeedCurve;
    public float LoopTime=1;
    public float MaxSpeedReaim = 1;
    protected Sequence _seq;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _flying = GetComponent<FlyingBehavior>();
        SafeAssigned(_gameManager);

        if (SpeedCurve != null && SpeedCurve.length>0)//checking for zero here as part of the null check
        {
            _flying.SpeedModifier = 0;
            _seq = DOTween.Sequence(); //new Sequence()
            _seq.SetUpdate(UpdateType.Manual);
            var tween=DOTween.To(() => _flying.SpeedModifier, x => _flying.SpeedModifier = x,1, LoopTime).SetEase(SpeedCurve).SetLoops(-1,LoopType.Restart);
            _seq.Append(tween);
        }

    }
    // Update is called once per frame
    protected override void PlausibleUpdate()
    {
        if (_flying.SpeedModifier <= MaxSpeedReaim)
        {
            var target = _gameManager.Player.transform.position - _flying.transform.position;
            _flying.MoveAngle = new Vector2(target.x, target.y);
        }

        

    }
    protected override void PlausibleFixedUpdate()
    {
        base.PlausibleFixedUpdate();
        _seq?.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }
}
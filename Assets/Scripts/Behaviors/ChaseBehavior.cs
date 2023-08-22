using System;
using DG.Tweening;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

[RequireComponent(typeof(FlyingBehavior))]
public class ChaseBehavior : AthenaMonoBehavior, IAlive
{
    private FlyingBehavior _flying;
    [SerializeField]
    private AnimationCurve _speedCurve;
    [SerializeField]
    private float _loopTime=1;

    [SerializeField]
    private float _maxSpeedReaim = 1;
    protected Sequence _seq;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _flying = GetComponent<FlyingBehavior>();
        SafeAssigned(_gameManager);

        if (_speedCurve != null)
        {
            _flying.SpeedModifier = 0;
            _seq = DOTween.Sequence(); //new Sequence()
            _seq.SetUpdate(UpdateType.Manual);
            var tween=DOTween.To(() => _flying.SpeedModifier, x => _flying.SpeedModifier = x,1, _loopTime).SetEase(_speedCurve).SetLoops(Int32.MaxValue-1,LoopType.Restart);
            _seq.Append(tween);
        }

    }
    // Update is called once per frame
    protected override void PlausibleUpdate()
    {
        if (_flying.SpeedModifier <= _maxSpeedReaim)
        {
            var target = _gameManager.Player.transform.position - _flying.transform.position;
            _flying.MoveAngle = new Vector2(target.x, target.y);
        }

        _seq?.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);


    }
}
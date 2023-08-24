using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeath: AthenaMonoBehavior, IDeath
{
    protected Sequence _seq;
    public override void OnActive()
    {
        base.OnActive();
        _seq = DOTween.Sequence();
        _seq.SetUpdate(UpdateType.Manual);
        _seq.Append(transform.DOScale(0, 1));
        _seq.AppendCallback(
            () => _gameManager.Lose()
        );
    }
    protected override void PlausibleFixedUpdate()
    {
        base.PlausibleFixedUpdate();
        _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }
}


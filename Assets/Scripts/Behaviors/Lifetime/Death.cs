using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public  class Death: AthenaMonoBehavior
{
    private Sequence _seq;
    protected override void OnActive()
    {
        base.OnActive();
        _seq = DOTween.Sequence();
        _seq.SetUpdate(UpdateType.Manual);
        _seq.Append(transform.DOScale(0, 1));
        _seq.AppendCallback(() => gameObject.SetActive(false));
    }
    protected override void PausibleFixedUpdate()
    {
        base.PausibleFixedUpdate();
        _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }
}


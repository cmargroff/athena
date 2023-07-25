using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class Spawn : AthenaMonoBehavior
{
    private Sequence _seq;
    protected override void OnActive()
    {
        base.OnActive();
        transform.localScale = Vector3.zero;

        _seq.SetUpdate(UpdateType.Manual);
        _seq.Append(transform.DOScale(1, 1));
        _seq.AppendCallback(() => gameObject.SetActive(false));
    }
    protected override void PausibleFixedUpdate()
    {
        base.PausibleFixedUpdate();
        _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }
}
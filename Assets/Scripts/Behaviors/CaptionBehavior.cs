using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public  class CaptionBehavior :AthenaMonoBehavior
{
    [SerializeField]
    private UnityEngine.UI.Image _image;
    [SerializeField]
    private TextMeshProUGUI _text;

    protected override void Start()
    {
        base.Start();
        SafeAssigned(_image);
        SafeAssigned(_text);

        gameObject.SetActive(false);
    }

    public void SetCaption(VoiceLine voiceline)
    {
        gameObject.SetActive(true);
        _text.text = voiceline.Text;
        _image.sprite = voiceline.Portrait;
        ExecuteAfterTime(voiceline.Time, () => gameObject.SetActive(false));


    }

}


using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HUDHealthBehavior : AthenaMonoBehavior
{
    private Material _mat;
    private Sequence _seq;
    private bool _animating;
    private void Start()
    {
        var img = gameObject.FindObjectByName("Fill").GetComponent<Image>();
        img.material = new Material(img.material);
        _mat = img.material;
        _gameManager.PlayerHealthChanged += UpdateHealth;
    }
    private void UpdateHealth(float percent)
    {
        if (_seq != null)
        {
            _seq.Kill();
        }
        _animating = true;
        _seq = DOTween.Sequence();
        _seq.SetUpdate(UpdateType.Manual);
        _seq.Append(_mat.DOFloat(percent, "_FillAmount", 0.5f));
    }
    protected override void PlausibleFixedUpdate()
    {
        if (_animating)
        {
            _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
        }
    }
}
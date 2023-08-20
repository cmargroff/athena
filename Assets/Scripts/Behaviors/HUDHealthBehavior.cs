using UnityEngine;
using UnityEngine.UI;

public class HUDHealthBehavior : AthenaMonoBehavior
{
    private Material _mat;
    private void Start()
    {
        var img = GetComponentInChildren<Image>();
        img.material = new Material(img.material);
        _mat = img.material;
        _gameManager.PlayerHealthChanged += UpdateHealth;
    }
    private void UpdateHealth(float percent)
    {
        _mat.SetFloat("_FillAmount", percent);
    }
}
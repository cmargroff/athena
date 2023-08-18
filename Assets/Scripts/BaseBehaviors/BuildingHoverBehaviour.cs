using UnityEngine;

public class BuildingHoverBehaviour : AthenaMonoBehavior
{
    [SerializeField]
    private Renderer _hoverIndicator;
    private static readonly int Highlight = Shader.PropertyToID("_Highlight");
    protected override void Start()
    {
        base.Start();
        SafeAssigned(_hoverIndicator);
    }
    public void EnableHoverIndicator()
    {
        _hoverIndicator.material.SetFloat(Highlight, 1);
    }
    public void DisableHoverIndicator()
    {
        _hoverIndicator.material.SetFloat(Highlight, 0);
    }
}
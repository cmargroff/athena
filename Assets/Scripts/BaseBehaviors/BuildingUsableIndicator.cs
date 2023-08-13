
using UnityEngine;

public class BuildingUsableIndicator : AthenaMonoBehavior
{
    [SerializeField]
    private Renderer _plane;

    private static readonly int Border = Shader.PropertyToID("_Border");

    protected override void Start()
    {
        base.Start();
        SafeAssigned(_plane);
    }

    public void EnableHoverIndicator()
    {
        _plane.material.SetFloat(Border, 1);
    }
    public void DisableHoverIndicator()
    {
        _plane.material.SetFloat(Border, 0);
    }

}
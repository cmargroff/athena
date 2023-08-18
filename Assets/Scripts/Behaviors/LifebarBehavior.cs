using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LifebarBehavior : AthenaMonoBehavior
{
    private Material _material;
    private static readonly int LifePercentage = Shader.PropertyToID("_LifePercentage");
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _material = GetComponent<MeshRenderer>().material;
    }
    public void SetHealthPercent(float percent)
    {
        _material.SetFloat(LifePercentage, percent);
    }
    protected override void ContinuousUpdate()
    {
        transform.forward = Camera.main.transform.forward;
        transform.Rotate(90, 180, 0);
    }
}
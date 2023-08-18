using UnityEngine;

public class GrowBehavior : AthenaMonoBehavior
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void PlausibleUpdate()
    {
        base.PlausibleUpdate();
        this.transform.localScale *= 1f + (0.1f * Time.deltaTime);
    }
}
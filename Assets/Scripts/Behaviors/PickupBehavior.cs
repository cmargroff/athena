using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupBehavior : AthenaMonoBehavior, IAlive
{
    public int Amount = 1;
    public Color Color;
    public PickupTypeEnum Type;

    public override void OnActive()
    {
        base.OnActive();
        var spriteMat = GetComponentInChildren<SpriteRenderer>().material;
        // clean up this switch statement to something more readable
        spriteMat.color = Color;
        var trail = GetComponentInChildren<TrailRenderer>();
        trail.colorGradient = new Gradient()
        {
            colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(Color,0),
                new GradientColorKey(Color,1)
            }
        };
    }
    public void Pickup()
    {
        _stateMachine.SetState(typeof(IDeath));
    }
}
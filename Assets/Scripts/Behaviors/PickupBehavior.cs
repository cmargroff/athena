using UnityEngine;



[RequireComponent(typeof(Collider2D))]
public class PickupBehavior : AthenaMonoBehavior, IAlive
{
    public int Amount = 1;
    public Color Color;
    public string Name;

    public void Kill()
    {
        _stateMachine.SetState(gameObject, typeof(IDeath));
    }

    public override void OnActive()
    {
        base.OnActive();
        var material = GetComponentInChildren<SpriteRenderer>().material;
        // clean up this switch statement to something more readable
        material.color = Color;



    }
}
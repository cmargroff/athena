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
        var material2 = GetComponentInChildren<MeshRenderer>().material;
        material2.color = Color;
    }

    public void Pickup()
    {
        if(enabled) //I kind of hate having this exception here but I don't know a better solution right now
            _gameManager.CollectPickup(this);
    }
}
using UnityEngine;

[RequireComponent(typeof(FlyingBehavior))]
public class PlayerInputDriverBehavior : AthenaMonoBehavior, IAlive
{
    private @PlayerInputActions _controls;
    private FlyingBehavior _flying;


    protected override void Start()
    {
        base.Start();
        _controls = new ();
        _flying = GetComponent<FlyingBehavior>();
        _controls.Game.Enable();
    }

    // Start is called before the first frame update

    protected override void PlausibleFixedUpdate()
    {
        _flying.MoveAngle = _controls.Game.Move.ReadValue<Vector2>()*new Vector2(-1,1);

    }
}
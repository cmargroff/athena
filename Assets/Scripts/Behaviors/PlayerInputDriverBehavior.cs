using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDriverBehavior : AthenaMonoBehavior
{
    private @PlayerInputActions _controls;
    private FlyingBehavior _flying;


    protected override void Start()
    {
        base.Start();
        _controls = new @PlayerInputActions();
        _flying = SafeGetComponent<FlyingBehavior>();
        _controls.Game.Enable();
    }

    // Start is called before the first frame update

    protected override void PausibleFixedUpdate()
    {
        _flying.MoveAngle = _controls.Game.Move.ReadValue<Vector2>();

    }
}
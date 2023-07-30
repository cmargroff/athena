using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public  class FieldInteractBehavior : AthenaMonoBehavior
{
    private @PlayerInputActions _controls;
    protected override void Start()
    {
        base.Start();



        _controls = new @PlayerInputActions();
        _controls.Game.Enable();
    }

    protected override void PlausibleFixedUpdate()
    {
        if (_controls.Game.Interact.ReadValue<float>()>0)
        {
            Collider2D[] result = new Collider2D[1];
            Physics2D.OverlapCircleNonAlloc(transform.position, transform.localScale.magnitude, result, _gameManager.Buildings);
            if (result[0] != null) 
            {
                var building = result[0].GetComponent<BuildingInteractBehaviour>();
                if (building != null)
                {
                    building.Interact();
                }
            }


        }

    }
}


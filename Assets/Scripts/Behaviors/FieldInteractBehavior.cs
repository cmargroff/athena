using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class FieldInteractBehavior : AthenaMonoBehavior
{
    private @PlayerInputActions _controls;
    protected override void Start()
    {
        base.Start();



        _controls = new @PlayerInputActions();
        _controls.Game.Enable();
    }

    private BuildingHoverBehaviour _lastBuildingTouched;
    protected override void PlausibleFixedUpdate()
    {

        Collider2D[] result = new Collider2D[1];
        Physics2D.OverlapCircleNonAlloc(transform.position, transform.localScale.magnitude, result, _gameManager.Buildings);
        {
            var building = result[0]?.GetComponent<BuildingInteractBehaviour>();
            if (building != null)
            {
                building.BuildingHover.EnableHoverIndicator();
                _lastBuildingTouched=building.BuildingHover;
                if (_controls.Game.Interact.ReadValue<float>() > 0)
                {
                    building.Interact();
                }

            }
            else
            {
                _lastBuildingTouched?.DisableHoverIndicator();
            }


        }




    }
}


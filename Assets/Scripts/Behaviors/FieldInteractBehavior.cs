using UnityEngine;

public class FieldInteractBehavior : AthenaMonoBehavior
{
    private @PlayerInputActions _controls;
    protected override void Start()
    {
        base.Start();
        _controls = new();
        _controls.Game.Enable();
    }
    private BuildingHoverBehaviour _lastBuildingTouched;
    protected override void PlausibleUpdate()
    {

        Collider2D[] result = new Collider2D[1];
        Physics2D.OverlapCircleNonAlloc(transform.position, transform.localScale.x, result, _gameManager.Buildings);//using x here, magnitude is giving an odd result
        {
            var building = result[0]?.GetComponent<BuildingInteractBehaviour>();
            if (building != null)
            {
                building.BuildingHover.EnableHoverIndicator();
                _lastBuildingTouched = building.BuildingHover;
                
                if (_controls.Game.Interact.WasPressedThisFrame()) //(_controls.Game.Interact.ReadValue<float>() > 0)
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
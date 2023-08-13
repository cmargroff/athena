
using UnityEngine;
public  class IndicatorBehavior:AthenaMonoBehavior
{
    public Transform Target;

    private MeshRenderer _renderer;
    private readonly float _activeWarningScale = 0.1f;

    protected override void Start()
    {
        base.Start();
        _renderer = GetComponent<MeshRenderer>();
    }
    protected override void PlausibleUpdate()
    {
        var mainCamera = Camera.main;
        var onScreen = mainCamera.CanSee(Target.transform, out Vector3 screenPosition);


        if (onScreen)
        {
            _renderer.enabled = false;
        }
        else
        {
            _renderer.enabled = true;

            var indicatorScreenPosition = new Vector3(Mathf.Clamp(screenPosition.x, 0, 1), Mathf.Clamp(screenPosition.y, 0, 1), screenPosition.z);


            var indicatorWorldPosition = mainCamera.ViewportToWorldPoint(indicatorScreenPosition);

            var targetDirection = mainCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, screenPosition.z)) - Target.position;
            var angle = Vector2.SignedAngle(Vector2.down, new Vector2(targetDirection.x, targetDirection.z));

            transform.SetPositionAndRotation(indicatorWorldPosition, Quaternion.Euler(90, 0, angle));// (Vector3.up,angle); //Quaternion.Euler(0, 0, angle) ;
            var size = _activeWarningScale * screenPosition.z;
            transform.localScale = new Vector3(size, size, size);
        }


    }
}


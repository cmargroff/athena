using UnityEngine;

public class SimpleRotateBehavior : AthenaMonoBehavior
{
    public float RotationSpeed = 1f;
    private Transform _transform;
    private float _rotation;
    void Start()
    {
        _transform = gameObject.transform;
    }
    protected override void PlausibleFixedUpdate()
    {
        _rotation = (RotationSpeed + _rotation) % 360;
        _transform.localRotation = Quaternion.Euler(0, 0, _rotation);
    }
}

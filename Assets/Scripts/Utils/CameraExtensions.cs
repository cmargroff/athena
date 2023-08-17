using UnityEngine;

public static class CameraExtensions
{
    public static bool CanSee(this Camera camera, Transform target)
    {
        return CanSee(camera, target, out _);
    }
    public static bool CanSee(this Camera camera, Transform target, out Vector3 targetScreenPosition)
    {
        targetScreenPosition = camera.WorldToViewportPoint(target.position);
        bool onScreen = targetScreenPosition is { z: > 0, x: > 0 and < 1, y: > 0 and < 1 };
        return onScreen;
    }
}
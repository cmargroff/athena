using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static  class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 v, float delta)
        {
            var rad = delta * Mathf.Deg2Rad;
            return new Vector2(
                v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
                v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
            );
        }
    }
}

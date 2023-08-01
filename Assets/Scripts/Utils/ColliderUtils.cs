using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class ColliderUtils
    {
        public static bool IsPointInsideCollider2D(Collider2D collider, Vector2 point)
        {
            if (collider is BoxCollider2D collider2D)
            {
                return IsPointInsideBoxCollider2D(collider2D, point);
            }
            else if (collider is CircleCollider2D circleCollider2D)
            {
                return IsPointInsideCircleCollider2D(circleCollider2D, point);
            }
            else if (collider is CapsuleCollider2D capsuleCollider2D)
            {
                return IsPointInsideCapsuleCollider2D(capsuleCollider2D, point);
            }
            // Handle other collider types here (e.g., PolygonCollider2D, EdgeCollider2D, etc.)

            // If the collider type is not supported or unknown, return false.
            return false;
        }

        private static bool IsPointInsideBoxCollider2D(BoxCollider2D collider, Vector2 point)
        {
            Vector2 localPoint = collider.transform.InverseTransformPoint(point);

            Vector2 boxHalfExtents = collider.size * 0.5f;

            // Check if the local point is within the box collider's boundaries.
            if (Mathf.Abs(localPoint.x) <= boxHalfExtents.x &&
                Mathf.Abs(localPoint.y) <= boxHalfExtents.y)
            {
                return true;
            }

            return false;
        }

        private static bool IsPointInsideCircleCollider2D(CircleCollider2D collider, Vector2 point)
        {
            Vector2 localPoint = collider.transform.InverseTransformPoint(point);

            // Check if the distance from the local point to the center of the circle is less than or equal to its radius.
            if (localPoint.magnitude <= collider.radius)
            {
                return true;
            }

            return false;
        }

        private static bool IsPointInsideCapsuleCollider2D(CapsuleCollider2D collider, Vector2 point)
        {
            Vector2 localPoint = collider.transform.InverseTransformPoint(point);

            Vector2 capsuleCenter = Vector2.zero;
            capsuleCenter.y = (collider.size.y * 0.5f) - collider.size.x * 0.5f;

            // Calculate the closest point on the capsule's central axis to the local point.
            Vector2 closestPoint = capsuleCenter;
            closestPoint.y += Mathf.Clamp(localPoint.y, -collider.size.y * 0.5f, collider.size.y * 0.5f);

            // Check if the distance from the local point to the closest point on the capsule's axis is less than or equal to its radius.
            if ((closestPoint - localPoint).sqrMagnitude <= collider.size.x * 0.5f * collider.size.x * 0.5f)
            {
                return true;
            }

            return false;
        }
        public static bool IsPointInsideCollider(Collider collider, Vector3 point)
        {
            if (collider is BoxCollider boxCollider)
            {
                return IsPointInsideBoxCollider(boxCollider, point);
            }
            else if (collider is SphereCollider sphereCollider)
            {
                return IsPointInsideSphereCollider(sphereCollider, point);
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                return IsPointInsideCapsuleCollider(capsuleCollider, point);
            }
            // Handle other collider types here (e.g., MeshCollider, TerrainCollider, etc.)

            // If the collider type is not supported or unknown, return false.
            return false;
        }

        private static bool IsPointInsideBoxCollider(BoxCollider collider, Vector3 point)
        {
            Vector3 localPoint = collider.transform.InverseTransformPoint(point);

            Vector3 boxHalfExtents = collider.size * 0.5f;

            // Check if the local point is within the box collider's boundaries.
            if (Mathf.Abs(localPoint.x) <= boxHalfExtents.x &&
                Mathf.Abs(localPoint.y) <= boxHalfExtents.y &&
                Mathf.Abs(localPoint.z) <= boxHalfExtents.z)
            {
                return true;
            }

            return false;
        }

        private static bool IsPointInsideSphereCollider(SphereCollider collider, Vector3 point)
        {
            Vector3 localPoint = collider.transform.InverseTransformPoint(point);

            // Check if the distance from the local point to the center of the sphere is less than or equal to its radius.
            if (localPoint.magnitude <= collider.radius)
            {
                return true;
            }

            return false;
        }

        private static bool IsPointInsideCapsuleCollider(CapsuleCollider collider, Vector3 point)
        {
            Vector3 localPoint = collider.transform.InverseTransformPoint(point);

            Vector3 capsuleCenter = Vector3.zero;
            capsuleCenter.y = (collider.height * 0.5f) - collider.radius;

            // Calculate the closest point on the capsule's central axis to the local point.
            Vector3 closestPoint = capsuleCenter;
            closestPoint.y += Mathf.Clamp(localPoint.y, -collider.height * 0.5f, collider.height * 0.5f);

            // Check if the distance from the local point to the closest point on the capsule's axis is less than or equal to its radius.
            if ((closestPoint - localPoint).sqrMagnitude <= collider.radius * collider.radius)
            {
                return true;
            }

            return false;
        }
    }
}

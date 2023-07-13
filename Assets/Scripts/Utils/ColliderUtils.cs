using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class ColliderUtils
    {
        public static bool IsPointInsideCollider(Collider collider, Vector3 point)
        {
            if (collider is BoxCollider)
            {
                return IsPointInsideBoxCollider((BoxCollider)collider, point);
            }
            else if (collider is SphereCollider)
            {
                return IsPointInsideSphereCollider((SphereCollider)collider, point);
            }
            else if (collider is CapsuleCollider)
            {
                return IsPointInsideCapsuleCollider((CapsuleCollider)collider, point);
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

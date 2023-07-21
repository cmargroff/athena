using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DamagingBehavior : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    public float Damage = 1;
    public float BaseKnockback = 1;
    public float KnockbackScaling = 1;
    public float KnockbackAngle = 0;
}

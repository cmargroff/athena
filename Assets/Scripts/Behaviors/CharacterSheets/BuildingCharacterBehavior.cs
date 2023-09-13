using System;
using UnityEngine;
using UnityEngine.Events;

public class BuildingCharacterBehavior : MonoBehaviour
{
    [Range(0.1f, 10)]
    public float Damage = 1f;
    [Range(0.1f, 10)]
    public float Knockback = 1f;
    [Range(0.0f, 10)]
    public float AttackFrequency = 0f;
    [Range(0.0f, 10)]
    public float BulletSize = 3f;
    public UnityEvent OnStatsChanged;
    void OnValidate()
    {
        OnStatsChanged?.Invoke();
    }
}
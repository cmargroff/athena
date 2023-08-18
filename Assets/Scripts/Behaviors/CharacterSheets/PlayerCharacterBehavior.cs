using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacterBehavior : MonoBehaviour
{
    [Range(0.1f, 10)]
    public float Damage = 1f;
    [Range(0.1f, 10)]
    public float Knockback = 1f;
    [Range(0.1f, 10)]
    public float Speed = 1f;
    [Range(0.1f, 10)]
    public float Armor = 1f;
    //public float AttackSpeed = 1f;
    //public float AttackDuration = 1f;
    [Range(0.1f, 10)]
    public float AttackFrequency = 1f;
    public UnityEvent OnStatsChanged;
    void OnValidate()
    {
        OnStatsChanged?.Invoke();
    }
}
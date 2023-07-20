using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "athena/Enemy", order = 0)]
public class EnemySO : ScriptableObject
{
    public string FriendlyName;
    public float Speed;
    public int Health;
    public int floatDamage;   
    public GameObject prefab;
}
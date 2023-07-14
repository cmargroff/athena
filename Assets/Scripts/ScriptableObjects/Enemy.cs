using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "athena/Enemy", order = 0)]
public class Enemy : ScriptableObject
{
  public int baseHp;
  public int baseSpeed;
  public GameObject weapon;
  public GameObject prefab;
}
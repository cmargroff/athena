using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "athena/Level", order = 0)]
public class LevelSO : ScriptableObject
{
    public List<EnemyTiming> EnemyTimings;
}
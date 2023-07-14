using System;
using UnityEngine;
using System.Collections.Generic;
[Serializable]
public class EnemyTiming {
  public int time;
  [Header("Should the enemy spawn as a boss?")]
  public bool bossSpawn = false;
  public List<Enemy> enemies;
}
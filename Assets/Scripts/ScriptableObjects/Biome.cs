using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Biome", menuName = "athena/Biome", order = 0)]

class Biome : ScriptableObject{
  public List<Tile> tiles;
  public EnemyScript enemies;
}
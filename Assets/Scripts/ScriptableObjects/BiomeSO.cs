using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Biome", menuName = "athena/Biome", order = 0)]

class BiomeSO : ScriptableObject{
  public List<TileSO> tiles;
  public EnemyScriptSO enemies;
}
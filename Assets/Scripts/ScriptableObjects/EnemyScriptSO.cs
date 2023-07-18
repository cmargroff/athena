using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Enemy Script", menuName = "athena/Enemy Script", order = 0)]
public class EnemyScriptSO : ScriptableObject {
  public List<EnemyTiming> entries;
}
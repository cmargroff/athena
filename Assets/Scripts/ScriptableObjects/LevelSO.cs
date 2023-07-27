using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "athena/Level", order = 0)]
public  class LevelSO:ScriptableObject 
{
    public List<EnemyTiming> EnemyTimings;
}


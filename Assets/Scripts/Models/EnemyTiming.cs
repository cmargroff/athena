using System;
using UnityEngine;
using System.Collections.Generic;
[Serializable]
public class EnemyTiming
{ public string Name;
  public int StartTime;
  public int EndTime;
  public float Rate;
  public EnemySO Enemy;
  public bool SingleSide;
  public float Aggressiveness;
  
  [HideInInspector]
    public GameManagerBehavior.TimedEvent Timer;
}
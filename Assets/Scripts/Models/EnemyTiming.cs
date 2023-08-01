using System;
using UnityEngine;

[Serializable]
public class EnemyTiming
{
    public string Name;
    public int StartTime;
    public int EndTime;
    public float Rate;
    public EnemySO Enemy;
    public SidesEnum Sides= SidesEnum.Random;
    [Range(0, 1)]
    public float Aggressiveness;

    [HideInInspector]
    public TimedEvent Timer;


    public enum SidesEnum
    {
        Random=0,
        Top=1,
        Bottom=2,
        Left=3,
        Right=4
    }
}
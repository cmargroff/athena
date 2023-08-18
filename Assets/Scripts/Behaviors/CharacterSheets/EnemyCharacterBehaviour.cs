using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


public class EnemyCharacterBehaviour : AthenaMonoBehavior
{
    [Range(0.1f, 10)]
    public float Damage = 1f;
    [Range(0.1f, 10)]
    public float Weight = 1f;
    [Range(0.1f, 10)]
    public float Speed = 1f;
    [Range(0.1f, 10)] 
    public float Health=1f;
    [Range(0.1f, 10)]
    public float SpawnFrequency = 1f;
    public UnityEvent OnStatsChanged;
    

    void OnValidate()
    {
        OnStatsChanged?.Invoke();
    }
}

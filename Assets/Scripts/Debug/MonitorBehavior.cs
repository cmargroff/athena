
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonitorBehavior:AthenaMonoBehavior
{
    
    [DebugGUIGraph(min: 0, max: 10000, r: 0, g: 1, b: 0, autoScale: true)]
    public float TotalEnemyHealth;
    [SerializeField]
    [DebugGUIGraph(min: 0, max: 10000, r: 0, g: 1, b: 1, autoScale: true)]
    public float DPS1 = 2f;

    [DebugGUIGraph(min: 0, max: 10000, r: 0, g: 1, b: 1, autoScale: true)]
    public float DPS10;

    private readonly Dictionary<int, float> _enemyHealths=new ();
    private readonly Dictionary<float, float> _playerDPS = new();

    protected override void Start()
    {
        base.Start();
        _gameManager.OnEnemyChanged.AddListener(EnemyChanged);
        _gameManager.OnEnemyDamaged.AddListener(EnemyDamaged);
        _gameManager.AddTimedEvent(1, () =>
        {
            TotalEnemyHealth = _enemyHealths.Sum(x => x.Value);
            DPS1 = _playerDPS.Where(x => x.Key > Time.realtimeSinceStartup - 1).Sum(x=>x.Value);
            DPS10 = _playerDPS.Where(x => x.Key > Time.realtimeSinceStartup - 10).Sum(x => x.Value)/Math.Min(10, Time.realtimeSinceStartup);

        }, this.gameObject);
    }

    private void EnemyDamaged(float damage)
    {
        _playerDPS.Add(Time.realtimeSinceStartup,damage);
    }

    //todo: only track enemies
    private void EnemyChanged(VulnerableBehavior vulnerable)
    {
        if (_enemyHealths.ContainsKey(vulnerable.GetInstanceID()))
        {
            if (vulnerable.Health > 0)
            {
                _enemyHealths[vulnerable.GetInstanceID()] = vulnerable.Health;
            }
            else
            {
                _enemyHealths.Remove(vulnerable.GetInstanceID());
            }
        }
        else
        {
            _enemyHealths.Add(vulnerable.GetInstanceID(),vulnerable.Health);
        }
    }
}


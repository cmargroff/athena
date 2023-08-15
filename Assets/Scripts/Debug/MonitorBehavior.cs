
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonitorBehavior:AthenaMonoBehavior
{
    [SerializeField]
    private float _totalEnemyHealth;

    private readonly Dictionary<int, float> _enemyHealths=new ();

    protected override void Start()
    {
        base.Start();
        _gameManager.OnEnemyChanged.AddListener(EnemyChanged);
        _gameManager.AddTimedEvent(1, () =>
        {
            _totalEnemyHealth = _enemyHealths.Sum(x => x.Value);
        }, this.gameObject);
    }

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


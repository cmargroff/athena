
using UnityEngine;

[RequireComponent(typeof(VulnerableBehavior))]
public class EnemyHealthMonitorBehavior : AthenaMonoBehavior, IAlive
{
    private VulnerableBehavior _vulnerable;

    protected override void Start()
    {
        base.Start();
        _vulnerable = GetComponent<VulnerableBehavior>();
        _vulnerable.OnHealthChanged.AddListener(HealthChanged);

    }

    private void HealthChanged(float damage)
    {
        _gameManager.OnEnemyHealthChanged.Invoke(_vulnerable,damage);
    }
}


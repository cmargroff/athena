
using UnityEngine;

[RequireComponent(typeof(VulnerableBehavior))]
public  class PlayerHealthMonitorBehavior :AthenaMonoBehavior, IAlive
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
            _gameManager.UpdatePlayerHealth(_vulnerable.Health / _vulnerable.MaxHealth);
        }
    }


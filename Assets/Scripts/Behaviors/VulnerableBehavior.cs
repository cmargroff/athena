using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableBehavior : AthenaMonoBehavior
{
    [SerializeField]
    private float _maxHealth = 1;
    [SerializeField]
    private float _health = 0;
    protected override void Start()
    {
        base.Start();
        _health = _maxHealth;

    }

    // Update is called once per frame
    protected override void PausibleUpdate()
    {

    }
    private void OnParticleTrigger()
    { 
    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<DamagingBehavior>(out var damaging))
        {
            _health -= damaging.Damage;
            if (_health < 1)
            {
                Destroy(gameObject); //todo when this is pooled change to disable
            }
        }
    }
}

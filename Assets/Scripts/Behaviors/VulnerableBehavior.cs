using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableBehavior : AthenaMonoBehavior
{
    [SerializeField]
    public float MaxHealth = 1;
    [SerializeField]
    private float _health = 0;
    [SerializeField]
    private LifebarBehavior _lifebar;

    [SerializeField]
    private float _ITime=1f/3f;
    private float _hitLast =0;

    [SerializeField]
    private LayerMask _damagingLayers;


    protected override void Start()
    {
        base.Start();
        _health = MaxHealth;
        
    }

    protected override void OnActive()
    {
        base.OnActive();
        _health = MaxHealth;
        _lifebar.SetHealthPercent(_health / MaxHealth);
    }
    // Update is called once per frame
    protected override void PausibleUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damage(other);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Damage(other);
    }

    private void Damage(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<DamagingBehavior>(out var damaging))
        {
            if ((_damagingLayers.value & (1 << other.gameObject.layer)) != 0)
            {
                if (_hitLast + _ITime < Time.realtimeSinceStartup)
                {
                    _hitLast = Time.realtimeSinceStartup;
                    _health -= damaging.Damage;

                    if (_lifebar != null)
                    {
                        _lifebar.SetHealthPercent(_health / MaxHealth);
                    }

                    if (_health < 1)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}

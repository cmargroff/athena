using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(StatAdjust))]
public class VulnerableBehavior : AthenaMonoBehavior, IAlive
{
    private StatAdjust _statAdjust;
    [SerializeField]
    public float MaxHealth = 1;
    public int Weight = 1;
    public int Friction = 1;
    [SerializeField]
    private float _health = 0;
    [SerializeField]
    private LifebarBehavior _lifebar;

    [SerializeField]
    private float _ITime = 1f / 3f;
    private float _hitLast = 0;

    [SerializeField]
    private LayerMask _damagingLayers;
    private bool _hitstun = false;
    private Vector3 _knockbackVector = Vector3.zero;
    private float _knockback = 0;

    private AudioSource _audioSource;
    private RewardDropBehavior _rewards;

    protected override void Start()
    {
        base.Start();
        _rewards = GetComponent<RewardDropBehavior>();
        _statAdjust = GetComponent<StatAdjust>();
    }

    public override void OnActive()
    {
        base.OnActive();
        _audioSource = GetComponent<AudioSource>();
        _health = MaxHealth;
        _lifebar.SetHealthPercent(_health / MaxHealth);
        _knockback = 0;
    }
    // Update is called once per frame
    protected override void PausibleUpdate()
    {
        if (_hitstun)
        {
            transform.position += new Vector3(_knockbackVector.x * _knockback, _knockbackVector.y * _knockback, 0);
            _knockback -= _gameManager.KnockbackFriction; // friction
            if (_knockback < 0)
            {
                _hitstun = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<DamagingBehavior>(out var damaging))
        {
            if ((_damagingLayers.value & (1 << other.gameObject.layer)) != 0)
            {
                if (_hitLast + _ITime < Time.realtimeSinceStartup)
                {
                    _hitLast = Time.realtimeSinceStartup;
                    _health -= damaging.Damage/ _statAdjust?.GetArmorAdjust()??1f;//todo:this is a hack, to tiered to fix right now

                    if (_lifebar != null)
                    {
                        _lifebar.SetHealthPercent(_health / MaxHealth);

                    }

                    //if (_health >=0)
                    //{
                    if (_audioSource != null)
                    {
                        damaging.HitSound.Play(_audioSource);
                    }
                    else
                    {
                        Debug.LogWarning("Empty _audioSource tried to play sound");
                    }
                    //}


                    if (_health < 1)
                    {
                        if (_rewards != null)
                        {
                            _rewards.DropRewards();
                        }
                        if (_stateMachine != null)
                        {
                            _stateMachine.SetState(gameObject, typeof(IDeath));
                        }
                        else
                        { 
                        
                        }
                    }
                    else
                    {
                        _knockbackVector = damaging.GetKnockbackAngle();
                        _knockback = CalculateKnockback(damaging.Knockback);
                        _hitstun = true;
                    }

                }
            }
        }
    }
    private float CalculateKnockback(float knockback)
    {

        return knockback / Weight * _gameManager.KnockbackFactor;
    }
    private void Damage(Collider2D other)
    {
       
    }

}
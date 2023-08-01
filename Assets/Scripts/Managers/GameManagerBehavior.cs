using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerCharacterBehavior))]
public class GameManagerBehavior : AthenaMonoBehavior
{
    public bool Paused;

    public Collider2D Bounds;
    public GameObject Player;
    public GameObject Weapons;

    public PoolBehavior Pool;
    public LayerMask Enemies;
    public LayerMask Buildings;

    public ShopBehavior Shop;

    [SerializeField]
    private readonly Dictionary<Guid,TimedEvent> _timedEvents= new ();

    private Int64 _frameCount = 1;


    public float KnockbackFriction = 0.1f;
    public float KnockbackFactor = 1f;
    public Dictionary<string, int> Pickups = new ();
    public event Action<string, int> OnPickupCollected;

    [FormerlySerializedAs("PlayerCharacterBehavior")] 
    public PlayerCharacterBehavior PlayerCharacter;

    protected override void Awake()
    {
        base.Awake();
        PlayerCharacter = GetComponent<PlayerCharacterBehavior>();
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        DOTween.SetTweensCapacity(10000, 10000);

        SafeAssigned(Bounds);
        SafeAssigned(Shop);
        SafeAssigned(Player);
        SafeAssigned(Weapons);

        //AddTimedEvent(1f,()=>Debug.Log($"Timed event {Time.timeSinceLevelLoad}"));
    }

    // Update is called once per frame

    public TimedEvent AddTimedEvent(float seconds, Action action, GameObject owner)
    {
        var te = new TimedEvent() {
            Id = Guid.NewGuid(),
            Action = action,
            Owner = owner
        };
        te.SetFramesInSeconds(seconds);
        _timedEvents.Add(te.Id, te);
        return te;
    }
    public void RemoveTimedEvent(TimedEvent te)
    {
        _timedEvents.Remove(te.Id);
    }

    protected override void PlausibleFixedUpdate()
    {
        foreach (var kv in _timedEvents)
        {
            if (kv.Value.Owner.activeInHierarchy)
            {
                if (_frameCount % kv.Value.Frames == 0)
                {
                    kv.Value.Action();
                }
            }
        }
        _frameCount++;
    }



    public void CollectPickup(PickupBehavior pickup)
    {
        if (Pickups.ContainsKey(pickup.Name))
        {
            Pickups[pickup.Name] += pickup.Amount;
        }
        else
        {
            Pickups.Add(pickup.Name, pickup.Amount);
        }
        OnPickupCollected?.Invoke(pickup.Name, Pickups[pickup.Name]);


        pickup.Kill();
    }
}

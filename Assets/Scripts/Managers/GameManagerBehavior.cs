using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManagerBehavior : AthenaMonoBehavior
{
    public bool Paused;

    public Collider2D Bounds;
    public GameObject Player;
    public GameObject Pickup;

    public PoolBehavior Pool;


    [SerializeField]
    private Dictionary<Guid,TimedEvent> _timedEvents= new Dictionary<Guid, TimedEvent>();

    private Int64 _frameCount = 0;


    public float KnockbackFriction = 0.1f;
    public float KnockbackFactor = 1f;
    public Dictionary<PickupType, int> Pickups = new Dictionary<PickupType, int>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        DOTween.SetTweensCapacity(10000, 10000);

        SafeAssigned(Bounds);


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


    protected override void PausibleFixedUpdate()
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

    public class TimedEvent
    {
        public void SetFramesInSeconds(float seconds)
        {
            Frames = (int)Math.Ceiling(seconds / Time.fixedDeltaTime);
        }

        public Guid Id;
        public int Frames;
        public Action Action;
        public GameObject Owner;
    }
    public void SpawnPickup(PickupType type, int Amount, Vector3 position, Quaternion rotation)
    {
        var pickup = Pool.GetPooledObject(Pickup, position, rotation);
        var pickupBehavior = pickup.GetComponent<PickupBehavior>();
        pickupBehavior.Type = type;
    }
    public void CollectPickup(PickupBehavior pickup)
    {
        if (Pickups.ContainsKey(pickup.Type))
        {
            Pickups[pickup.Type] += pickup.Amount;
        }
        else
        {
            Pickups.Add(pickup.Type, pickup.Amount);
        }
        pickup.gameObject.SetActive(false);
    }
}

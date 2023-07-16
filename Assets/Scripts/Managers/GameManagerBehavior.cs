using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerBehavior : AthenaMonoBehavior
{
    public bool Paused;

    public Collider2D Bounds;
    public GameObject Player;

    public PoolBehavior Pool;


    [SerializeField]
    private Dictionary<Guid,TimedEvent> _timedEvents= new Dictionary<Guid, TimedEvent>();

    private Int64 _frameCount = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
}

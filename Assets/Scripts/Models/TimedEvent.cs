using System;
using UnityEngine;

public class TimedEvent
{
    public void SetFramesInSeconds(float seconds)
    {
        Frames = (int)Math.Ceiling((1/seconds) / Time.fixedDeltaTime);
    }

    public Guid Id;
    public int Frames;
    public Action Action;
    public GameObject Owner;
}
using System;
using UnityEngine;

public class TimedEvent
{
    public void SetFramesInSeconds(float seconds,uint currentFrame)
    {
        Frames = (uint)Math.Ceiling((1/seconds) / Time.fixedDeltaTime);
        offset = currentFrame % Frames;
    }

    public bool IsActiveFrame(uint currentFrame)
    {
        return Owner.activeInHierarchy&&(currentFrame + offset) % Frames == 0;
    }

    public Guid Id;
    public uint Frames;
    public Action Action;
    public GameObject Owner;
    public uint offset;
}
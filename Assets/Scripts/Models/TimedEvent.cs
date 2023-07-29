using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
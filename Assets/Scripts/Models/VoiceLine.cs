using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]
public  class VoiceLine
{
    public AudioClip VoiceClip;
    [TextArea(5, 20)]
    public string Text;

    public float Time;

    public Sprite Portrait;
}

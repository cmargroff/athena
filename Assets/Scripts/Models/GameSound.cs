﻿using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class GameSound
    {
        public AudioClip Sound;
        [Range(0f, 1f)]
        public float Volume = 1;
        public void Play(AudioSource source)
        {
            source.PlayOneShot(Sound, Volume);
        }
    }
}
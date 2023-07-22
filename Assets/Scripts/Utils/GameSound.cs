using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public  class GameSound
    {
        public AudioClip FireSound;
        [Range(0f, 1f)]
        public float Volume=1;
        public void Play(AudioSource source)
        {
            source.PlayOneShot(FireSound, Volume);
        }
    }
}

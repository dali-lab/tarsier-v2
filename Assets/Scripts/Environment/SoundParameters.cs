using System;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Environment
{
    /// <summary>
    /// Small class holding modifiable sound parameters
    /// </summary>
    [Serializable]
    public class SoundParameters
    {
        public Animal animal; //animal that has these parameters
        public bool mute = false; //whether the sound should be muted or not
        public AudioClip audioClip; //audio clip to play when this animal is switched to
        [Range(0, 1)] public float volume; //volume of audio clip
    }

}
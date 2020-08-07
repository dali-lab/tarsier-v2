using System;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Environment
{
    [Serializable]
    public class SoundParameters
    {
        public Animal animal;
        public bool mute = false;
        public AudioClip audioClip;
        [Range(0, 1)] public float volume;
    }

}
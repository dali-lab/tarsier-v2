using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Environment
{
    /// <summary>
    /// This script manages sound switching when animals switch
    /// Add it to any game object with an audio source
    /// Can add multiple to a game object if you have multiple audio sources there that you want to control
    /// </summary>
    public class SoundController : MonoBehaviour
    {
        public AudioSource AudioSource;
        public SoundParameters[] SoundParametersList;
        public bool playOnSwitch = true; //whether to play the sound clip immediately when animal is switched
        private SoundParameters originalParameters;
        private Dictionary<Animal, SoundParameters> soundParametersDict;
        
        private void Awake()
        {
            if (AudioSource == null) AudioSource = GetComponent<AudioSource>();
            soundParametersDict = new Dictionary<Animal, SoundParameters>();
            foreach (SoundParameters s in SoundParametersList)
            {
                if (!soundParametersDict.ContainsKey(s.animal))
                {
                    soundParametersDict.Add(s.animal, s);
                }
            }
            
            originalParameters = new SoundParameters();
            originalParameters.mute = AudioSource.mute;
            originalParameters.volume = AudioSource.volume;
            originalParameters.audioClip = AudioSource.clip;
        }

        private void OnEnable()
        {
            AnimalManager.Instance.AnimalSwitch.AddListener(SetSound);
        }

        private void OnDisable()
        {
            AnimalManager.Instance.AnimalSwitch.RemoveListener(SetSound);
        }

        private void SetSound(Animal animal)
        {
            SoundParameters parameters;
            if (AudioSource.isPlaying)
            {
                AudioSource.Stop();
            }
            
            //see if there are sound parameters for this animal
            if (soundParametersDict.TryGetValue(animal, out parameters))
            {
                AudioSource.mute = parameters.mute;
                AudioSource.volume = parameters.volume;
                AudioSource.clip = parameters.audioClip;
            }
            else
            {
                //no sound parameters set, so revert to original
                AudioSource.mute = originalParameters.mute;
                AudioSource.volume = originalParameters.volume;
                AudioSource.clip = originalParameters.audioClip;
            }

            if (playOnSwitch)
            {
                AudioSource.Play();
            }
        }
    }
}
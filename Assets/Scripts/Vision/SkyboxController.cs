using System;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    public class SkyboxController : MonoBehaviour
    {
        private Material originalSkyboxMaterial;
        private Dictionary<VisionEffect, SkyboxEffect> effectsDictionary;
        private AnimalManager _animalManager;

        private void Awake()
        {
            originalSkyboxMaterial = RenderSettings.skybox;
            
            effectsDictionary = new Dictionary<VisionEffect, SkyboxEffect>();
            SkyboxEffect[] effectsList = GetComponents<SkyboxEffect>();
            foreach (SkyboxEffect effect in effectsList)
            {
                if (!effectsDictionary.ContainsKey(effect.Effect))
                {
                    effectsDictionary.Add(effect.Effect, effect);
                }
            }

        }

        private void Start()
        {
            _animalManager = AnimalManager.Instance;

            if (_animalManager != null)
            {
                _animalManager.VisionSwitch.AddListener(ApplyEffects);
            }
        }

        private void ApplyEffects(VisionParameters parameters)
        {
            RevertToOriginal();
            
            foreach (KeyValuePair<VisionEffect, SkyboxEffect> effect in effectsDictionary)
            {
                effect.Value.ApplyEffect(parameters);
            }
        }

        private void RevertToOriginal()
        {
            RenderSettings.skybox = originalSkyboxMaterial;
        }
    }
}
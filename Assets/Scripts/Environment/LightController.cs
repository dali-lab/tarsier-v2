using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Environment
{
    /// <summary>
    /// Class that controls a light's settings when animal is switched
    /// </summary>
    public class LightController : MonoBehaviour
    {
        public Light light;
        public LightParameters[] LightParametersList;

        private LightParameters originalParameters;
        private Dictionary<Animal, LightParameters> lightParametersDict;

        private void Awake()
        {
            if (light == null) light = GetComponent<Light>();
            lightParametersDict = new Dictionary<Animal, LightParameters>();
            foreach (LightParameters l in LightParametersList)
            {
                if (!lightParametersDict.ContainsKey(l.animal))
                {
                    lightParametersDict.Add(l.animal, l);
                }
            }
            
            originalParameters = new LightParameters();
            originalParameters.color = light.color;
            originalParameters.intensity = light.intensity;
        }

        private void OnEnable()
        {
            AnimalManager.Instance.AnimalSwitch.AddListener(SetLight);
        }

        private void OnDisable()
        {
            AnimalManager.Instance.AnimalSwitch.RemoveListener(SetLight);
        }

        private void SetLight(Animal animal)
        {
            LightParameters parameters;
            //try to see if light parameters are set for this animal
            if (lightParametersDict.TryGetValue(animal, out parameters))
            {
                light.intensity = parameters.intensity;
                light.color = parameters.color;
            }
            else
            {
                //light parameters not set, so revert to original
                light.intensity = originalParameters.intensity;
                light.color = originalParameters.color;
            }
        }
    }
}
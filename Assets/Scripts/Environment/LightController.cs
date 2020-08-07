using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Environment
{
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
            if (lightParametersDict.TryGetValue(animal, out parameters))
            {
                light.intensity = parameters.intensity;
                light.color = parameters.color;
            }
            else
            {
                light.intensity = originalParameters.intensity;
                light.color = originalParameters.color;
            }
        }
    }
}
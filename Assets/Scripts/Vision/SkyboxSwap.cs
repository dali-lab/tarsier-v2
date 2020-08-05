using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    public class SkyboxSwap : MonoBehaviour
    {
        public MaterialSwap.MaterialGroup skyboxes = new MaterialSwap.MaterialGroup(); //materials for skybox for various animals

        private AnimalManager _animalManager;
        private Dictionary<Animal, Material> skyboxDictionary;

        private void Awake()
        {
            foreach (MaterialSwap.MaterialAnimal m in skyboxes.MaterialsToSwap)
            {
                if (!skyboxDictionary.ContainsKey(m.animal))
                {
                    skyboxDictionary.Add(m.animal, m.material);
                }
            }
        }

        private void Start()
        {
            _animalManager = AnimalManager.Instance;

            if (_animalManager != null)
            {
                _animalManager.VisionSwitch.AddListener(SwapSkybox);
            }
        }

        private void SwapSkybox(VisionParameters parameters)                                       
        {

            if (parameters.visionEffects.Contains(VisionEffect.MaterialSwap))
            {
                Material materialToSwapTo;
                if (skyboxDictionary.TryGetValue(parameters.Animal, out materialToSwapTo))
                {
                    RenderSettings.skybox = materialToSwapTo;
                    return;
                }
            }

            RenderSettings.skybox = skyboxes.originalMaterial;
        }
    }
}
    




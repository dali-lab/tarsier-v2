using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    //swaps skybox for the skybox associated with an animal
    public class SkyboxSwap : SkyboxEffect
    {
        public List<MaterialSwap.MaterialAnimal> skyboxes = new List<MaterialSwap.MaterialAnimal>(); //materials for skybox for various animals
        public override VisionEffect Effect => VisionEffect.MaterialSwap;
        
        private AnimalManager _animalManager;
        private Dictionary<Animal, Material> skyboxDictionary;
        private Material _originalMaterial;

        private void Awake()
        {
            //create new dictionary of animal to skybox material
            skyboxDictionary = new Dictionary<Animal, Material>();
            foreach (MaterialSwap.MaterialAnimal m in skyboxes)
            {
                if (!skyboxDictionary.ContainsKey(m.animal))
                {
                    skyboxDictionary.Add(m.animal, m.material);
                }
            }
            
            //save original material
            _originalMaterial = RenderSettings.skybox;
        }
        
        //apply skybox swap
        public override void ApplyEffect(VisionParameters parameters)
        {
            Material materialToSwapTo;
            if (skyboxDictionary.TryGetValue(parameters.Animal, out materialToSwapTo))
            {
                RenderSettings.skybox = materialToSwapTo;
                return;
            }

            RenderSettings.skybox = _originalMaterial;
        }
    }
}
    




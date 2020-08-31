using UnityEngine;
using System;
using System.Collections.Generic;
using Anivision.Core;

namespace Anivision.Vision
{
    /// <summary>
    /// Swaps materials depending on which type of animal
    /// </summary>
    [RequireComponent(typeof(MaterialController))]
    public class MaterialSwap : MaterialEffect
    {
        //What materials to swap between
        public MaterialGroup[] MaterialGroups;
        public override VisionEffect Effect => VisionEffect.MaterialSwap;

        private Dictionary<string, Dictionary<Animal, Material>> materialsDictionary; //original material to materials to swap to
        private Dictionary<string, Material> reverseMaterialDictionary; //material to original material for faster lookup

        private void Awake()
        {
            materialsDictionary = new Dictionary<string, Dictionary<Animal, Material>>();
            reverseMaterialDictionary = new Dictionary<string, Material>();
            ConstructMaterialDictionaries(MaterialGroups, materialsDictionary, reverseMaterialDictionary);
        }

        /// <summary>
        /// Function looks through the dictionary of materials to see, based on the current material and animal, which
        /// material should be swapped in
        /// </summary>
        /// <param name="propBlock"></param>
        /// <param name="currentMaterial"></param>
        /// <param name="renderer"></param>
        /// <param name="rendererNewMaterials"></param>
        /// <param name="visionParameters"></param>
        public override void ApplyEffect(MaterialPropertyBlock propBlock, int currentMaterialIndex, Renderer currentRenderer,
            VisionParameters visionParameters)
        {
            propBlock.Clear();
            currentRenderer.SetPropertyBlock(propBlock, currentMaterialIndex);
            Material swapMaterial = currentRenderer.sharedMaterials[currentMaterialIndex];
            Material originalMaterial = swapMaterial;
            if (!materialsDictionary.ContainsKey(GetMaterialName(originalMaterial)))
            {
                originalMaterial = GetOriginalMaterial(originalMaterial);
            }

            if (originalMaterial != null)
            {
                swapMaterial = GetMaterialToSwapTo(originalMaterial, visionParameters.Animal);
            }

            Material[] newMaterials = currentRenderer.materials;
            newMaterials[currentMaterialIndex] = swapMaterial;
            currentRenderer.sharedMaterials = newMaterials;
        }

        private void ConstructMaterialDictionaries(MaterialGroup[] materialGroupsList, Dictionary<string, Dictionary<Animal, Material>> matDictionary, Dictionary<string, Material> reverseMatDictionary)
        {
            if (materialGroupsList != null && materialGroupsList.Length > 0)
            {
                matDictionary.Clear();
                reverseMatDictionary.Clear();
                foreach (MaterialGroup m in materialGroupsList)
                {
                    if (!matDictionary.ContainsKey(GetMaterialName(m.originalMaterial)))
                    {
                        Dictionary<Animal, Material> valueDict = new Dictionary<Animal, Material>();
                        foreach (MaterialAnimal materialAnimal in m.MaterialsToSwap)
                        {

                            if (!valueDict.ContainsKey(materialAnimal.animal))
                            {
                                valueDict.Add(materialAnimal.animal, materialAnimal.material);
                            }

                            if (!reverseMatDictionary.ContainsKey(GetMaterialName(materialAnimal.material)))
                            {
                                reverseMatDictionary.Add(GetMaterialName(materialAnimal.material), m.originalMaterial);
                            }
                        }

                        matDictionary.Add(GetMaterialName(m.originalMaterial), valueDict);
                    }
                }
            }
        }
    
        /// <summary>
        /// Given a material, looks up what the original material is
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public Material GetOriginalMaterial(Material material)
        {
            Material originalMaterial;
        
            if (reverseMaterialDictionary.TryGetValue(GetMaterialName(material), out originalMaterial) == false)
            {
                if (materialsDictionary.ContainsKey(GetMaterialName(material)))
                {
                    return material;
                }
            }
            return originalMaterial;
        }
    
        /// <summary>
        /// Given a material and an animal, looks up what material should be swapped in
        /// </summary>
        /// <param name="originalMaterial"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        public Material GetMaterialToSwapTo(Material originalMaterial, Animal animal)
        {
            Dictionary<Animal, Material> materialsSwap;
            Material returnMaterial;
            if (!materialsDictionary.ContainsKey(GetMaterialName(originalMaterial)))
            {
                return null;
            }

            materialsDictionary.TryGetValue(GetMaterialName(originalMaterial), out materialsSwap);
            materialsSwap.TryGetValue(animal, out returnMaterial);
            return returnMaterial;
        }
    
        /// <summary>
        /// Reverts all materials of a renderer to their original materials
        /// </summary>
        /// <param name="r"></param>
        public override void RevertToOriginal(Renderer r)
        {
            Material[] currentMaterials = r.sharedMaterials;
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            for (int i = 0; i < currentMaterials.Length; i++)
            {
                Material originalMaterial = GetOriginalMaterial(currentMaterials[i]);

                if (originalMaterial != null)
                {
                    currentMaterials[i] = originalMaterial;
                }
                
                r.SetPropertyBlock(propertyBlock, i);
            }

            r.sharedMaterials = currentMaterials;

        }
    }
}
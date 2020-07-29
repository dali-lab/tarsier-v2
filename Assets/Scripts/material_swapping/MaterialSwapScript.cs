using UnityEngine;
using System;
using System.Collections.Generic;
using Anivision.Core;
using Anivision.Vision;

[RequireComponent(typeof(MaterialController))]
public class MaterialSwapScript : MaterialEffect
{
    public Material[] nUVmats;
    public Material[] UVmats;
    public MaterialGroup[] MaterialGroups;
    public override VisionEffect Effect => VisionEffect.MaterialSwap;

    private Dictionary<Material, Dictionary<Animal, Material>> materialsDictionary;
    private Dictionary<Material, Material> reverseMaterialDictionary;

    private void Awake()
    {
        materialsDictionary = new Dictionary<Material, Dictionary<Animal, Material>>();
        reverseMaterialDictionary = new Dictionary<Material, Material>();
        ConstructMaterialDictionaries(MaterialGroups, materialsDictionary, reverseMaterialDictionary);
    }

    void OnEnable()
    {
        MaterialEventManager.OnMaterialSwap += SwapMaterial;
    }

    void OnDisable()
    {
        MaterialEventManager.OnMaterialSwap -= SwapMaterial;
    }

    int GetIndexOfMaterial(Material mat)
    {
        for(int i = 0; i < nUVmats.Length; i++) {
            if((nUVmats[i].name + " (Instance)").Equals(mat.name)) {
                return i;
            }
            if((UVmats[i].name + " (Instance)").Equals(mat.name)) {
                return i;
            }
        }
        return -1;
    }

    void SwapMaterial(bool uvMode)
    {
        SwapMaterialRecursive(gameObject.transform, uvMode);
    }

    void SwapMaterialRecursive(Transform t, bool uvMode)
    {
        GameObject currGameObject = t.gameObject;
        // if current gameobject has a renderer
        if(currGameObject.GetComponent<Renderer>() != null){
            // switch to the correct texture, if valid and switchable
            int index = GetIndexOfMaterial(currGameObject.GetComponent<Renderer>().material);
            if (index > -1)
            {
                if (uvMode)
                {
                    currGameObject.GetComponent<Renderer>().material = UVmats[index];
                }
                else
                {
                    currGameObject.GetComponent<Renderer>().material = nUVmats[index];
                }
            }
        }
        // recurse over children
        foreach( Transform child in t) {
            SwapMaterialRecursive(child, uvMode);
        }
    }

    public override void ApplyEffect(MaterialPropertyBlock propBlock, Material currentMaterial, List<Material> rendererNewMaterials,
        VisionParameters visionParameters)
    {
        Material swapMaterial = currentMaterial;
        Material originalMaterial = currentMaterial;
        if (!materialsDictionary.ContainsKey(originalMaterial))
        {
            originalMaterial = GetOriginalMaterial(originalMaterial);
        }

        if (originalMaterial != null)
        {
            swapMaterial = GetMaterialToSwapTo(originalMaterial, visionParameters.Animal);
        }
        
        rendererNewMaterials.Add(swapMaterial);

    }

    private void ConstructMaterialDictionaries(MaterialGroup[] materialGroupsList, Dictionary<Material, Dictionary<Animal, Material>> matDictionary, Dictionary<Material, Material> reverseMatDictionary)
    {
        if (materialGroupsList != null && materialGroupsList.Length > 0)
        {
            matDictionary.Clear();
            reverseMatDictionary.Clear();
            foreach (MaterialGroup m in materialGroupsList)
            {
                if (!matDictionary.ContainsKey(m.originalMaterial))
                {
                    Dictionary<Animal, Material> valueDict = new Dictionary<Animal, Material>();
                    foreach (MaterialAnimal materialAnimal in m.MaterialsToSwap)
                    {
                        // Debug.Log(m.originalMaterial.name);
                        // Debug.Log(materialAnimal.animal);
                        // Debug.Log(materialAnimal.material.name);
                        if (!valueDict.ContainsKey(materialAnimal.animal))
                        {
                            valueDict.Add(materialAnimal.animal, materialAnimal.material);
                        }

                        if (!reverseMatDictionary.ContainsKey(materialAnimal.material))
                        {
                            reverseMatDictionary.Add(materialAnimal.material, m.originalMaterial);
                        }
                    }

                    matDictionary.Add(m.originalMaterial, valueDict);
                }
            }
        }
    }

    public Material GetOriginalMaterial(Material material)
    {
        Material originalMaterial;
        
        if (reverseMaterialDictionary.TryGetValue(material, out originalMaterial) == false)
        {
            if (materialsDictionary.ContainsKey(material))
            {
                Debug.Log(material.name);
                return material;
            }
        }
        // Debug.Log(originalMaterial == null);
        return originalMaterial;
    }

    public Material GetMaterialToSwapTo(Material originalMaterial, Animal animal)
    {
        Dictionary<Animal, Material> materialsSwap;
        Material returnMaterial;
        if (!materialsDictionary.ContainsKey(originalMaterial))
        {
            return null;
        }

        materialsDictionary.TryGetValue(originalMaterial, out materialsSwap);
        materialsSwap.TryGetValue(animal, out returnMaterial);
        return returnMaterial;
    }

    public override void RevertToOriginal(Renderer r)
    {
        Material[] currentMaterials = r.sharedMaterials;
        Material[] originalMaterials = new Material[currentMaterials.Length];
        for (int i = 0; i < currentMaterials.Length; i++)
        {
            Debug.Log(currentMaterials[i].name);
            originalMaterials[i] = GetOriginalMaterial(currentMaterials[i]);
        }

        r.materials = originalMaterials;
    }

    [Serializable]
    public class MaterialAnimal
    {
        public Animal animal;
        public Material material;
    }

    [Serializable]
    public class MaterialGroup
    {
        public Material originalMaterial;
        public MaterialAnimal[] MaterialsToSwap;
    }
}
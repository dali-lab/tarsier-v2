using System;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    public class MaterialController : MonoBehaviour
    {
        private Dictionary<VisionEffect, MaterialEffect> effects;
        private AnimalManager _animalManager;

        private void Awake()
        {
            effects = new Dictionary<VisionEffect, MaterialEffect>();
            MaterialEffect[] effectsList = GetComponents<MaterialEffect>();
            foreach (MaterialEffect effect in effectsList)
            {
                if (!effects.ContainsKey(effect.Effect))
                {
                    effects.Add(effect.Effect, effect);
                }
            }
        }

        private void Start()
        {
            _animalManager = AnimalManager.Instance;
            
            if (_animalManager == null)
            {
                throw new Exception("There must be an instance of the AnimalManager script in the scene");
            }
            else
            {
                _animalManager.VisionSwitch.AddListener(MaterialChange);
            }
        }

        private void MaterialChange(VisionParameters visionParameters)
        {
            MaterialChangeRecursive(gameObject.transform, visionParameters);
        }

        private void MaterialChangeRecursive(Transform t, VisionParameters visionParameters)
        {
            GameObject currGameObject = t.gameObject;
            Renderer mRenderer = currGameObject.GetComponent<Renderer>();
            
            // if current game object has a renderer
            if (mRenderer != null)
            {
                List<Material> newMaterialsList = new List<Material>();
                RevertRendererToOriginal(mRenderer);
                for (int i = 0; i < mRenderer.materials.Length; i++)
                {
                    Material mat = mRenderer.materials[i];
                    Material currentSharedMaterial = mRenderer.sharedMaterials[i];
                    MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                    mRenderer.GetPropertyBlock(propBlock, i);
                    foreach (VisionEffect effect in visionParameters.visionEffects)
                    {
                        if (effects.ContainsKey(effect))
                        {
                            effects[effect].ApplyEffect(propBlock, currentSharedMaterial, newMaterialsList, visionParameters);
                        }
                    }
                    mRenderer.SetPropertyBlock(propBlock, i);
                }

                if (newMaterialsList.Count > 0)
                {
                    mRenderer.materials = newMaterialsList.ToArray();
                }
               
            }
            // recurse over children
            foreach(Transform child in t) {
                MaterialChangeRecursive(child, visionParameters);
            }
        }

        private void RevertRendererToOriginal(Renderer r)
        {
            foreach (KeyValuePair<VisionEffect, MaterialEffect> effect in effects)
            {
                effect.Value.RevertToOriginal(r);
            }
        }
    }
}
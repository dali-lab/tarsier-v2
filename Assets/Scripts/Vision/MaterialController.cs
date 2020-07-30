using System;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    /// <summary>
    /// Controller class to apply all of the relevant material effects that the object supports
    /// </summary>
    public class MaterialController : MonoBehaviour
    {
        private Dictionary<VisionEffect, MaterialEffect> effects; //get all of the effects attached to this game object
        private AnimalManager _animalManager;

        private List<VisionEffect> lastAppliedEffects;

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
            // RevertToOriginalRecursive(transform);
            MaterialChangeRecursive(gameObject.transform, visionParameters);
            lastAppliedEffects = visionParameters.visionEffects;
        }

        /// <summary>
        /// Recurses through all of the child transforms and applies the relevant effects
        /// </summary>
        /// <param name="t"></param>
        /// <param name="visionParameters"></param>
        private void MaterialChangeRecursive(Transform t, VisionParameters visionParameters)
        {
            GameObject currGameObject = t.gameObject;
            Renderer mRenderer = currGameObject.GetComponent<Renderer>();
            
            // if current game object has a renderer
            if (mRenderer != null)
            {
                RevertToOriginal(mRenderer); //revert materials to original before applying new changes
                //go through renderer materials
                for (int i = 0; i < mRenderer.sharedMaterials.Length; i++)
                {
                    MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                    mRenderer.GetPropertyBlock(propBlock, i);
                    //go through list of effects for this animal and apply effect if this effect is on the object
                    foreach (VisionEffect effect in visionParameters.visionEffects)
                    {
                        if (effects.ContainsKey(effect))
                        {
                            effects[effect].ApplyEffect(propBlock, i, mRenderer, visionParameters);
                        }
                    }
                    mRenderer.SetPropertyBlock(propBlock, i);
                }

            }
            // recurse over children
            foreach(Transform child in t) {
                MaterialChangeRecursive(child, visionParameters);
            }
        }
        
        //revert current renderer to original
        private void RevertToOriginal(Renderer r)
        {
            if (r != null)
            {
                
                if (lastAppliedEffects != null && lastAppliedEffects.Count > 0)
                {
                    
                    int i = lastAppliedEffects.Count - 1;
                    
                    while (i >= 0)
                    {
                        VisionEffect effect = lastAppliedEffects[i];
                        if (effects.ContainsKey(effect))
                        {
                            effects[effect].RevertToOriginal(r);
                        }
                        i--;
                    }
                }
            }
           
        }
        
        //recurse through children to revert renderers to original
        private void RevertToOriginalRecursive(Transform t)
        {
            GameObject currGameObject = t.gameObject;
            Renderer mRenderer = currGameObject.GetComponent<Renderer>();

            RevertToOriginal(mRenderer);

            // recurse over children
            foreach(Transform child in t) {
                RevertToOriginalRecursive(child);
            }
            
        }
    }
}
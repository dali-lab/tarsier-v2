using System;
using System.Collections.Generic;
using System.Linq;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    /// <summary>
    /// Controller class to apply all of the relevant material effects that the object supports.
    /// This script recurses through the children of the game object it is placed on.
    /// Therefore, this should be placed at the top-most level of a game object that you want to start having material changes to.
    /// For example, if you have a gameObject and you want the material changes to affect the child (and its children) but not the
    /// top-most level, you would place this script in the gameObject's child.
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
        
        /// <summary>
        /// Goes through all renderers in itself and in children and applies vision effects
        /// </summary>
        /// <param name="visionParameters"></param>
        private void MaterialChange(VisionParameters visionParameters)
        {
            List<Renderer> renderers = gameObject.GetComponentsInChildren<Renderer>(true).ToList();
            Renderer parentRenderer = gameObject.GetComponent<Renderer>();
            if (parentRenderer != null)
            {
                renderers.Add(parentRenderer);
            }

            foreach (Renderer mRenderer in renderers)
            {
                RevertToOriginal(mRenderer);
                
                for (int i = 0; i < mRenderer.sharedMaterials.Length; i++)
                {
                    MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                    mRenderer.GetPropertyBlock(propBlock, i);
                    //go through list of effects for this animal and apply effect if this effect is on the object
                    foreach (VisionEffect effect in visionParameters.VisionEffects)
                    {
                        if (effects.ContainsKey(effect))
                        {
                            effects[effect].ApplyEffect(propBlock, i, mRenderer, visionParameters);
                        }
                    }
                    mRenderer.SetPropertyBlock(propBlock, i);
                }
            }
            lastAppliedEffects = visionParameters.VisionEffects;
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
    }
}
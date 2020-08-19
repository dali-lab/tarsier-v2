using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Anivision.Core;
using Anivision.Vision;

namespace Anivision.Vision
{
    /// <summary>
    /// Controls the nectar sparkliness based on the amount of nectar
    /// </summary>
    public class NectarShaderIntensity : MaterialEffect
    {
        public override VisionEffect Effect => VisionEffect.NectarIntensity;
        private int globcount = 0;

        private MaterialPropertyBlock _propBlock;

        void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            UpdateMaterial();
        }

        public void IncrementGlob()
        {
            globcount++;
            UpdateMaterial();
        }

        public void GlobConsumed()
        {
            if (globcount > 0)
            {
                globcount--;
                UpdateMaterial();
            }
            
        }

        public override void ApplyEffect(MaterialPropertyBlock propBlock, int materialIndex, Renderer renderer,
            VisionParameters visionParameters)
        {
            propBlock.SetInt("_Intensity", globcount * 2 + 1);
        }

        public override void RevertToOriginal(Renderer r)
        {
            _propBlock.Clear();
            r.SetPropertyBlock(_propBlock);
        }

        private void UpdateMaterial()
        {
            _propBlock.SetInt("_Intensity", globcount * 2 + 1);
            UpdateMaterialRecursive(transform, _propBlock);
        }
        
        /// <summary>
        /// Recurses through all of the child transforms and applies updated intensity
        /// </summary>
        /// <param name="t"></param>
        /// <param name="propBlock"></param>
        private void UpdateMaterialRecursive(Transform t, MaterialPropertyBlock propBlock)
        {
            GameObject currGameObject = t.gameObject;
            Renderer mRenderer = currGameObject.GetComponent<Renderer>();
            
            // if current game object has a renderer
            if (mRenderer != null)
            {
                //go through renderer materials
                for (int i = 0; i < mRenderer.sharedMaterials.Length; i++)
                {
                    mRenderer.SetPropertyBlock(propBlock, i);
                }

            }
            // recurse over children
            foreach(Transform child in t) {
                UpdateMaterialRecursive(child, propBlock);
            }
        }

    }
}
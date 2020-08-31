using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Anivision.Core;

namespace Anivision.Vision
{
    /// <summary>
    /// Controls the nectar sparkliness based on the amount of nectar
    /// </summary>
    public class NectarShaderIntensity : MaterialEffect
    {
        public override VisionEffect Effect => VisionEffect.NectarIntensity;
        private int globcount = 0;
        private float maxGlobs = 1;

        private MaterialPropertyBlock _propBlock;

        void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            UpdateMaterial();
        }

        public void IncrementGlob()
        {
            globcount++;
            if (globcount > maxGlobs)
            {
                maxGlobs = globcount;
            }
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
        
        private float GetIntensity()
        {
            return globcount / maxGlobs;
        }

        public override void ApplyEffect(MaterialPropertyBlock propBlock, int materialIndex, Renderer renderer,
            VisionParameters visionParameters)
        {
            propBlock.SetFloat("_SparkleOn", 1.0f);
            propBlock.SetFloat("_SparkleIntensity", GetIntensity());
        }

        public override void RevertToOriginal(Renderer r)
        {
            _propBlock.Clear();
            r.SetPropertyBlock(_propBlock);
        }

        private void UpdateMaterial()
        {
            // _propBlock.SetFloat("_SparkleIntensity", GetIntensity());
            UpdateMaterialHelper(transform, _propBlock);
        }
        
        /// <summary>
        /// Recurses through all of the child transforms and applies updated intensity
        /// </summary>
        /// <param name="t"></param>
        /// <param name="propBlock"></param>
        private void UpdateMaterialHelper(Transform t, MaterialPropertyBlock propBlock)
        {
            
            List<Renderer> renderers = gameObject.GetComponentsInChildren<Renderer>(true).ToList();
            Renderer parentRenderer = gameObject.GetComponent<Renderer>();
            if (parentRenderer != null)
            {
                renderers.Add(parentRenderer);
            }

            foreach (Renderer r in renderers)
            {
                for (int i = 0; i < r.sharedMaterials.Length; i++)
                {
                    r.GetPropertyBlock(propBlock, i);
                    propBlock.SetFloat("_SparkleIntensity", GetIntensity());
                    propBlock.SetFloat("_SparkleOn", r.sharedMaterials[i].GetFloat("_SparkeOn"));
                    r.SetPropertyBlock(propBlock, i);
                }
            }
        }

    }
}
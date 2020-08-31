using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Anivision.Core;

namespace Anivision.Vision
{
    /// <summary>
    /// Controls the nectar sparkliness based on the amount of nectar
    /// </summary>
    public class SparkleShaderIntensity : MaterialEffect
    {
        public override VisionEffect Effect => VisionEffect.Sparkle;
        public int SparkleIntensity = 0;
        
        private float _maxSparkle = 1;

        private MaterialPropertyBlock _propBlock;

        void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            IncreaseSparkle(SparkleIntensity);
            UpdateMaterial();
        }

        public void IncreaseSparkle(int incrementBy = 1)
        {
            SparkleIntensity += incrementBy;
            if (SparkleIntensity > _maxSparkle)
            {
                _maxSparkle = SparkleIntensity;
            }
            UpdateMaterial();
        }

        public void DecreaseSparkle(int decrementBy = 1)
        {
            if (SparkleIntensity > 0)
            {
                if (SparkleIntensity - decrementBy > 0)
                {
                    SparkleIntensity-= decrementBy;
                }
                else
                {
                    SparkleIntensity--;
                }
                
                UpdateMaterial();
            }
        }
        
        private float GetIntensity()
        {
            return SparkleIntensity / _maxSparkle;
        }

        public override void ApplyEffect(MaterialPropertyBlock propBlock, int materialIndex, Renderer renderer,
            VisionParameters visionParameters)
        {
            propBlock.SetFloat("_SparkleOn", 1.0f);
            if (renderer.sharedMaterials[materialIndex].HasProperty("_SparkleOn")) propBlock.SetFloat("_SparkleIntensity", GetIntensity());
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
                    if (r.sharedMaterials[i].HasProperty("_SparkleOn")) propBlock.SetFloat("_SparkleOn", r.sharedMaterials[i].GetFloat("_SparkleOn"));
                    r.SetPropertyBlock(propBlock, i);
                }
            }
        }
    }
}
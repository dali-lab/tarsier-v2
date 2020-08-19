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
            propBlock.SetFloat("_Intensity", GetIntensity());
        }

        public override void RevertToOriginal(Renderer r)
        {
            _propBlock.Clear();
            r.SetPropertyBlock(_propBlock);
        }

        private void UpdateMaterial()
        {
            _propBlock.SetFloat("_Intensity", GetIntensity());
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
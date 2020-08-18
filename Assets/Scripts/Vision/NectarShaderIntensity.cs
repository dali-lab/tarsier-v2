using UnityEngine;
using System.Collections;
using System;
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

        private Renderer[] _renderers;
        private MaterialPropertyBlock _propBlock;

        void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            _renderers = GetComponentsInChildren<Renderer>();
            updateMaterial();
        }

        public void IncrementGlob()
        {
            globcount++;
            updateMaterial();
        }

        public void GlobConsumed()
        {
            globcount--;
            updateMaterial();
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

        private void updateMaterial()
        {
            _propBlock.SetInt("_Intensity", globcount * 2 + 1);
            foreach(Renderer r in _renderers) {
                r.SetPropertyBlock(_propBlock);
            }
        }

}
}
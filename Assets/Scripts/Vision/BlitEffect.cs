using Anivision.Core;
using UnityEngine;
using UnityEngine.Experiemntal.Rendering.Universal;
using UnityEngine.Rendering.Universal;

namespace Anivision.Vision
{
    // A general - yet useable on it's own - renderer effect for blit features
    public class BlitEffect : RendererEffect
    {
        [Tooltip("The vision effect to use this renderer effect for.")] 
        public new VisionEffect Effect;

        [Tooltip("This name will appear as the effect's name in the renderer pipeline.")]
        public string effectName = "Blit Effect";
        [Tooltip("The settings for the blit itself.")]
        public Blit.BlitSettings settings;

        public override ScriptableRendererFeature GetRendererFeature(VisionParameters parameters)
        {
            // Create a new blit feature
            Blit blit = new Blit();

            // Give it the proper name and settings
            blit.name = effectName;
            blit.settings = settings;

            return blit;
        }
    }
}


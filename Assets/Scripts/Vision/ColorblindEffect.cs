using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;
using UnityEngine.Experiemntal.Rendering.Universal;
using UnityEngine.Rendering.Universal;

namespace Anivision.Vision
{
    public class ColorblindEffect : RendererEffect
    {
        [HideInInspector]
        public new VisionEffect Effect = VisionEffect.Colorblindness;

        [Tooltip("A material using the Anivision/Colorblind shader.")]
        public Material colorblindMaterial;

        // Associates colorblindness types with the proper shader keyword
        private static readonly Dictionary<ColorblindType, string> ShaderKeywords = new Dictionary<ColorblindType, string>
        {
            {ColorblindType.Protanopia, "_TYPE_PROTANOPIA"},
            {ColorblindType.Protanomaly, "_TYPE_PROTANOMALY"},
            {ColorblindType.Deuteranopia, "_TYPE_DEUTERANOPIA"},
            {ColorblindType.Deuteranomaly, "_TYPE_DEUTERANOMALY"},
            {ColorblindType.Tritanopia, "_TYPE_TRITANOPIA"},
            {ColorblindType.Tritanomaly, "_TYPE_TRITANOMALY"},
            {ColorblindType.Achromatopsia, "_TYPE_ACHROMATOPSIA"},
            {ColorblindType.Achromatomaly, "_TYPE_ACHROMATOMALY"},
            {ColorblindType.Custom, "_TYPE_CUSTOM"}
        };

        public override ScriptableRendererFeature GetRendererFeature(VisionParameters parameters)
        {
            // Create a new blit feature
            Blit blit = new Blit();

            // Give it the proper name and settings
            blit.name = "Colorblindness";
            blit.settings.Event = RenderPassEvent.AfterRenderingTransparents;
            blit.settings.blitMaterialPassIndex = 0;
            blit.settings.destination = Blit.Target.Color;

            // Disable other colorblind modes
            foreach (KeyValuePair<ColorblindType, string> entry in ShaderKeywords)
            {
                colorblindMaterial.DisableKeyword(entry.Value);
            }
            // Enable the correct colorblind mode
            colorblindMaterial.EnableKeyword(ShaderKeywords[parameters.Colorblindness]);
            // If colorblind type is custom, pass the shader the custom matrix
            if (parameters.Colorblindness.Equals(ColorblindType.Custom))
            {
                colorblindMaterial.SetMatrix("_CustomMatrix", parameters.ColorblindMatrix);
            }

            // Pass the material to the blit effect
            blit.settings.blitMaterial = colorblindMaterial;

            return blit;
        }
    }
}


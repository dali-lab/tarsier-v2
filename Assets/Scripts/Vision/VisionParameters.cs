using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

namespace Anivision.Vision
{
    /// <summary>
    /// Contains all of the vision information for an animal
    /// </summary>
    public class VisionParameters
    {
        public Animal Animal { get; private set; }
        public List<VisionEffect> visionEffects { get; private set; }
        public ColorblindType Colorblindness { get; private set; }
        public Matrix4x4 ColorblindMatrix { get; private set; }

        public readonly Dictionary<ColorblindType, Matrix4x4> ColorblindMatrixReference = new Dictionary<ColorblindType, Matrix4x4>
        {
            {ColorblindType.Protanopia, new Matrix4x4(
                new Vector4(0.567f,0.433f, 0f, 0f), 
                new Vector4(0.558f,0.442f, 0f, 0f), 
                new Vector4(0f,0.242f,0.758f, 0f), 
                new Vector4(0f, 0f, 0f, 1f))
            },
            {ColorblindType.Protanomaly, new Matrix4x4(
                new Vector4(0.817f,0.183f, 0f, 0f), 
                new Vector4(0.333f,0.667f, 0f, 0f), 
                new Vector4(0f,0.125f,0.875f, 0f), 
                new Vector4(0f, 0f, 0f, 1f))
            },
            {ColorblindType.Deuteranopia, new Matrix4x4(
                new Vector4(0.625f,0.375f, 0f, 0f), 
                new Vector4(0.7f,0.3f, 0f, 0f), 
                new Vector4(0f,0.3f,0.7f, 0f), 
                new Vector4(0f, 0f, 0f, 1f))
            },
            {ColorblindType.Deuteranomaly, new Matrix4x4(
                new Vector4(0.8f,0.2f,0,0), 
                new Vector4(0.258f,0.742f,0,0),
                new Vector4(0f,0.142f,0.858f),
                new Vector4(0f, 0f, 0f, 1f))
            },
            {ColorblindType.Tritanopia, new Matrix4x4(
                new Vector4(0.95f,0.05f, 0f, 0f), 
                new Vector4(0f,0.433f,0.567f, 0f), 
                new Vector4(0f,0.475f,0.525f, 0f), 
                new Vector4(0f, 0f, 0f, 1f))
            },
            {ColorblindType.Tritanomaly, new Matrix4x4(
                new Vector4(0.967f,0.033f, 0f, 0f), 
                new Vector4(0f,0.733f,0.267f, 0f), 
                new Vector4(0f,0.183f,0.817f, 0f), 
                new Vector4(0f, 0f, 0f, 1f))
            },
            {ColorblindType.Achromatopsia, new Matrix4x4(
                new Vector4(0.299f,0.587f,0.114f, 0f), 
                new Vector4(0.299f,0.587f,0.114f, 0f), 
                new Vector4(0.299f,0.587f,0.114f, 0f), 
                new Vector4(0f, 0f, 0f, 1f))
            },
            {ColorblindType.Achromatomaly, new Matrix4x4(
                new Vector4(0.618f,0.320f,0.062f, 0f), 
                new Vector4(0.163f,0.775f,0.062f, 0f), 
                new Vector4(0.163f,0.320f,0.516f, 0f), 
                new Vector4(0f, 0f, 0f, 1f))
            },
            {ColorblindType.None, Matrix4x4.identity}
        };

        public VisionParameters(Animal animal, List<VisionEffect> effects, ColorblindType colorblindness, Matrix4x4? customColorblindMatrix)
        {
            Animal = animal;
            Colorblindness = colorblindness;
            visionEffects = effects;

            if (colorblindness == ColorblindType.Custom)
            {
                if (customColorblindMatrix.HasValue)
                {
                    ColorblindMatrix = customColorblindMatrix.Value;
                }
                else
                {
                    UnityEngine.Debug.LogError("If Colorblindness is a custom type of colorblindness, must pass in a 4x4 matrix that contains channel ouput weights");
                    ColorblindMatrix = Matrix4x4.identity;
                }
                
            }
            else
            {
                ColorblindMatrix = ColorblindMatrixReference[colorblindness];
            }
        }
    }
    
    

}
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
        public List<VisionEffect> VisionEffects { get; private set; }
        public ColorblindType Colorblindness { get; private set; }
        public Matrix4x4 ColorblindMatrix { get; private set; }

        public VisionParameters(Animal animal, List<VisionEffect> effects, ColorblindType colorblindness, Matrix4x4? customColorblindMatrix)
        {
            Animal = animal;
            Colorblindness = colorblindness;
            VisionEffects = effects;

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
        }
    }
}
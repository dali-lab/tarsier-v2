using System;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Environment
{
    /// <summary>
    /// Small class holding modifiable light parameters
    /// </summary>
    [Serializable]
    public class LightParameters
    {
        public Animal animal;
        public float intensity = 1.0f;
        public Color color = Color.white;
    }

}
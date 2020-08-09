using System;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    //small class to have pairing between animal and material
    [Serializable]
    public class MaterialAnimal
    {
        public Animal animal;
        public Material material;
    }
}
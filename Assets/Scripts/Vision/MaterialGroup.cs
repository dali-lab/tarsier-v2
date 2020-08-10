using System;
using UnityEngine;

namespace Anivision.Vision
{
    //small class to group together all the materials that should be swapped between
    [Serializable]
    public class MaterialGroup
    {
        public Material originalMaterial;
        public MaterialAnimal[] MaterialsToSwap;
    }
}
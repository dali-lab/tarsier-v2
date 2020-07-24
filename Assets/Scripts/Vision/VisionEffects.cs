using System.Collections.Generic;
using UnityEngine;

namespace Anivision.Vision
{
    /// <summary>
    /// Contains helpful functions for applying various vision effects and calculations
    /// </summary>
    public class VisionEffects
    {
        public static Dictionary<Material, Color> SaveOriginalColors(Transform t, string baseColorName)
        {
            Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>();
            SaveOriginalColorsRecursive(t, originalColors, baseColorName);
            return originalColors;
        }
        
        //save original colors in hash table so that we can retrieve it easily later.
        private static void SaveOriginalColorsRecursive(Transform t, Dictionary<Material, Color> _originalColors, string materialBaseColorName)
        {
            GameObject currGameObject = t.gameObject;
            Renderer mRenderer = currGameObject.GetComponent<Renderer>();
            if (mRenderer != null)
            {
                foreach (Material m in mRenderer.materials)
                {
                    if (!_originalColors.ContainsKey(m))
                    {
                        _originalColors.Add(m, m.GetColor(materialBaseColorName));
                    }
                }
            }
            
            foreach( Transform child in t) {
                SaveOriginalColorsRecursive(child, _originalColors, materialBaseColorName);
            }
        }
    }
}
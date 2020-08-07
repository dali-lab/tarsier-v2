using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    /// <summary>
    /// Base class for all effects that affect the materials of world objects
    /// Contains helpful functions for applying various vision effects and calculations
    /// All material effects should inherit from this class
    /// </summary>
    public abstract class MaterialEffect: MonoBehaviour
    {
        public abstract VisionEffect Effect { get; }
        public abstract void RevertToOriginal(Renderer r);
        /// <summary>
        /// function for each class that inherits MaterialEffect to implement. Applies the specific effect to the material
        /// </summary>
        /// <param name="propBlock">passed in material property block</param>
        /// <param name="materialIndex">index of current material</param>
        /// <param name="renderer">current renderer</param>
        /// <param name="visionParameters">VisionParameters object holding the specific effects and vision information</param>
        public abstract void ApplyEffect(MaterialPropertyBlock propBlock, int materialIndex, Renderer renderer, VisionParameters visionParameters);

        /// <summary>
        /// Function to save material information into a dictionary
        /// </summary>
        /// <param name="t"></param>
        /// <param name="shaderBaseColor"></param>
        /// <param name="shaderBaseTexture"></param>
        /// <returns></returns>
        public static Dictionary<string, MaterialInfo> SaveMaterialInfo(Transform t, string shaderBaseColor, string shaderBaseTexture)
        {
            Dictionary<string, MaterialInfo> originalMaterialInfo = new Dictionary<string, MaterialInfo>();
            SaveMaterialInfoRecursive(t, originalMaterialInfo, shaderBaseColor, shaderBaseTexture);
            return originalMaterialInfo;
        }
        
        //save original material's info in hash table so that we can retrieve it easily later. 
        private static void SaveMaterialInfoRecursive(Transform t, Dictionary<string, MaterialInfo> _originalMaterials, string shaderColorProperty, string shaderTextureProperty)
        {
            GameObject currGameObject = t.gameObject;
            Renderer mRenderer = currGameObject.GetComponent<Renderer>();
            if (mRenderer != null)
            {
                foreach (Material m in mRenderer.sharedMaterials)
                {
                    if (!_originalMaterials.ContainsKey(GetMaterialName(m)))
                    {
                        _originalMaterials.Add(GetMaterialName(m), CreateMaterialInfo(m, shaderColorProperty, shaderTextureProperty, mRenderer));
                    }
                }
            }
            
            foreach( Transform child in t) {
                SaveMaterialInfoRecursive(child, _originalMaterials, shaderColorProperty, shaderTextureProperty);
            }
        }

        public static MaterialInfo CreateMaterialInfo(Material m, string shaderColorProperty, string shaderTextureProperty, Renderer r)
        {
            Color originalColor = m.GetColor(shaderColorProperty);
            MaterialInfo matInfo = new MaterialInfo();
            matInfo.color = new Color(originalColor.r, originalColor.g, originalColor.b);
            matInfo.texture = (Texture2D) m.GetTexture(shaderTextureProperty);
            matInfo.shaderColorProperty = shaderColorProperty;
            matInfo.shaderTextureProperty = shaderTextureProperty;
            matInfo.renderer = r;

            return matInfo;
        }

        public static string GetMaterialName(Material m)
        {
            return m.name.Replace(" (Instance)", "");
        }
    }
}
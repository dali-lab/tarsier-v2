using System.Collections.Generic;
using System.Linq;
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
        /// <param name="parentGameObject"></param>
        /// <param name="shaderBaseColor"></param>
        /// <param name="shaderBaseTexture"></param>
        /// <returns></returns>
        public static Dictionary<string, MaterialInfo> SaveMaterialInfo(GameObject parentGameObject, string shaderBaseColor, string shaderBaseTexture)
        {
            Dictionary<string, MaterialInfo> originalMaterialInfo = new Dictionary<string, MaterialInfo>();
            List<Renderer> renderers = parentGameObject.GetComponentsInChildren<Renderer>(true).ToList();
            Renderer parentRenderer = parentGameObject.GetComponent<Renderer>();
            if (parentRenderer != null)
            {
                renderers.Add(parentRenderer);
            }

            foreach (Renderer r in renderers)
            {
                foreach (Material m in r.sharedMaterials)
                {
                    if (!originalMaterialInfo.ContainsKey(GetMaterialName(m)))
                    {
                        originalMaterialInfo.Add(GetMaterialName(m), CreateMaterialInfo(m, shaderBaseColor, shaderBaseTexture, r));
                    }
                }
            }
            
            return originalMaterialInfo;
        }

        public static MaterialInfo CreateMaterialInfo(Material m, string shaderColorProperty, string shaderTextureProperty, Renderer r)
        {
            

            MaterialInfo matInfo = new MaterialInfo();
            if (m.HasProperty(shaderColorProperty))
            {
                Color originalColor = m.GetColor(shaderColorProperty);
                matInfo.color = new Color(originalColor.r, originalColor.g, originalColor.b);
            }
            if (m.HasProperty(shaderTextureProperty)) matInfo.texture = (Texture2D) m.GetTexture(shaderTextureProperty);
            if (m.HasProperty(shaderColorProperty)) matInfo.shaderColorProperty = shaderColorProperty;
            if (m.HasProperty(shaderTextureProperty)) matInfo.shaderTextureProperty = shaderTextureProperty;
            matInfo.renderer = r;

            return matInfo;
        }

        public static string GetMaterialName(Material m)
        {
            return m.name.Replace(" (Instance)", "");
        }
    }
}
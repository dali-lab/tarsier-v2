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
        public abstract void ApplyEffect(MaterialPropertyBlock propBlock, Material currentMaterial, Renderer renderer,
            List<Material> rendererMaterials, VisionParameters visionParameters);
        
        // class to save all of the relevant material information
        public class MaterialInfo
        {
            public Texture2D texture;
            public Texture2D changedTexture;
            public Color color;
            public string shaderTextureProperty;
            public string shaderColorProperty;
            public Renderer renderer;
        }
        
        
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
                        MaterialInfo matInfo = new MaterialInfo();
                        matInfo.color = m.GetColor(shaderColorProperty);
                        matInfo.texture = (Texture2D) m.GetTexture(shaderTextureProperty);
                        matInfo.shaderColorProperty = shaderColorProperty;
                        matInfo.shaderTextureProperty = shaderTextureProperty;
                        matInfo.renderer = mRenderer;
                        _originalMaterials.Add(GetMaterialName(m), matInfo);
                    }
                }
            }
            
            foreach( Transform child in t) {
                SaveMaterialInfoRecursive(child, _originalMaterials, shaderColorProperty, shaderTextureProperty);
            }
        }

        public static string GetMaterialName(Material m)
        {
            return m.name.Replace(" (Instance)", "");
        }
    }
}
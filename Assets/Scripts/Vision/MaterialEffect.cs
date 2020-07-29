using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    /// <summary>
    /// Contains helpful functions for applying various vision effects and calculations
    /// </summary>
    public abstract class MaterialEffect: MonoBehaviour
    {
        public abstract VisionEffect Effect { get; }
        public abstract void RevertToOriginal(Renderer r);
        public abstract void ApplyEffect(MaterialPropertyBlock propBlock, Material currentMaterial,
            List<Material> rendererMaterials, VisionParameters visionParameters);
        
        public static Dictionary<Material, MaterialInfo> SaveOriginalColors(Transform t, string shaderBaseColor, string shaderBaseTexture)
        {
            Dictionary<Material, MaterialInfo> originalMaterialInfo = new Dictionary<Material, MaterialInfo>();
            SaveOriginalColorsRecursive(t, originalMaterialInfo, shaderBaseColor, shaderBaseTexture);
            return originalMaterialInfo;
        }
        
        //save original material's info in hash table so that we can retrieve it easily later. 
        private static void SaveOriginalColorsRecursive(Transform t, Dictionary<Material, MaterialInfo> _originalMaterials, string shaderColorProperty, string shaderTextureProperty)
        {
            GameObject currGameObject = t.gameObject;
            Renderer mRenderer = currGameObject.GetComponent<Renderer>();
            if (mRenderer != null)
            {
                foreach (Material m in mRenderer.materials)
                {
                    if (!_originalMaterials.ContainsKey(m))
                    {
                        MaterialInfo matInfo = new MaterialInfo();
                        matInfo.color = m.GetColor(shaderColorProperty);
                        matInfo.texture = (Texture2D) m.GetTexture(shaderTextureProperty);
                        matInfo.shaderColorProperty = shaderColorProperty;
                        matInfo.shaderTextureProperty = shaderTextureProperty;
                        UnityEngine.Debug.Log(m.name);
                        _originalMaterials.Add(m, matInfo);
                    }
                }
            }
            
            foreach( Transform child in t) {
                SaveOriginalColorsRecursive(child, _originalMaterials, shaderColorProperty, shaderTextureProperty);
            }
        }
    }

    public class MaterialInfo
    {
        public Texture2D texture;
        public Color color;
        public string shaderTextureProperty;
        public string shaderColorProperty;
    }
}
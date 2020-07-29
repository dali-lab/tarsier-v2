using System;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    /// <summary>
    /// Script to apply UV false color effect to materials
    /// </summary>
    public class UV : MaterialEffect
    {
        [Range(0, 1)] public float UVAmount;
        [Tooltip("The property name in the shader that refers to the main color property that should be changed")]
        public string shaderBaseColor = "_BaseColor";
        public string shaderBaseTexture = "_BaseMap";
        
        public override VisionEffect Effect => VisionEffect.UV;
        
        private AnimalManager _animalManager;
        private Dictionary<string, MaterialInfo> _materialInfo;

        private void Awake()
        {
            //save original colors in hash table so that we can access them easily later.
            _materialInfo = SaveMaterialInfo(gameObject.transform, shaderBaseColor, shaderBaseTexture);
            ConstructUVTextures(_materialInfo);
        }
        
        //constructs the UV textures (if base material has a texture) and caches them for performance
        private void ConstructUVTextures(Dictionary<string, MaterialInfo> materialInfo)
        {
            foreach (KeyValuePair<string, MaterialInfo> m in materialInfo)
            {
                Texture2D originalTexture = m.Value.texture;
                if (originalTexture != null)
                {
                    Color[] colors = originalTexture.GetPixels();
                    Color[] newColors = new Color[colors.Length];
                        
                    for (int i = 0; i < newColors.Length; i++)
                    {
                        newColors[i] = new Color(colors[i].g, colors[i].b, UVAmount);

                    }

                    Texture2D newT = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGB24, true);
                    newT.wrapMode = TextureWrapMode.Clamp;
                    newT.SetPixels(newColors);
                    newT.Apply();
                    m.Value.changedTexture = newT;
                }
            }
        }
        
        /// <summary>
        /// Goes through dictionary of materials to find material's UV texture and applies it to the shader property
        /// If texture is non-existent, changes the shader color
        /// </summary>
        /// <param name="propBlock"></param>
        /// <param name="currentMaterial"></param>
        /// <param name="renderer"></param>
        /// <param name="rendererMaterials"></param>
        /// <param name="visionParameters"></param>
        public override void ApplyEffect(MaterialPropertyBlock propBlock, Material currentMaterial, Renderer renderer, List<Material> rendererMaterials,
            VisionParameters visionParameters)
        {
            
            //if we have recursed to a child game object with its own UV script, we should use the child's UV settings instead
            if (renderer.gameObject != gameObject)
            {
                UV childUVScript = renderer.gameObject.GetComponent<UV>();
                if (childUVScript != null)
                {
                    childUVScript.ApplyEffect(propBlock, currentMaterial, renderer, rendererMaterials, visionParameters);
                    return;
                }
            }
            
            MaterialInfo matInfo;
            _materialInfo.TryGetValue(GetMaterialName(currentMaterial), out matInfo);
            Color color = currentMaterial.GetColor(shaderBaseColor);

            if (matInfo != null)
            {
                if (matInfo.changedTexture != null)
                {
                    propBlock.SetTexture(shaderBaseTexture, matInfo.changedTexture);
                    
                }
            }
            
            Color newColor = new Color(color.g, color.b,UVAmount);
            propBlock.SetColor(shaderBaseColor, newColor);

        }
        
        /// <summary>
        /// Changes materials of a renderer back to the original
        /// </summary>
        /// <param name="r"></param>
        public override void RevertToOriginal(Renderer r)
        {
            for (int i = 0; i < r.sharedMaterials.Length; i++)
            {
                MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                r.GetPropertyBlock(propBlock, i);
                
                MaterialInfo matInfo;
                _materialInfo.TryGetValue(GetMaterialName(r.sharedMaterials[i]), out matInfo);

                if (matInfo != null)
                {
                    if (matInfo.texture != null)
                    {
                        propBlock.SetTexture(matInfo.shaderTextureProperty, matInfo.texture);
                    }

                    propBlock.SetColor(matInfo.shaderColorProperty, matInfo.color);
                    r.SetPropertyBlock(propBlock, i);
                }
            }

        }

    }
}
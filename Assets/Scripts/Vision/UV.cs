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
            _materialInfo = SaveMaterialInfo(gameObject, shaderBaseColor, shaderBaseTexture);
            ConstructUVTextures(_materialInfo);
        }
        
        //adds required MaterialController component if not found in parent
        private void OnValidate()
        {
            Transform currentTransform = transform;
            if (currentTransform.GetComponent<MaterialController>() != null) return;

            while (currentTransform.parent != null)
            {
                if (currentTransform.parent.GetComponent<MaterialController>() != null) return;

                currentTransform = currentTransform.parent;
            }

            gameObject.AddComponent<MaterialController>();

        }

        //constructs the UV textures (if base material has a texture) and caches them for performance
        private void ConstructUVTextures(Dictionary<string, MaterialInfo> materialInfo)
        {
            foreach (KeyValuePair<string, MaterialInfo> m in materialInfo)
            {
                Texture2D originalTexture = m.Value.texture;
                if (originalTexture != null)
                {
                    m.Value.changedTexture = CreateUVTexture(originalTexture, UVAmount);
                }
            }
        }

        private Texture2D CreateUVTexture(Texture2D originalTexture, float uv)
        {
            if (originalTexture != null)
            {
                Color[] colors = originalTexture.GetPixels();
                Color[] newColors = new Color[colors.Length];

                for (int i = 0; i < newColors.Length; i++)
                {
                    newColors[i] = new Color(colors[i].g, colors[i].b, uv);

                }

                Texture2D newT = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGB24,
                    true);
                newT.wrapMode = TextureWrapMode.Clamp;
                newT.SetPixels(newColors);
                newT.Apply();
                return newT;
            }

            return null;
        }
        
        /// <summary>
        /// Goes through dictionary of materials to find material's UV texture and applies it to the shader property
        /// If texture is non-existent, changes the shader color
        /// </summary>
        /// <param name="propBlock"></param>
        /// <param name="materialIndex"></param>
        /// <param name="currentRenderer"></param>
        /// <param name="visionParameters"></param>
        public override void ApplyEffect(MaterialPropertyBlock propBlock, int materialIndex, Renderer currentRenderer, 
            VisionParameters visionParameters)
        {
            
            //if child game object has its own UV script, we should use the child's UV settings instead
            if (currentRenderer.gameObject != gameObject)
            {
                UV childUVScript = currentRenderer.gameObject.GetComponent<UV>();
                if (childUVScript != null)
                {
                    childUVScript.ApplyEffect(propBlock, materialIndex, currentRenderer, visionParameters);
                    return;
                }
            }

            Material currentMaterial = currentRenderer.sharedMaterials[materialIndex];
            MaterialInfo matInfo;
            _materialInfo.TryGetValue(GetMaterialName(currentMaterial), out matInfo);
            if (currentMaterial.HasProperty(shaderBaseColor))
            {
                Color color = currentMaterial.GetColor(shaderBaseColor);
            
                if (matInfo == null)
                {

                    MaterialInfo newMatInfo =
                        CreateMaterialInfo(currentMaterial, shaderBaseColor, shaderBaseTexture, currentRenderer);
                    if (newMatInfo.texture != null)
                    {
                        newMatInfo.changedTexture = CreateUVTexture(newMatInfo.texture, UVAmount);
                    }
                    _materialInfo.Add(GetMaterialName(currentMaterial), newMatInfo);
                    matInfo = newMatInfo;
                }

                if (matInfo.changedTexture != null)
                {
                    propBlock.SetTexture(shaderBaseTexture, matInfo.changedTexture);
                }

                Color newColor = new Color(color.g, color.b,UVAmount);
                propBlock.SetColor(shaderBaseColor, newColor);
            }

        }
        
        /// <summary>
        /// Changes materials of a renderer back to the original
        /// </summary>
        /// <param name="r"></param>
        public override void RevertToOriginal(Renderer r)
        {
            MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            for (int i = 0; i < r.sharedMaterials.Length; i++)
            {
                r.GetPropertyBlock(propBlock, i);
                propBlock.Clear();
                r.SetPropertyBlock(propBlock, i);
            }

        }

    }
}
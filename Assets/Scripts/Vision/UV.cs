using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    [RequireComponent(typeof(MaterialController))]
    public class UV : MaterialEffect
    {
        [Range(0, 1)] public float UVAmount;
        [Tooltip("The property name in the shader that refers to the main color property that should be changed")]
        public string shaderBaseColor = "_BaseColor";
        public string shaderBaseTexture = "_BaseMap";
        public override VisionEffect Effect => VisionEffect.UV;
        
        private AnimalManager _animalManager;
        private Dictionary<Material, MaterialInfo> _originalMaterialInfo;
        
        private void Awake()
        {
            //save original colors in hash table so that we can access them easily later.
            _originalMaterialInfo = SaveOriginalColors(gameObject.transform, shaderBaseColor, shaderBaseTexture);
        }
        
        // Start is called before the first frame update
        // void Start()
        // {
        //     _animalManager = AnimalManager.Instance;
        //
        //     if (_animalManager == null)
        //     {
        //         throw new Exception("There must be an instance of the AnimalManager script in the scene");
        //     }
        //     else
        //     {
        //         _animalManager.VisionSwitch.AddListener(SwitchUV);
        //     }
        // }

        public override void ApplyEffect(MaterialPropertyBlock propBlock, Material currentMaterial, List<Material> rendererMaterials,
            VisionParameters visionParameters)
        {
            // MaterialInfo matInfo;
            // _originalMaterialInfo.TryGetValue(material, out matInfo);

            Texture2D texture = (Texture2D) propBlock.GetTexture(shaderBaseTexture);
            Color color = propBlock.GetColor(shaderBaseColor);
            
            if (texture != null)
            {
                Color[] colors = texture.GetPixels();
                Color[] newColors = new Color[colors.Length];
                        
                for (int i = 0; i < newColors.Length; i++)
                {
                    newColors[i] = new Color(colors[i].g, colors[i].b, UVAmount);

                }

                Texture2D newT = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, true);
                newT.wrapMode = TextureWrapMode.Clamp;
                newT.SetPixels(newColors);
                newT.Apply();
                // Color newColor = new Color(originalColor.g, originalColor.b,UVAmount);
                // propBlock.SetColor(materialBaseColorName, newColor);
                propBlock.SetTexture(shaderBaseTexture, newT);
                propBlock.SetColor(shaderBaseColor, Color.white);
            }
            else
            {
                Color newColor = new Color(color.g, color.b,UVAmount);
                propBlock.SetColor(shaderBaseColor, newColor);
            }
            
            // if (matInfo != null)
            // {
            //     if (matInfo.texture != null)
            //     {
            //         Color[] colors = matInfo.texture.GetPixels();
            //         Color[] newColors = new Color[colors.Length];
            //             
            //         for (int i = 0; i < newColors.Length; i++)
            //         {
            //             newColors[i] = new Color(colors[i].g, colors[i].b, UVAmount);
            //
            //         }
            //
            //         Texture2D newT = new Texture2D(matInfo.texture.width, matInfo.texture.height, TextureFormat.RGB24, true);
            //         newT.wrapMode = TextureWrapMode.Clamp;
            //         newT.SetPixels(newColors);
            //         newT.Apply();
            //         // Color newColor = new Color(originalColor.g, originalColor.b,UVAmount);
            //         // propBlock.SetColor(materialBaseColorName, newColor);
            //         propBlock.SetTexture(matInfo.shaderTextureProperty, newT);
            //         propBlock.SetColor(matInfo.shaderColorProperty, Color.white);
            //     }
            //     else
            //     {
            //         Color newColor = new Color(matInfo.color.g, matInfo.color.b,UVAmount);
            //         propBlock.SetColor(matInfo.shaderColorProperty, newColor);
            //     }
            // }
            
        }
        public override void RevertToOriginal(Renderer r)
        {
            for (int i = 0; i < r.materials.Length; i++)
            {
                MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                r.GetPropertyBlock(propBlock, i);
                
                MaterialInfo matInfo;
                _originalMaterialInfo.TryGetValue(r.materials[i], out matInfo);

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
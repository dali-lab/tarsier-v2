using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    [Serializable]
    public class MaterialUV
    {
        private Material material;
        private float UVAmount;
    }
    public class UV : MonoBehaviour
    {
        [Range(0, 1)] public float UVAmount;
        [Tooltip("The property name in the shader that refers to the main color property that should be changed")]
        public string materialBaseColorName = "_BaseColor";
        public string materialBaseTextureName = "_BaseMap";
        
        private AnimalManager _animalManager;
        private Dictionary<Material, Color> _originalColors;

        public Texture2D UVTexture;
        
        private void Awake()
        {
            //save original colors in hash table so that we can access them easily later.
            _originalColors = VisionEffects.SaveOriginalColors(gameObject.transform, materialBaseColorName);
        }
        
        // Start is called before the first frame update
        void Start()
        {
            _animalManager = AnimalManager.Instance;

            if (_animalManager == null)
            {
                throw new Exception("There must be an instance of the AnimalManager script in the scene");
            }
            else
            {
                _animalManager.VisionSwitch.AddListener(SwitchUV);
            }
        }

        private void SwitchUV(Renderer r, bool switchUV, Color originalColor)
        {
            if (r != null)
            {
                MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                r.GetPropertyBlock(propBlock);

                if (switchUV)
                {
                    UnityEngine.Debug.Log("Called");
                    Texture2D t = (Texture2D) r.material.GetTexture(materialBaseTextureName);

                    if (t != null)
                    {
                        UnityEngine.Debug.Log("Not null");
                        Color[] colors = t.GetPixels();
                        Color[] UVAmounts = UVTexture.GetPixels();
                        Color[] newColors = new Color[UVAmounts.Length];
                        Mesh mesh = GetComponent<MeshFilter>().mesh;
                        
                        for (int i = 0; i < newColors.Length; i++)
                        {
                            if (i < colors.Length)
                            {
                                float grayscale = (float)(0.2989 * UVAmounts[i].r + 0.5870 * UVAmounts[i].g +
                                                          0.1140 * UVAmounts[i].b); 
                                newColors[i] = new Color(colors[i].g, colors[i].b, grayscale);
                            }

                        }

                        Texture2D newT = new Texture2D(UVTexture.width, UVTexture.height, TextureFormat.RGB24, true);
                        newT.wrapMode = TextureWrapMode.Clamp;
                        newT.SetPixels(newColors);
                        newT.Apply();
                        // Color newColor = new Color(originalColor.g, originalColor.b,UVAmount);
                        // propBlock.SetColor(materialBaseColorName, newColor);
                        propBlock.SetTexture(materialBaseTextureName, newT);
                        propBlock.SetColor(materialBaseColorName, Color.white);
                    }
                    else
                    {
                        UnityEngine.Debug.Log("null");
                        Texture2D newT = new Texture2D(UVTexture.width, UVTexture.height, TextureFormat.RGB24, true);
                        newT.wrapMode = TextureWrapMode.Clamp;
                        Color[] uvAmounts = UVTexture.GetPixels();
                        Color[] newColors = new Color[uvAmounts.Length];
                        for (int i = 0; i < uvAmounts.Length; i++)
                        {
                            float grayscale = (float)(0.2989 * uvAmounts[i].r + 0.5870 * uvAmounts[i].g +
                                              0.1140 * uvAmounts[i].b); 
                            newColors[i] = new Color(originalColor.g, originalColor.b,grayscale);
                        }
                        
                        newT.SetPixels(newColors);
                        newT.Apply();
                        // r.material.SetTexture(materialBaseTextureName, newT);
                        propBlock.SetTexture(materialBaseTextureName, newT);
                        UnityEngine.Debug.Log(originalColor);
                        Color newColor = new Color(originalColor.g, originalColor.b,0);
                        propBlock.SetColor(materialBaseColorName, Color.white);
                        
                    }
                    
                }
                else
                {
                    propBlock.SetColor(materialBaseColorName, originalColor);
                }
                
                r.SetPropertyBlock(propBlock);
            }
        }

        private void SwitchUV(VisionParameters visionParameters)
        {
            SwitchUVRecursive(gameObject.transform, visionParameters);
        }
        
        //go through game object and apply colorblindness to it and to all children
        private void SwitchUVRecursive(Transform t, VisionParameters visionParameters)
        {
            GameObject currGameObject = t.gameObject;
            Renderer mRenderer = currGameObject.GetComponent<Renderer>();
            
            // if current game object has a renderer
            if (mRenderer != null){
                Color originalColor;
                _originalColors.TryGetValue(mRenderer.material, out originalColor);
                SwitchUV(mRenderer, visionParameters.HasUvVision, originalColor);
            }
            // recurse over children
            foreach( Transform child in t) {
                SwitchUVRecursive(child, visionParameters);
            }
        }
    }
}


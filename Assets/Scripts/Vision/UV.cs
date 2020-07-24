using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.Vision
{
    public class UV : MonoBehaviour
    {
        [Range(0, 1)] public float UVAmount;
        [Tooltip("The property name in the shader that refers to the main color that should be changed")]
        public string materialBaseColorName = "_BaseColor";
        
        private AnimalManager _animalManager;
        private Dictionary<Material, Color> _originalColors;
        
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
                    Color newColor = new Color(originalColor.g, originalColor.b,UVAmount);
                    propBlock.SetColor(materialBaseColorName, newColor);
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


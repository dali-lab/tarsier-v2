using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using Anivision.Vision;
using UnityEngine;

namespace Anivision.Vision
{
    /// <summary>
    /// Script to switch material's main color to color as seen by an animal with a specific kind of colorblindness
    /// </summary>
    
    public class Colorblind : MonoBehaviour
    {
        [Tooltip("The property name in the shader that refers to the main color that should be changed")]
        public string materialBaseColorName = "_BaseColor";
    
        private AnimalManager _animalManager;
        private Dictionary<Material, Color> _originalColors;
        private bool _materialSwapSupported;
    
        // Start is called before the first frame update
        private void Awake()
        {
            //save original colors in hash table so that we can do matrix math easily later.
            _originalColors = VisionEffects.SaveOriginalColors(gameObject.transform, materialBaseColorName);
            _materialSwapSupported = GetComponent<MaterialSwapScript>() != null;
        }

        void Start()
        {
            _animalManager = AnimalManager.Instance;

            if (_animalManager == null)
            {
                throw new Exception("There must be an instance of the AnimalManager script in the scene");
            }
            else
            {
                _animalManager.VisionSwitch.AddListener(SwitchColorblind);
            }

        }

        private void SwitchColorblind(Renderer r, Matrix4x4 colorMatrix, Color originalColor)
        {
            if (r != null)
            {
                MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                r.GetPropertyBlock(propBlock);
                Vector4 newColorVector = colorMatrix.MultiplyVector(new Vector4(originalColor.r, originalColor.g, originalColor.b, originalColor.a));
                Color newColor = new Color(newColorVector.x, newColorVector.y, newColorVector.z, originalColor.a);
                propBlock.SetColor(materialBaseColorName, newColor);
                r.SetPropertyBlock(propBlock);
            }
        }

        private void SwitchColorblind(VisionParameters visionParameters)
        {
            SwitchColorblindRecursive(gameObject.transform, visionParameters);
        }
        
        //go through game object and apply colorblindness to it and to all children
        private void SwitchColorblindRecursive(Transform t, VisionParameters visionParameters)
        {
            GameObject currGameObject = t.gameObject;
            Renderer mRenderer = currGameObject.GetComponent<Renderer>();
            // if current game object has a renderer
            if (mRenderer != null){
                Color originalColor;
                _originalColors.TryGetValue(mRenderer.material, out originalColor);
                SwitchColorblind(mRenderer, visionParameters.ColorblindMatrix, originalColor);
            }
            // recurse over children
            foreach( Transform child in t) {
                SwitchColorblindRecursive(child, visionParameters);
            }
        }
    }

}
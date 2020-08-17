using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;
using UnityEngine.Rendering.Universal;

namespace Anivision.Vision
{
    public class RendererController : MonoBehaviour
    {
        [Tooltip("The forward renderer to apply renderer features to.")]
        public ForwardRendererData forwardRenderer;

        private Dictionary<VisionEffect, RendererEffect> effectsDictionary; // A dictionary matching VisionEffect enum values to RendererEffects scripts 
        private AnimalManager _animalManager; // The scene's animal manager
        private List<ScriptableRendererFeature> addedFeatures; // The list of renderer features added to the pipeline by this script

        private void Awake()
        {
            // Instantiate the effects dictionary
            effectsDictionary = new Dictionary<VisionEffect, RendererEffect>();
            // Find all renderer effect scripts on this game object, and add them to the dictionary
            RendererEffect[] effectsList = GetComponents<RendererEffect>();
            foreach (RendererEffect effect in effectsList)
            {
                if (!effectsDictionary.ContainsKey(effect.Effect))
                {
                    effectsDictionary.Add(effect.Effect, effect);
                }
            }

            // Instantiate the list of added features
            addedFeatures = new List<ScriptableRendererFeature>();
        }

        // Start is called before the first frame update
        void Start()
        {
            // Find an instance of the animal manager
            _animalManager = AnimalManager.Instance;

            // Ensure the animal manager exists before attempting to subscribe to it
            if (_animalManager != null)
            {
                // Subscribe the the vision switch event
                _animalManager.VisionSwitch.AddListener(UpdatePipeline);
            }
        }

        // Called when the application quits, but also when the editor exits play mode.
        // This is here for convenience, so the pipeline is reset whenever play mode is exited.
        private void OnApplicationQuit()
        {
            ResetPipeline(); // Reset the pipeline
        }

        private void OnDestroy()
        {
            ResetPipeline(); // Reset the pipeline
        }

        // Called when VisionSwitch is invoked
        // Iterates through the list of vision effects, and if one is in the effect dictionary, add it's feature to the pipeline
        private void UpdatePipeline(VisionParameters parameters)
        {
            // Reset the pipeline to remove renderer features added by previous animals
            ResetPipeline();
            // Iterate through the parameter's list of vision effects
            foreach (VisionEffect visionEffect in parameters.VisionEffects)
            {
                // If the current vision effect is in the effect dictionary, add it to the pipeline
                if (effectsDictionary[visionEffect])
                {
                    // Get the effect's scriptable renderer feature
                    ScriptableRendererFeature feature = effectsDictionary[visionEffect].GetRendererFeature(parameters);
                    // Add it to the pipeline, and the list of added features
                    forwardRenderer.rendererFeatures.Add(feature);
                    addedFeatures.Add(feature);
                }
            }
            // Tells unity that changes have been made to the renderer, and that it should update
            forwardRenderer.SetDirty();
        }

        // Resets all additions made to the pipeline by this script
        private void ResetPipeline()
        {
            // Iterate through the list of added features, and remove them from the piepline
            foreach (ScriptableRendererFeature feature in addedFeatures)
            {
                forwardRenderer.rendererFeatures.Remove(feature);
            }
            addedFeatures.Clear();
            forwardRenderer.SetDirty();
        }
    }
}


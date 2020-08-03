using System.Collections;
using UnityEngine;

namespace Anivision.Core
{
    public class HeadsetFade : MonoBehaviour
    {
        [Tooltip("The Fade material used in the Blit Renderer Feature in the active Forward Renderer.")]
        public Material fadeBlitMaterial;

        public delegate void FadeAction();
        public event FadeAction OnFadeStart;     // Called when a fade starts
        public event FadeAction OnFadeEnd;       // Called when a fade ends
        public event FadeAction OnUnfadeStart;   // Called when an unfade starts
        public event FadeAction OnUnfadeEnd;     // Called when an unfade ends

        [HideInInspector]
        public float fadePercent { get; set; } = 0;  // The percent the headset is currently faded (ranges from 0 to 1)
        private bool active = false;    // Whether the headset is currently fading or unfading


        void OnEnable()
        {
            // Start completely unfaded
            fadePercent = 0;
        }

        // Called when the application quits, but also when the editor exits play mode.
        // This is here for convenience, so if play mode is exited during a fade the fade will reset and the editor will be visible.
        void OnApplicationQuit()
        {
            // Reset the fade material's fade percent
            fadeBlitMaterial.SetFloat("_FadePercent", 0);
        }

        void Update()
        {
            // Set the fade material's fade percent to the current fade percent governed by fades and unfades\
            if (active)
            {
                fadeBlitMaterial.SetFloat("_FadePercent", fadePercent);

            }
        }

        // Returns whether the headset is fading or unfading
        public bool IsFadeActive()
        {
            return active;
        }

        // Starts a fade.
        // speed is how fast the fade occurs, and max is the fade percent to reach (1 means a full fade, .5 is a half fade, etc...)
        public void StartFade(float speed = 1, float max = 1)
        {
            active = true; // Note that the fader is active
            // Call the event for a fade start
            OnFadeStart?.Invoke();
            // Start the fade coroutine
            StartCoroutine(Fade(speed, max));
        }

        // Coroutine for the actual fade
        private IEnumerator Fade(float speed, float max)
        {
            // Until the fade percent reaches the max, keep fading
            while (fadePercent < max)
            {
                fadePercent += speed * Time.deltaTime;
                yield return null;
            }
            // Once the fade has completed, set it to the max (to eliminate small errors), and call the event for a fade end
            fadePercent = max;
            OnFadeEnd?.Invoke();
            active = false; // Note that the fader is no longer active
        }

        // Starts a unfade.
        // speed is how fast the unfade occurs, and min is the fade percent to reach (0 means a full unfade, .5 is a half unfade, etc...)
        public void StartUnfade(float speed = 1, float min = 0)
        {
            active = true; // Note that the fader is active
            // Call the event for a unfade start
            OnUnfadeStart?.Invoke();
            // Start the unfade coroutine
            StartCoroutine(Unfade(speed, min));
        }

        private IEnumerator Unfade(float speed, float min)
        {
            // Until the fade percent reaches the min, keep unfading
            while (fadePercent > min)
            {
                fadePercent -= speed * Time.deltaTime;
                yield return null;
            }
            // Once the unfade has completed, set it to the min (to eliminate small errors), and call the event for a unfade end
            fadePercent = min;
            OnUnfadeEnd?.Invoke();
            active = false; // Note that the fader is no longer active
        }
    }
}
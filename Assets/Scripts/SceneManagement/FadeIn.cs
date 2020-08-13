using UnityEngine;
using Anivision.Core;

namespace Anivision.SceneManagement
{
    public class FadeIn : MonoBehaviour
    {
        [Tooltip("Headset Fade Script to use for fading in and out when switching scenes.")]
        public HeadsetFade headsetFade;
        [Tooltip("How fast to fade in when the current scene is switched to.")]
        public float fadeSpeed = 2;

        void Start()
        {
            // Start an unfade
            headsetFade.fadePercent = 1;
            headsetFade.StartUnfade(fadeSpeed);
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;
using Anivision.Core;

namespace Anivision.SceneManagement
{
    // Fades into a new scene
    public class SceneSwitch : MonoBehaviour
    {
        [Tooltip("The name of the scene to switch to.")]
        public string sceneName = "LobbyScene";
        [Tooltip("The HeadsetFade script handling scene fades.")]
        public HeadsetFade headsetFade;
        [Tooltip("How fast to fade the headset.")]
        public float fadeSpeed = 2;

        private bool switching = false; // Whether the scene is in the process of switching

        private void Start()
        {
            // Start off with switching as false
            switching = false;
        }

        private void OnDisable()
        {
            // Remove the SwitchScene function from the OnFadeEnd callback
            headsetFade.OnFadeEnd -= SwitchScene;
        }

        // Starts the transition into the next scene
        public void StartTransition()
        {
            // Make sure that the scene isn't already switching
            if (!switching)
            {
                // Add to the OnFadeEnd callback so that when the fade ends the scene is switched
                // Done in this function instead of OnEnable so that multiple instances of this script can all exist without all changing the scene when a fade ends
                headsetFade.OnFadeEnd += SwitchScene;
                // Start a headset fade
                headsetFade.StartFade(fadeSpeed);
                // Node that the scene is in the process of switching
                switching = true;
            }
        }

        // Called when the fade ends, actually loads and switches to the new scene
        private void SwitchScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}

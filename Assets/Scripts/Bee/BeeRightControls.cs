using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;
using Anivision.PlayerInteraction;

namespace Anivision.Bee
{
    public class BeeRightControls : MonoBehaviour
    {
        [Tooltip("The default human skybox.")]
        public Material normalSkybox;
        [Tooltip("The skybox to use in bee vision.")]
        public Material beeSkybox;

        [Tooltip("The Camera Rig used to move the player when flying.")]
        public GameObject cameraRig;
        [Tooltip("The Center Eye object used to determine the headset's rotation when flying.")]
        public GameObject centerEye;
        [Tooltip("The wind particle system.")]
        public GameObject windParticles;

        [Tooltip("The Teleport script handling user teleportation.")]
        public TeleportController teleport;

        [Tooltip("A headset fader script to use to fade the headset when transitioning between flying and teleporting. Should be a unique instance used only in this script.")]
        public HeadsetFade headsetFade;
        [Tooltip("How quickly to fade the headset.")]
        public float headsetFadeSpeed = 1;
        [Tooltip("The audio for the wind noises while flying.")]
        public AudioSource windSound;
        [Tooltip("How quickly to fade the wind audio in and out.")]
        public float audioFadeSpeed = 1;

        [Tooltip("How fast the user flies.")]
        public float flySpeed = .06f;

        private InputManager _inputManager; // The input manager managing button presses
        private bool isFlying = false;      // Whether the user is currently flying
        private bool isNormalSkybox = true; // Whether the skybox is currently the normal human skybox

        private void Start()
        {
            // Start with teleport mode enabled
            teleport.enabled = true;
            isFlying = false;
            windParticles.SetActive(false);
            windSound.volume = 0;

            // Start with default skybox
            isNormalSkybox = true;
            SkyboxSwap();
        }

        void OnEnable()
        {
            // Set the input manager to an instance of an Input Manager
            _inputManager = InputManager.Instance;
            // Ensure an input manager has been set before continuing
            if (_inputManager == null)
            {
                throw new System.Exception("Must have an input manager script in the scene");
            }

            // If there is a valid input manager, attach callbacks to the necessary button presses
            if (_inputManager != null)
            {
                _inputManager.AttachInputHandler(StartSkyboxSwap, InputManager.InputState.ON_PRESS, InputManager.Button.A);
                _inputManager.AttachInputHandler(StartFade, InputManager.InputState.ON_PRESS, InputManager.Button.B);
            }
            // Make the movement transition function get called when a headset fade ends
            headsetFade.OnFadeEnd += movementTransition;
        }

        private void OnDisable()
        {
            // Remove callbacks that were added in OnEnable
            if (_inputManager != null)
            {
                _inputManager.DetachInputHandler(StartSkyboxSwap, InputManager.InputState.ON_PRESS, InputManager.Button.A);
                _inputManager.DetachInputHandler(StartFade, InputManager.InputState.ON_PRESS, InputManager.Button.B);
            }
            headsetFade.OnFadeEnd -= movementTransition;
        }

        // Initiates a headset fade
        private void StartFade()
        {
            headsetFade.StartFade(headsetFadeSpeed);
        }

        // Swaps the skybox
        private void StartSkyboxSwap()
        {
            isNormalSkybox = !isNormalSkybox;
            SkyboxSwap();
        }

        private void Update()
        {
            Fly();
        }

        // If flying is enabled, move the user based on head tilt (which tracks the headset)
        private void Fly()
        {
            if (isFlying == true)
            {
                Vector3 flyDir = centerEye.transform.forward;
                cameraRig.transform.position += flyDir.normalized * flySpeed;
            }
        }

        // Toggles between the human and bee vision skybox
        private void SkyboxSwap()
        {
            if (isNormalSkybox)
            {
                RenderSettings.skybox = normalSkybox;
            }
            else
            {
                RenderSettings.skybox = beeSkybox;
            }
        }

        // Transitions from flying to teleporting, or teleporting to flying.
        // Called when a fade transition ends
        private void movementTransition()
        {
            // Unfade the headset
            headsetFade.StartUnfade(headsetFadeSpeed);
            // Toggle the ability to teleport
            teleport.enabled = !teleport.enabled;
            // Toggle flying
            isFlying = !isFlying;
            // Fade in or out the wind sound
            StartCoroutine(FadeSound());
            // Toggle wind particles
            windParticles.SetActive(!windParticles.activeSelf);
        }

        // Either fades the wind sound in or out, depending on whether the user is flying
        private IEnumerator FadeSound()
        {
            if (isFlying == false) // Fade the sound out
            {
                while (windSound.volume > 0)
                {
                    windSound.volume -= Time.deltaTime * audioFadeSpeed;
                    yield return null;
                }
            }
            else // Fade the sound in
            {
                while (windSound.volume < 1)
                {
                    windSound.volume += Time.deltaTime * audioFadeSpeed;
                    yield return null;
                }
            }
        }
    }
}
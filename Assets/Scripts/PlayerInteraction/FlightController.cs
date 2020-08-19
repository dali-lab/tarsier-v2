using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.PlayerInteraction
{
    public class FlightController : MonoBehaviour
    {
        [Tooltip("The Camera Rig used to move the player when flying.")]
        public GameObject cameraRig;                                    // to move the player when flying and teleport player back to hive when they run out of health
        [Tooltip("The Center Eye object used to determine the headset's rotation when flying.")]
        public GameObject centerEye;                                    // for flying via headtilt
        [Tooltip("How fast the user flies.")]
        public float flySpeed = .06f;
        
        [Tooltip("A headset fader script to use to fade the headset when transitioning between flying and teleporting. Should be a unique instance used only in this script.")]
        public HeadsetFade headsetFade;
        [Tooltip("How quickly to fade the headset.")]
        public float headsetFadeSpeed = 1;
        [Tooltip("How quickly to fade the wind audio in and out.")]
        public float audioFadeSpeed = 1;
        
        private InputManager _inputManager;
        private TeleportController _teleportController;
        private AudioSource windSound;
        private GameObject windParticles;
        private bool isFlying = false;

        private void Awake()
        {
            //Get attached wind audio source and particles and save them
            windParticles = GetComponentInChildren<ParticleSystem>().gameObject;
            windSound = GetComponentInChildren<AudioSource>();
            windParticles.transform.parent = centerEye.transform;
            windParticles.transform.forward = centerEye.transform.forward * -1;
            windParticles.transform.localPosition = new Vector3(0f, 0f, 0.5f);
            windParticles.transform.localScale = new Vector3(0.05f, 0.05f, 0.01f);
            windParticles.SetActive(false);
            windSound.transform.parent = centerEye.transform;
            windSound.volume = 0;
        }

        private void Start()
        {
            // Set the teleport controller to an instance of teleport controller, if available
            _teleportController = TeleportController.Instance;
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
                _inputManager.DetachInputHandler(StartFade, InputManager.InputState.ON_PRESS, InputManager.Button.B);
            }
            
            headsetFade.OnFadeEnd -= movementTransition;
        }
        
        // Initiates a headset fade
        private void StartFade()
        {
            headsetFade.StartFade(headsetFadeSpeed);
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
        
        // Transitions from flying to teleporting, or teleporting to flying.
        // Called when a fade transition ends
        private void movementTransition()
        {
            // Unfade the headset
            headsetFade.StartUnfade(headsetFadeSpeed);
            // Toggle flying
            isFlying = !isFlying;
            // Toggle the ability to teleport
            if (_teleportController != null)
            {
                _teleportController.gameObject.SetActive(!isFlying);
            }
            // Fade in or out the wind sound
            StartCoroutine(FadeSound());
            // Toggle wind particles
            windParticles.SetActive(isFlying);
            
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

        private void EnableFlight(MovementParameters parameters)
        {
            ResetFlight(parameters);
            
        }

        private void ResetFlight(MovementParameters parameters)
        {
            if (isFlying && !parameters.CanFly)
            {
                isFlying = parameters.CanFly;
                windParticles.SetActive(false);
                windSound.volume = 0;
            }
            
            gameObject.SetActive(parameters.CanFly);
        
        }

    }
}

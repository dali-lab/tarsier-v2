using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.PlayerInteraction
{
    public class FlightController : MonoBehaviour
    {
        public GameObject cameraRig;                                    // to move the player when flying and teleport player back to hive when they run out of health
        public GameObject centerEye;                                    // for flying via headtilt
        public AudioSource windSound;
        public GameObject windParticles;
        public float speed = .06f;
        
        private InputManager _inputManager;
        private AnimalManager _animalManager;
        private TeleportController _teleportController;
        private bool isFlying = false;
        private bool canTeleport = false;

        private void Awake()
        {
            windParticles.transform.parent = centerEye.transform;
            windParticles.transform.localPosition = new Vector3(0f, 0f, 0.5f);
            windParticles.transform.localScale = new Vector3(0.05f, 0.05f, 0.01f);
            windParticles.SetActive(false);
            windSound.transform.parent = centerEye.transform;
            windSound.volume = 0;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _animalManager = AnimalManager.Instance;
            _teleportController = TeleportController.Instance;

            if (_animalManager != null)
            {
                _animalManager.MovementSwitch.AddListener(EnableFlight);
            }
            if (_inputManager == null)
            {
                throw new System.Exception("Must have an input manager script in the scene");
            }
            
        }

        void OnEnable()
        {

            if (_inputManager == null)
            {
                _inputManager = InputManager.Instance;
            }
            if (_inputManager != null)
            {
                _inputManager.AttachInputHandler(StartMovementTransition, InputManager.InputState.ON_PRESS, InputManager.Button.B);
            }
        }

        private void StartMovementTransition()                        // fly: trigger transition, toggle teleport
        {
            StartCoroutine(movementTransition());
        }

        private void Update()
        {
            Fly();
        }

        private void Fly()                                              // fly via head tilt (tracks headset)
        {
            if (isFlying)
            {
                Vector3 flyDir = centerEye.transform.forward;
                cameraRig.transform.position += flyDir.normalized * speed;
            }
        }
    
        private IEnumerator movementTransition()                        // fade to black and unfade for transition
        {
            isFlying = !isFlying;
            windParticles.SetActive(isFlying);
            if (canTeleport && _teleportController != null)
            {
                _teleportController.gameObject.SetActive(!isFlying);
            }

            if (isFlying == false)                                       // fade out sound
            {
                float startVolume = windSound.volume;
                while (windSound.volume > 0)
                {
                    windSound.volume -= startVolume * Time.deltaTime / 1;
                    yield return null;
                }
            }
            else                                                        // fade in sound
            {
                while (windSound.volume < 1)
                {
                    windSound.volume += 1 * Time.deltaTime / 1;
                    yield return null;
                }
            }
        }

        private void EnableFlight(MovementParameters parameters)
        {
            canTeleport = parameters.CanTeleport;
            gameObject.SetActive(parameters.CanFly);
        }
        
        private void OnDisable()
        {
            if (_inputManager != null)
            {
                _inputManager.DetachInputHandler(StartMovementTransition, InputManager.InputState.ON_PRESS, InputManager.Button.B);
            }
        
        }

    }
}

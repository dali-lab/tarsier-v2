using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using UnityEngine;

namespace Anivision.PlayerInteraction
{
    public class Fly : MonoBehaviour
    {
        public GameObject cameraRig;                                    // to move the player when flying and teleport player back to hive when they run out of health
        public GameObject centerEye;                                    // for flying via headtilt
        public AudioSource windSound;
        public float speed = .06f;
        
        private InputManager _inputManager;
        private AnimalManager _animalManager;
        private Teleport _teleport;
        private bool isFlying = false;
    
        // Start is called before the first frame update
        private void Start()
        {
            _animalManager = AnimalManager.Instance;
            
            if (_animalManager != null)
            {
                _animalManager.MovementSwitch.AddListener((MovementParameters parameters) => {gameObject.SetActive(parameters.CanFly);});
            }
            if (_inputManager == null)
            {
                throw new System.Exception("Must have an input manager script in the scene");
            }
            
            _teleport = Teleport.Instance;
        }

        void OnEnable()
        {

            if (_inputManager == null)
            {
                _inputManager = InputManager.Instance;
            }
            if (_inputManager != null)
            {
                _inputManager.AttachInputHandler(StartMovementTransition, InputManager.InputState.ON_PRESS, InputManager.Button.A);
            }
        }

        private void StartMovementTransition()                        // fly: trigger transition, toggle teleport
        {
            StartCoroutine(movementTransition());
            if (Teleport.Instance != null)
            {
                Teleport.Instance.gameObject.SetActive(!Teleport.Instance.gameObject.activeSelf);
            }
        }
    
        private IEnumerator movementTransition()                        // fade to black and unfade for transition
        {
            isFlying = !isFlying;

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
        
        private void OnDisable()
        {
            if (_inputManager != null)
            {
                _inputManager.DetachInputHandler(StartMovementTransition, InputManager.InputState.ON_PRESS, InputManager.Button.A);
            }
        
        }

    }
}

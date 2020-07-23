using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.PlayerInteraction;
using Anivision.Vision;
using UnityEngine;
using UnityEngine.Events;

namespace Anivision.Core
{
    /// <summary>
    /// This script switches between animal prefabs on button press. Calls the vision and movement events when button is pressed
    /// </summary>
    public class AnimalManager : MonoBehaviour
    {
        public static AnimalManager Instance; //singleton instance
        
        [Tooltip("List of animals supported by the scene")]
        public AnimalController[] AnimalControllers; //list of animals that the scene supports
        [Tooltip("The button that the animal switch should be linked to")]
        public InputManager.Button AnimalSwitchButton;
        public UnityEvent<VisionParameters> VisionSwitch; //called when animal is switched 
        public UnityEvent<MovementParameters> MovementSwitch; //called when animal is switched 
        
        private int index = 0; //used to keep track of current animal in list
        private InputManager _inputManager;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);

            } else
            {
                Instance = this;
                if (VisionSwitch == null)
                {
                    VisionSwitch = new VisionSwitchEvent();
                }
                
                if (MovementSwitch == null)
                {
                    MovementSwitch = new MovementSwitchEvent();
                }
            }

        }
        
        // Start is called before the first frame update
        void Start()
        {
            
            // _inputManager = InputManager.Instance;
            //
            // if (_inputManager == null)
            // {
            //     throw new Exception("There must be an instance of the InputManager script in the scene");
            // }
            
            // _inputManager.AttachInputHandler(SwitchAnimal, InputManager.InputState.ON_PRESS, AnimalSwitchButton);
            SwitchAnimal();
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                SwitchAnimal();
            }
        }

        private void SwitchAnimal()
        {
            if (AnimalControllers.Length > 0)
            {
                if (VisionSwitch != null) VisionSwitch.Invoke(AnimalControllers[index].VisionParameters);
                if (MovementSwitch != null) MovementSwitch.Invoke(AnimalControllers[index].MovementParameters);
                if (index == AnimalControllers.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
        }

        private void OnDestroy()
        {
            _inputManager.DetachInputHandler(SwitchAnimal, InputManager.InputState.ON_PRESS, AnimalSwitchButton);
        }
    }
    
    public class VisionSwitchEvent : UnityEvent<VisionParameters>
    {
    }
    
    public class MovementSwitchEvent : UnityEvent<MovementParameters>
    {
    }
}


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
        private static AnimalManager _animalManager;
        //singleton instance
        public static AnimalManager Instance { get
        {
            if (!_animalManager)
            {
                _animalManager = FindObjectOfType (typeof (AnimalManager)) as AnimalManager;

                if (!_animalManager)
                {
                    UnityEngine.Debug.LogError("There needs to be one active InputManger script on a GameObject in your scene.");
                }
            }

            return _animalManager;
        } } 

        
        [Tooltip("List of animals supported by the scene")]
        public AnimalController[] AnimalControllers; //list of animals that the scene supports
        [Tooltip("The button that the animal switch should be linked to")]
        public InputManager.Button AnimalSwitchButton;
        public class VisionSwitchEvent : UnityEvent<VisionParameters>{}
        public class MovementSwitchEvent : UnityEvent<MovementParameters>{}
        public VisionSwitchEvent VisionSwitch = new VisionSwitchEvent(); //called when animal is switched 
        public MovementSwitchEvent MovementSwitch = new MovementSwitchEvent(); //called when animal is switched 
        
        private int _index = 0; //used to keep track of current animal in list
        private InputManager _inputManager;

        // Start is called before the first frame update
        private void Start()
        {
            
            // _inputManager = InputManager.Instance;
            //
            // if (_inputManager == null)
            // {
            //     throw new Exception("There must be an instance of the InputManager script in the scene");
            // }
            
            // _inputManager.AttachInputHandler(SwitchAnimal, InputManager.InputState.ON_PRESS, AnimalSwitchButton);
            SwitchAnimal(); //switch animal to first animal in the list
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                SwitchAnimal();
            }
        }
        
        //switch the animal that we are currently on to the next one in the list
        private void SwitchAnimal()
        {
            if (AnimalControllers.Length > 0)
            {
                VisionSwitch.Invoke(AnimalControllers[_index].VisionParameters);
                MovementSwitch.Invoke(AnimalControllers[_index].MovementParameters);
                if (_index == AnimalControllers.Length - 1)
                {
                    _index = 0;
                }
                else
                {
                    _index++;
                }
            }
        }

        private void OnDestroy()
        {
            // _inputManager.DetachInputHandler(SwitchAnimal, InputManager.InputState.ON_PRESS, AnimalSwitchButton);
        }
    }
    
    
}


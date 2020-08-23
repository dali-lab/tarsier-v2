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
                    UnityEngine.Debug.LogError("There needs to be one active AnimalManager script on a GameObject in your scene.");
                }
            }

            return _animalManager;
        } } 

        
        [Tooltip("List of animals supported by the scene")]
        public AnimalController[] AnimalControllers; //list of animals that the scene supports
        
        [Tooltip("The native animal for the scene")]
        public AnimalController defaultAnimal;
        public class VisionSwitchEvent : UnityEvent<VisionParameters>{}
        public class AnimalSwitchEvent : UnityEvent<Animal>{}
        public VisionSwitchEvent VisionSwitch = new VisionSwitchEvent(); //called when animal is switched 
        public AnimalSwitchEvent AnimalSwitch = new AnimalSwitchEvent();
        [Tooltip("Button used to switch between human vision and the current animal of the scene")]
        public InputManager.Button visionSwapButton = InputManager.Button.A;
        [HideInInspector]
        public Animal currentAnimalToSwitch { get; set; }
        
        private Dictionary<Animal, AnimalController> _animalControllerDict;
        private bool initialSetup = false;
        private Animal currentVision = Animal.Human;

        private InputManager inputManager;
        // Start is called before the first frame update

        private void Awake()
        {
            if (defaultAnimal != null)
            {
                currentAnimalToSwitch = defaultAnimal.animal;
            }
            
            _animalControllerDict = new Dictionary<Animal, AnimalController>();
            
            //build dictionary for easy access later
            foreach (AnimalController controller in AnimalControllers)
            {
                if (!_animalControllerDict.ContainsKey(controller.animal))
                {
                    _animalControllerDict.Add(controller.animal, controller);
                }
                else
                {
                    UnityEngine.Debug.LogError("Animal type already declared. Skipping add to dictionary.");
                }
            }
        }

        private void Start()
        {
            inputManager = InputManager.Instance;
            if (inputManager != null)
            {
                inputManager.AttachInputHandler(SwitchHumanAnimal, InputManager.InputState.ON_PRESS, visionSwapButton);
            }
        }

        private void OnDestroy()
        {
            if (inputManager != null)
            {
                inputManager.DetachInputHandler(SwitchHumanAnimal, InputManager.InputState.ON_PRESS, visionSwapButton);
            }
        }

        private void Update()
        {
            if (!initialSetup && _animalControllerDict.ContainsKey(Animal.Human))
            {
                if (_animalControllerDict.ContainsKey(Animal.Human))
                {
                    SwitchAnimal(Animal.Human, false);
                } else
                {
                    UnityEngine.Debug.LogError("Must have human prefab as one of the animals");
                }
                
                initialSetup = true;
            }
            
        }


        //switch the animal to the desired one and invoke events
        public void SwitchAnimal(Animal animal, bool setAsNewAnimal = true)
        {
            AnimalController animalController;
            if (_animalControllerDict.TryGetValue(animal, out animalController))
            {
                VisionSwitch.Invoke(animalController.VisionParameters);
                AnimalSwitch.Invoke(animal);
                currentVision = animal;
                
                if (setAsNewAnimal)
                {
                    currentAnimalToSwitch = animal;
                }
            }
            
        }
        
        public void SwitchHumanAnimal()
        {
            if (currentVision == Animal.Human)
            {
                SwitchAnimal(currentAnimalToSwitch, false);
            }
            else
            {
                SwitchAnimal(Animal.Human, false);
            }
        }
    }
    
    
}


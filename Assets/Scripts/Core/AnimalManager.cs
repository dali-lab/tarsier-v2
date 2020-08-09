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

        public AnimalController defaultAnimal;
        public class VisionSwitchEvent : UnityEvent<VisionParameters>{}
        public class MovementSwitchEvent : UnityEvent<MovementParameters>{}
        public VisionSwitchEvent VisionSwitch = new VisionSwitchEvent(); //called when animal is switched 
        public MovementSwitchEvent MovementSwitch = new MovementSwitchEvent(); //called when animal is switched 
        
        private Dictionary<Animal, AnimalController> _animalControllerDict;

        private int index;

        private bool setDefault = false;
        // Start is called before the first frame update
        private void Start()
        {
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

        private void Update()
        {
            if (!setDefault && defaultAnimal != null)
            {
                SwitchAnimal(defaultAnimal.animal);
                setDefault = true;
            }
        }


        //switch the animal to the desired one and invoke events
        public void SwitchAnimal(Animal animal)
        {
            AnimalController animalController;
            if (_animalControllerDict.TryGetValue(animal, out animalController))
            {
                VisionSwitch.Invoke(animalController.VisionParameters);
                MovementSwitch.Invoke(animalController.MovementParameters);
            }
        }
    }
    
    
}


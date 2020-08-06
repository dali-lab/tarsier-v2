using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using JetBrains.Annotations;
using UnityEngine;

public class SwitchAnimalTest : MonoBehaviour
{
    public Animal[] animalsToSwitch;
    
    private InputManager _inputManager;
    [CanBeNull] private AnimalManager _animalManager;
    private int currentIndex = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        _inputManager = InputManager.Instance;
        _animalManager = AnimalManager.Instance;

        if (_inputManager != null)
        {
            _inputManager.AttachInputHandler(SwitchAnimal, InputManager.InputState.ON_PRESS, InputManager.Button.A);
        }
        
    }

    // private void Update()
    // {
    //     if (Input.anyKeyDown)
    //     {
    //         SwitchAnimal();
    //     }
    // }

    private void OnDisable()
    {
        if (_inputManager != null)
        {
            _inputManager.DetachInputHandler(SwitchAnimal, InputManager.InputState.ON_PRESS, InputManager.Button.A);
        } 
    }

    void SwitchAnimal()
    {
        if (animalsToSwitch != null)
        {
            _animalManager?.SwitchAnimal(animalsToSwitch[currentIndex % animalsToSwitch.Length]);
            currentIndex++;
        }
        
    }
}

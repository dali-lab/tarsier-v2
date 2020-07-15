using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

namespace Anivision.PlayerInteraction
{
    public class Grabber : MonoBehaviour
    {
        [Tooltip("Button to press to grab")]
        public Button grabButton;
        
        // The input manager to use to check for button presses
        private InputManager inputManager;

        void Start()
        {
            // Find an input manager to use
            inputManager = InputManager.Instance;
        }

        // Return whether this grabber is grabbing (true if the button is pressed)
        public bool IsGrabbing()
        {
            return inputManager.IsButtonPressed(grabButton);
        }
    }
}

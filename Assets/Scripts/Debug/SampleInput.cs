using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Anivision.Core;

namespace Anivision.Debug
{
    /// <summary>
    /// This script is used to debug and test the InputManager.
    /// Can also be referred to as an example on how to use the InputManager.
    /// </summary>

    public class SampleInput : MonoBehaviour
    {
        [Header("Button Input")]
        [Space(10f)]
        public InputManager.Button Button;
        public bool DemoButtonPress;
        public bool DemoButtonRelease;
        public bool DemoButtonTouchStart;
        public bool DemoButtonTouchEnd;

        [Header("Joystick Input")]
        public InputManager.Joystick Joystick;
        public bool DemoJoystickMovement;
        public bool DemoJoystickMonitor;

        [Header("Trigger Input")]
        public InputManager.Trigger Trigger;
        public bool DemoTriggerMonitor;

        [Header("Grip Input")]
        public InputManager.Grip Grip;
        public bool DemoGripMonitor;

        [Space(10f)]
        public TextMesh ForceDisplay;
        public TextMesh DebugDisplay;

        private Color buttonDownColor = Color.red;
        private Color buttonTouchColor = Color.blue;
        private Color originalColor = Color.white;

        private MaterialPropertyBlock _propBlock;
        private Renderer _renderer;
        private InputManager _InputManager;


        private void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            _renderer = gameObject.GetComponent<Renderer>();
            TurnColor(originalColor);
        }

        void Start()
        {
            _InputManager = InputManager.Instance; //get singleton instance of InputManager

            if (_InputManager != null)
            {
                AttachCallbacks();
            }
        }

        private void OnDestroy()
        {
            //avoid memory leaks! Detach callbacks when gameObject in destroyed
            if (_InputManager != null)
            {
                DetachCallbacks();
            }

        }

        //attach the callbacks
        private void AttachCallbacks()
        {
            //attach button callbacks
            if (DemoButtonPress) _InputManager.AttachInputHandler(OnButtonDown, InputManager.InputState.ON_PRESS, Button);
            if (DemoButtonRelease) _InputManager.AttachInputHandler(OnButtonUp, InputManager.InputState.ON_RELEASE, Button);
            if (DemoButtonTouchStart) _InputManager.AttachInputHandler(OnButtonTouchStart, InputManager.InputState.ON_TOUCH_START, Button);
            if (DemoButtonTouchEnd) _InputManager.AttachInputHandler(OnButtonTouchEnd, InputManager.InputState.ON_TOUCH_END, Button);

            //attach joystick callbacks
            if (DemoJoystickMovement) _InputManager.AttachInputHandler(OnJoystickMovement, Joystick);
            if (DemoJoystickMonitor) _InputManager.AttachInputHandler(OnJoystickMonitor, Joystick);

            //attach trigger callback
            if(DemoTriggerMonitor) _InputManager.AttachInputHandler(OnButtonMonitor, Trigger);

            //attach grip callback
            if (DemoGripMonitor) _InputManager.AttachInputHandler(OnButtonMonitor, Grip);
        }

        //detach the callbacks
        private void DetachCallbacks()
        {
            //detach button callbacks
            if (DemoButtonPress) _InputManager.DetachInputHandler(OnButtonDown, InputManager.InputState.ON_PRESS, Button);
            if (DemoButtonRelease) _InputManager.DetachInputHandler(OnButtonUp, InputManager.InputState.ON_RELEASE, Button);
            if (DemoButtonTouchStart) _InputManager.DetachInputHandler(OnButtonTouchStart, InputManager.InputState.ON_TOUCH_START, Button);
            if (DemoButtonTouchEnd) _InputManager.DetachInputHandler(OnButtonTouchEnd, InputManager.InputState.ON_TOUCH_END, Button);

            //detach joystick callbacks
            if (DemoJoystickMovement) _InputManager.DetachInputHandler(OnJoystickMovement, Joystick);
            if (DemoJoystickMonitor) _InputManager.DetachInputHandler(OnJoystickMonitor, Joystick);

            //detach trigger callback
            if (DemoTriggerMonitor) _InputManager.DetachInputHandler(OnButtonMonitor, Trigger);

            //detach grip callback
            if (DemoGripMonitor) _InputManager.DetachInputHandler(OnButtonMonitor, Grip);
        }


        //display joystick movement floats
        private void OnJoystickMonitor(Vector2 v)
        {
            if (ForceDisplay != null)
            {
                ForceDisplay.text = "X: " + v.x + " Y:" + v.y;
            }

        }

        //display joystick movement direction
        private void OnJoystickMovement(InputManager.Direction direction)
        {
            if (DebugDisplay != null)
            {
                DebugDisplay.text = "Joystick moved: " + direction;
            }
        }

        //change cube color when button is released, show debug message
        private void OnButtonUp()
        {
            TurnColor(originalColor);

            if (DebugDisplay != null)
            {
                DebugDisplay.text = "Button Released";
            }
        }

        //change cube color when touch capacitive button is touched, show debug message
        private void OnButtonTouchStart()
        {
            TurnColor(buttonTouchColor);

            if (DebugDisplay != null)
            {
                DebugDisplay.text = "Button Touched";
            }

        }

        //change cube color when touch capacitive button is untouched, show debug message
        private void OnButtonTouchEnd()
        {
            TurnColor(originalColor);

            if (DebugDisplay != null)
            {
                DebugDisplay.text = "Button Untouched";
            }

        }

        //change cube color when button is pressed, show debug message
        private void OnButtonDown()
        {
            TurnColor(buttonDownColor);

            if (DebugDisplay != null)
            {
                DebugDisplay.text = "Button Pressed";
            }
        }

        //display force used to press triggers
        private void OnButtonMonitor(float v)
        {
            if (ForceDisplay != null)
            {
                ForceDisplay.text = "Force: " + v;
            }
        }

        private void TurnColor(Color color)
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_BaseColor", color);
            _renderer.SetPropertyBlock(_propBlock);
        }
    }
}


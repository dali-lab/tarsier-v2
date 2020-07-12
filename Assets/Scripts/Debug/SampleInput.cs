using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

namespace Anivision.Debug
{
    /// <summary>
    /// This script is used to debug and test the InputManager.
    /// Can also be referred to as an example on how to use the InputManager.
    /// </summary>

    public class SampleInput : MonoBehaviour
    {
        //booleans that can be selected based on what you want to test
        [Header("Button A Input")]
        public bool DemoButtonAPress;
        public bool DemoButtonARelease;
        public bool DemoButtonATouch;

        [Header("Button B Input")]
        public bool DemoButtonBPress;
        public bool DemoButtonBRelease;
        public bool DemoButtonBTouch;

        [Header("Button X Input")]
        public bool DemoButtonXPress;
        public bool DemoButtonXRelease;
        public bool DemoButtonXTouch;

        [Header("Button Y Input")]
        public bool DemoButtonYPress;
        public bool DemoButtonYRelease;
        public bool DemoButtonYTouch;

        [Header("Right Joystick Input")]
        public bool DemoRightJoystickPress;
        public bool DemoRightJoystickRelease;
        public bool DemoRightJoystickTouch;
        public bool DemoRightJoystickMovement;
        public bool DemoRightJoystickMonitor;

        [Header("Left Joystick Input")]
        public bool DemoLeftJoystickPress;
        public bool DemoLeftJoystickRelease;
        public bool DemoLeftJoystickTouch;
        public bool DemoLeftJoystickMovement;
        public bool DemoLeftJoystickMonitor;

        [Header("Right Grip Input")]
        public bool DemoRightGripPress;
        public bool DemoRightGripRelease;
        public bool DemoRightGripMonitor;

        [Header("Left Grip Input")]
        public bool DemoLeftGripPress;
        public bool DemoLeftGripRelease;
        public bool DemoLeftGripMonitor;

        [Header("Right Trigger Input")]
        public bool DemoRightTriggerPress;
        public bool DemoRightTriggerRelease;
        public bool DemoRightTriggerMonitor;

        [Header("Left Trigger Input")]
        public bool DemoLeftTriggerPress;
        public bool DemoLeftTriggerRelease;
        public bool DemoLeftTriggerMonitor;

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

            //attach all callbacks to input manager
            AttachPressCallbacks();
            AttachReleaseCallbacks();
            AttachTouchCallbacks();
            AttachMonitorCallbacks();
            AttachMovementCallbacks();
        }

        private void OnDestroy()
        {
            //avoid memory leaks! Detach callbacks when gameObject in destroyed
            DetachPressCallbacks();
            DetachTouchCallbacks();
            DetachReleaseCallbacks();
            DetachMonitorCallbacks();
            DetachMovementCallbacks();

        }

        //attach the callbacks that are called when buttons are pressed
        private void AttachPressCallbacks()
        {
            if (DemoButtonAPress) _InputManager.OnButtonAPress += OnButtonDown;
            if (DemoButtonBPress) _InputManager.OnButtonBPress += OnButtonDown;
            if (DemoButtonXPress) _InputManager.OnButtonXPress += OnButtonDown;
            if (DemoButtonYPress) _InputManager.OnButtonYPress += OnButtonDown;
            if (DemoLeftJoystickPress) _InputManager.OnLeftJoystickPress += OnButtonDown;
            if (DemoRightJoystickPress) _InputManager.OnRightJoystickPress += OnButtonDown;
            if (DemoRightGripPress) _InputManager.OnRightGripPress += OnButtonDown;
            if (DemoLeftGripPress) _InputManager.OnLeftGripPress += OnButtonDown;
            if (DemoRightTriggerPress) _InputManager.OnRightTriggerPress += OnButtonDown;
            if (DemoLeftTriggerPress) _InputManager.OnLeftTriggerPress += OnButtonDown;

        }

        //attach the callbacks that are called when buttons are pressed
        private void DetachPressCallbacks()
        {
            if (DemoButtonAPress) _InputManager.OnButtonAPress -= OnButtonDown;
            if (DemoButtonBPress) _InputManager.OnButtonBPress -= OnButtonDown;
            if (DemoButtonXPress) _InputManager.OnButtonXPress -= OnButtonDown;
            if (DemoButtonYPress) _InputManager.OnButtonYPress -= OnButtonDown;
            if (DemoLeftJoystickPress) _InputManager.OnLeftJoystickPress -= OnButtonDown;
            if (DemoRightJoystickPress) _InputManager.OnRightJoystickPress -= OnButtonDown;
            if (DemoRightGripPress) _InputManager.OnRightGripPress -= OnButtonDown;
            if (DemoLeftGripPress) _InputManager.OnLeftGripPress -= OnButtonDown;
            if (DemoRightTriggerPress) _InputManager.OnRightTriggerPress -= OnButtonDown;
            if (DemoLeftTriggerPress) _InputManager.OnLeftTriggerPress -= OnButtonDown;

        }

        //attach the callbacks that are called when buttons are released
        private void AttachReleaseCallbacks()
        {
            if (DemoButtonARelease) _InputManager.OnButtonARelease += OnButtonUp;
            if (DemoButtonBRelease) _InputManager.OnButtonBRelease += OnButtonUp;
            if (DemoButtonXRelease) _InputManager.OnButtonXRelease += OnButtonUp;
            if (DemoButtonYRelease) _InputManager.OnButtonYRelease += OnButtonUp;
            if (DemoLeftJoystickRelease) _InputManager.OnLeftJoystickRelease += OnButtonUp;
            if (DemoRightJoystickRelease) _InputManager.OnRightJoystickRelease += OnButtonUp;
            if (DemoRightGripRelease) _InputManager.OnRightGripRelease += OnButtonUp;
            if (DemoLeftGripRelease) _InputManager.OnLeftGripRelease += OnButtonUp;
            if (DemoRightTriggerRelease) _InputManager.OnRightTriggerRelease += OnButtonUp;
            if (DemoLeftTriggerRelease) _InputManager.OnLeftTriggerRelease += OnButtonUp;

        }

        //detach the callbacks that are called when buttons are released
        private void DetachReleaseCallbacks()
        {
            if (DemoButtonARelease) _InputManager.OnButtonARelease -= OnButtonUp;
            if (DemoButtonBRelease) _InputManager.OnButtonBRelease -= OnButtonUp;
            if (DemoButtonXRelease) _InputManager.OnButtonXRelease -= OnButtonUp;
            if (DemoButtonYRelease) _InputManager.OnButtonYRelease -= OnButtonUp;
            if (DemoLeftJoystickRelease) _InputManager.OnLeftJoystickRelease -= OnButtonUp;
            if (DemoRightJoystickRelease) _InputManager.OnRightJoystickRelease -= OnButtonUp;
            if (DemoRightGripRelease) _InputManager.OnRightGripRelease -= OnButtonUp;
            if (DemoLeftGripRelease) _InputManager.OnLeftGripRelease -= OnButtonUp;
            if (DemoRightTriggerRelease) _InputManager.OnRightTriggerRelease -= OnButtonUp;
            if (DemoLeftTriggerRelease) _InputManager.OnLeftTriggerRelease -= OnButtonUp;
        }

        //attach the callbacks that are called when capacitive touch buttons are touched
        private void AttachTouchCallbacks()
        {
            if (DemoButtonATouch) _InputManager.OnButtonATouch += OnButtonTouch;
            if (DemoButtonBTouch) _InputManager.OnButtonBTouch += OnButtonTouch;
            if (DemoButtonXTouch) _InputManager.OnButtonXTouch += OnButtonTouch;
            if (DemoButtonYTouch) _InputManager.OnButtonYTouch += OnButtonTouch;
            if (DemoLeftJoystickTouch) _InputManager.OnLeftJoystickTouch += OnButtonTouch;
            if (DemoRightJoystickTouch) _InputManager.OnRightJoystickTouch += OnButtonTouch;
        }

        //detach the callbacks that are called when capacitive touch buttons are touched
        private void DetachTouchCallbacks()
        {
            if (DemoButtonATouch) _InputManager.OnButtonATouch -= OnButtonTouch;
            if (DemoButtonBTouch) _InputManager.OnButtonBTouch -= OnButtonTouch;
            if (DemoButtonXTouch) _InputManager.OnButtonXTouch -= OnButtonTouch;
            if (DemoButtonYTouch) _InputManager.OnButtonYTouch -= OnButtonTouch;
            if (DemoLeftJoystickTouch) _InputManager.OnLeftJoystickTouch -= OnButtonTouch;
            if (DemoRightJoystickTouch) _InputManager.OnRightJoystickTouch -= OnButtonTouch;
        }

        //attach the callbacks that are called when joystick movement is detected
        private void AttachMovementCallbacks()
        {
            if (DemoRightJoystickMovement) _InputManager.OnRightJoystickMovement += OnJoystickMovement;
            if (DemoLeftJoystickMovement) _InputManager.OnLeftJoystickMovement += OnJoystickMovement;
        }

        //detach the callbacks that are called when joystick movement is detected
        private void DetachMovementCallbacks()
        {
            if (DemoRightJoystickMovement) _InputManager.OnRightJoystickMovement -= OnJoystickMovement;
            if (DemoLeftJoystickMovement) _InputManager.OnLeftJoystickMovement -= OnJoystickMovement;
        }

        //attach the callbacks that are called to monitor force of triggers and joystick movement
        private void AttachMonitorCallbacks()
        {
            if (DemoLeftGripMonitor) _InputManager.LeftGripMonitor += OnButtonMonitor;
            if (DemoRightGripMonitor) _InputManager.RightGripMonitor += OnButtonMonitor;
            if (DemoRightJoystickMonitor) _InputManager.RightJoystickMonitor += OnJoystickMonitor;
            if (DemoLeftJoystickMonitor) _InputManager.LeftJoystickMonitor += OnJoystickMonitor;
            if (DemoLeftTriggerMonitor) _InputManager.LeftTriggerMonitor += OnButtonMonitor;
            if (DemoRightTriggerMonitor) _InputManager.RightTriggerMonitor += OnButtonMonitor;

        }

        //detach the callbacks that are called to monitor force of triggers and joystick movement
        private void DetachMonitorCallbacks()
        {
            if (DemoLeftGripMonitor) _InputManager.LeftGripMonitor -= OnButtonMonitor;
            if (DemoRightGripMonitor) _InputManager.RightGripMonitor -= OnButtonMonitor;
            if (DemoRightJoystickMonitor) _InputManager.RightJoystickMonitor -= OnJoystickMonitor;
            if (DemoLeftJoystickMonitor) _InputManager.LeftJoystickMonitor -= OnJoystickMonitor;
            if (DemoLeftTriggerMonitor) _InputManager.LeftTriggerMonitor -= OnButtonMonitor;
            if (DemoRightTriggerMonitor) _InputManager.RightTriggerMonitor -= OnButtonMonitor;
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
        private void OnJoystickMovement(JoystickMovement direction)
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

        //change cube color when touch capacitive button is touched/untouched, show debug message
        private void OnButtonTouch(bool touched)
        {
            if (touched)
            {
                TurnColor(buttonTouchColor);

                if (DebugDisplay != null)
                {
                    DebugDisplay.text = "Button Touched";
                }

            }
            else
            {
                TurnColor(originalColor);

                if (DebugDisplay != null)
                {
                    DebugDisplay.text = "Button Untouched";
                }
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


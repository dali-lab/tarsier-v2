using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Anivision.Core
{
    /// <summary>
    /// This script allows other scripts to subscribe to events that are executed when buttons on the
    /// Oculus Touch Controllers are pressed, released, and touched
    /// Also allows scripts to subscribe to events when joystick is moved
    ///
    /// </summary>

    public enum JoystickMovement
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public sealed class InputManager : MonoBehaviour
    {

        public static InputManager Instance { get; private set; }

        /* Note: this script uses virtual mappings. Generally, the mappings follow as below:
         * 
         * OVRInput.Button.One: Button A
         * OVRInput.Button.Two: Button B
         * OVRInput.Button.Three: Button X
         * OVRInput.Button.Four: Button Y
         * OVRInput.Button.PrimaryThumbstick: Left Joystick
         * OVRInput.Button.SecondaryThumbstick: Right Joystick
         * OVRInput.Axis1D.PrimaryHandTrigger: Left Grip
         * OVRInput.Axis1D.SecondaryHandTrigger: Right Grip
         * OVRInput.Axis1D.PrimaryIndexTrigger: Left Trigger
         * OVRInput.Axis1D.SecondaryIndexTrigger: Right Trigger
         * 
         * But this may change if your controllers are somehow re-mapped.
         */

        public UnityAction<bool> onHasController = null; //Called when controllers' connection state changes

        //Right Controller Events
        public UnityAction OnButtonAPress = null; //Button A Pressed
        public UnityAction OnButtonARelease = null; //Button A Released
        public UnityAction<bool> OnButtonATouch = null; //Button A Touch state has changed

        public UnityAction OnButtonBPress = null; //Button B Pressed
        public UnityAction OnButtonBRelease = null; //Button B Released
        public UnityAction<bool> OnButtonBTouch = null; //Button B Touch state has changed

        public UnityAction OnRightJoystickPress = null; //Right Joystick Pressed
        public UnityAction OnRightJoystickRelease = null; //Right Joystick Released
        public UnityAction<bool> OnRightJoystickTouch = null; //Right joystick touch state has changed

        public UnityAction<JoystickMovement> OnRightJoystickMovement = null; //Right Joystick moved more than halfway in a direction
        public UnityAction<Vector2> RightJoystickMonitor = null; //Monitor right joystick movement

        public UnityAction OnRightGripPress = null; //Right Grip Button Pressed
        public UnityAction OnRightGripRelease = null; //Right Grip Button Released
        public UnityAction<float> RightGripMonitor = null; //Monitor force on right grip button

        public UnityAction OnRightTriggerPress = null; //Right Trigger Button Pressed
        public UnityAction OnRightTriggerRelease = null; //Right Trigger Button Released
        public UnityAction<float> RightTriggerMonitor = null; //Monitor force on right trigger button

        //Left Controller Events
        public UnityAction OnButtonXPress = null; //Button X Pressed
        public UnityAction OnButtonXRelease = null; //Button X Released
        public UnityAction<bool> OnButtonXTouch = null; //Button X touch state has changed

        public UnityAction OnButtonYPress = null; //Button Y Pressed
        public UnityAction OnButtonYRelease = null; //Button Y Released
        public UnityAction<bool> OnButtonYTouch = null; //Button Y touch state has changed

        public UnityAction OnLeftJoystickPress = null; //Left Joystick Pressed
        public UnityAction OnLeftJoystickRelease = null; //Left Joystick Released
        public UnityAction<bool> OnLeftJoystickTouch = null; //Left joystick touch state has changed

        public UnityAction<JoystickMovement> OnLeftJoystickMovement = null; //Left Joystick moved more than halfway in a direction       
        public UnityAction<Vector2> LeftJoystickMonitor = null; //Monitor left joystick movement

        public UnityAction OnLeftGripPress = null; //Left grip button Pressed
        public UnityAction OnLeftGripRelease = null; //Left grip button Released
        public UnityAction<float> LeftGripMonitor = null; //Monitor force on left grip button

        public UnityAction OnLeftTriggerPress = null; //Left Trigger Button Pressed
        public UnityAction OnLeftTriggerRelease = null; //Left Trigger Button Released
        public UnityAction<float> LeftTriggerMonitor = null; //Monitor force on left trigger button

        public bool ButtonATouched { get; private set; } //whether button A is currently touched or not
        public bool ButtonBTouched { get; private set; } //whether button B is currently touched or not
        public bool ButtonXTouched { get; private set; } //whether button X is currently touched or not
        public bool ButtonYTouched { get; private set; } //whether button Y is currently touched or not
        public bool LeftJoystickTouched { get; private set; } //whether left joystick is touched or not
        public bool RightJoystickTouched { get; private set; } //whether right joystick is touched or not

        [Header("Left Hand Force")]

        [Tooltip("The minimum pressure needed to execute the OnLeftGripPress callback. A float between 0.0 and 1.0")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float LeftGripPress = 0.55f; //determines the force needed on the left grip button to execute a call to the OnLeftGripPress function 

        [Tooltip("The maximum pressure needed to execute the OnLeftGripRelease callback. A float between 0.0 and 1.0")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float LeftGripRelease = 0.35f; //determines the force needed on the left grip button to execute a call to the OnLeftGripRelease function

        [Tooltip("The minimum pressure needed to execute the OnLeftTriggerPress callback. A float between 0.0 and 1.0")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float LeftTriggerPress = 0.55f; //determines the force needed on the left trigger button to execute a call to the OnLeftTriggerPress function

        [Tooltip("The maximum pressure needed to execute the OnLeftTriggerRelease callback. A float between 0.0 and 1.0")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float LeftTriggerRelease = 0.35f; //determines the force needed on the left trigger button to execute a call to the OnLeftTriggerRelease function

        [Header("Right Hand Force")]

        [Tooltip("The minimum pressure needed to execute the OnRightGripPress callback. A float between 0.0 and 1.0")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float RightGripPress = 0.55f; //determines the force needed on the right grip button to execute a call to the OnRightGripPress function

        [Tooltip("The maximum pressure needed to execute the OnRightGripRelease callback. A float between 0.0 and 1.0")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float RightGripRelease = 0.35f; //determines the force needed on the right grip button to execute a call to the OnRightGripRelease function

        [Tooltip("The minimum pressure needed to execute the OnRightTriggerPress callback. A float between 0.0 and 1.0")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float RightTriggerPress = 0.55f; //determines the force needed on the right trigger button to execute a call to the OnRightTriggerPress function

        [Tooltip("The maximum pressure needed to execute the OnRightTriggerRelease callback. A float between 0.0 and 1.0")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float RightTriggerRelease = 0.35f; //determines the force needed on the right trigger button to execute a call to the OnRightTriggerPress function

        private bool _hasControllers = false;
        private bool _inputActive = true;
        private bool _leftGripPressed = false;
        private bool _leftTriggerPressed = false;
        private bool _rightGripPressed = false;
        private bool _rightTriggerPressed = false;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);

            } else
            {
                Instance = this;
                OVRManager.HMDMounted += PlayerFound;
                OVRManager.HMDUnmounted += PlayerLost;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!_inputActive) return;
            _hasControllers = CheckForControllers(_hasControllers);

            CheckButtonsPress();
            CheckButtonsRelease();
            CheckButtonsTouch();
            CheckJoysticksPress();
            CheckJoysticksRelease();
            CheckJoysticksTouch();
            CheckJoysticksMovement();
            CheckGripsPress();
            CheckGripsRelease();
            CheckTriggersPress();
            CheckTriggersRelease();

        }

        private void OnDestroy()
        {
            OVRManager.HMDMounted -= PlayerFound;
            OVRManager.HMDUnmounted -= PlayerLost;
        }


        //---------FUNCTIONS TO CHECK THE STATE OF BUTTONS, TRIGGER BUTTONS, GRIP BUTTONS, AND JOYSTICKS OF OCULUS TOUCH CONTROLLERS-----------


        //Function to check for all button presses excluding grip and trigger buttons
        private void CheckButtonsPress()
        {
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.Touch))
            {
                if (OnButtonAPress != null)
                {
                    OnButtonAPress();
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.Touch))
            {
                if (OnButtonBPress != null)
                {
                    OnButtonBPress();
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.Three, OVRInput.Controller.Touch))
            {
                if (OnButtonXPress != null)
                {
                    OnButtonXPress();
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.Four, OVRInput.Controller.Touch))
            {
                if (OnButtonYPress != null)
                {
                    OnButtonYPress();
                }
            }

        }

        //Function to check for all button releases excluding grip and trigger buttons
        private void CheckButtonsRelease()
        {
            if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.Touch))
            {
                if (OnButtonARelease != null)
                {
                    OnButtonARelease();
                }
            }



            if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.Touch))
            {
                if (OnButtonBRelease != null)
                {
                    OnButtonBRelease();
                }
            }



            if (OVRInput.GetUp(OVRInput.Button.Three, OVRInput.Controller.Touch))
            {
                if (OnButtonXRelease != null)
                {
                    OnButtonXRelease();
                }
            }



            if (OVRInput.GetUp(OVRInput.Button.Four, OVRInput.Controller.Touch))
            {
                if (OnButtonYRelease != null)
                {
                    OnButtonYRelease();
                }
            }
        }

        //Check if there is a touch or untouch sensed by the capacitive touch buttons
        private void CheckButtonsTouch()
        {

            if (OVRInput.Get(OVRInput.Touch.One, OVRInput.Controller.Touch) != ButtonATouched)
            {
                ButtonATouched = !ButtonATouched;
                OnButtonATouch(ButtonATouched);
            }


            if (OVRInput.Get(OVRInput.Touch.Two, OVRInput.Controller.Touch) != ButtonBTouched)
            {
                ButtonBTouched = !ButtonBTouched;
                OnButtonBTouch(ButtonBTouched);
            }

            if (OVRInput.Get(OVRInput.Touch.Three, OVRInput.Controller.Touch) != ButtonXTouched)
            {
                ButtonXTouched = !ButtonXTouched;
                OnButtonXTouch(ButtonXTouched);
            }

            if (OVRInput.Get(OVRInput.Touch.Four, OVRInput.Controller.Touch) != ButtonYTouched)
            {
                ButtonYTouched = !ButtonYTouched;
                OnButtonYTouch(ButtonYTouched);
            }

        }

        //Check if joysticks are being pressed down on
        private void CheckJoysticksPress()
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.Touch))
            {
                if (OnLeftJoystickPress != null)
                {
                    OnLeftJoystickPress();
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick, OVRInput.Controller.Touch))
            {
                if (OnRightJoystickPress != null)
                {
                    OnRightJoystickPress();
                }
            }
        }

        //Check if joysticks have been released
        private void CheckJoysticksRelease()
        {
            if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.Touch))
            {
                if (OnLeftJoystickRelease != null)
                {
                    OnLeftJoystickRelease();
                }
            }

            if (OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick, OVRInput.Controller.Touch))
            {
                if (OnRightJoystickRelease != null)
                {
                    OnRightJoystickRelease();
                }
            }
        }

        private void CheckJoysticksMovement()
        {
            CheckLeftJoystickMovement();
            CheckRightJoystickMovement();
        }

        //Check movement of left joystick
        private void CheckLeftJoystickMovement()
        {

            if (OnLeftJoystickMovement != null)  //true if left joystick has been moved upwards more than halfway
            {
                if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
                {
                    OnLeftJoystickMovement(JoystickMovement.UP);
                }

                if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown)) //true if left joystick has been moved downwards more than halfway
                {
                    OnLeftJoystickMovement(JoystickMovement.DOWN);
                }

                if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft)) //true if left joystick has been moved left more than halfway
                {
                    OnLeftJoystickMovement(JoystickMovement.LEFT);
                }

                if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight)) //true if left joystick has been moved right more than halfway
                {
                    OnLeftJoystickMovement(JoystickMovement.RIGHT);
                }

            }

            //access the joystick's current state as a Vector2
            if (LeftJoystickMonitor != null) LeftJoystickMonitor(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick));
        }

        //Check movement of right joystick
        private void CheckRightJoystickMovement()
        {

            if (OnRightJoystickMovement != null)
            {
                if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp)) //true if right joystick has been moved upwards more than halfway
                {
                    OnRightJoystickMovement(JoystickMovement.UP);

                }

                if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown)) //true if right joystick has been moved downwards more than halfway
                {
                    OnRightJoystickMovement(JoystickMovement.DOWN);
                }

                if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft)) //true if right joystick has been moved left more than halfway
                {
                    OnRightJoystickMovement(JoystickMovement.LEFT);
                }

                if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight)) //true if right joystick has been moved right more than halfway
                {
                    OnRightJoystickMovement(JoystickMovement.RIGHT);

                }
            }

            //access the joystick's current state as a Vector2
            if (RightJoystickMonitor != null) RightJoystickMonitor(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick));
        }

        //Check if a touch/untouch has been sensed by the joysticks
        private void CheckJoysticksTouch()
        {

            if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, OVRInput.Controller.Touch) != LeftJoystickTouched)
            {
                LeftJoystickTouched = !LeftJoystickTouched;
                OnLeftJoystickTouch(LeftJoystickTouched);
            }

            if (OVRInput.Get(OVRInput.Touch.SecondaryThumbstick, OVRInput.Controller.Touch) != RightJoystickTouched)
            {
                RightJoystickTouched = !RightJoystickTouched;
                OnRightJoystickTouch(RightJoystickTouched);
            }

        }

        //Check if the grip buttons have been pressed with a force greater than or equal to the respective properties set in the inspector
        private void CheckGripsPress()
        {
            float _leftGripForce = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
            float _rightGripForce = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);

            if (_leftGripForce >= LeftGripPress && !_leftGripPressed)
            {
                if (OnLeftGripPress != null)
                {
                    OnLeftGripPress();
                }

                _leftGripPressed = true;
            }

            if (_rightGripForce >= RightGripPress && !_rightGripPressed)
            {
                if (OnRightGripPress != null)
                {
                    OnRightGripPress();
                }

                _rightGripPressed = true;
            }

            //access force currently being applied to grip buttons
            if (LeftGripMonitor != null) LeftGripMonitor(_leftGripForce);
            if (RightGripMonitor != null) RightGripMonitor(_rightGripForce);

        }

        //Check if grip buttons have been released
        private void CheckGripsRelease()
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) <= LeftGripRelease && _leftGripPressed)
            {
                if (OnLeftGripRelease != null)
                {
                    OnLeftGripRelease();
                }

                _leftGripPressed = false;
            }

            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) <= RightGripRelease && _rightGripPressed)
            {
                if (OnRightGripRelease != null)
                {
                    OnRightGripRelease();
                }

                _rightGripPressed = false;
            }
        }

        //Check if the trigger buttons have been pressed with a force greater than or equal to the respective properties set in the inspector
        private void CheckTriggersPress()
        {
            float _leftTriggerForce = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            float _rightTriggerForce = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

            if (_leftTriggerForce >= LeftTriggerPress && !_leftTriggerPressed)
            {
                if (OnLeftTriggerPress != null)
                {
                    OnLeftTriggerPress();
                }

                _leftTriggerPressed = true;
            }

            if (_rightTriggerForce >= RightTriggerPress && !_rightTriggerPressed)
            {
                if (OnRightTriggerPress != null)
                {
                    OnRightTriggerPress();
                }

                _rightTriggerPressed = true;
            }

            //access force currently being applied to trigger buttons
            if (LeftTriggerMonitor != null) LeftTriggerMonitor(_leftTriggerForce);
            if (RightTriggerMonitor != null) RightTriggerMonitor(_rightTriggerForce);


        }

        //Check if trigger buttons have been released
        private void CheckTriggersRelease()
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) <= LeftTriggerRelease && _leftTriggerPressed)
            {
                if (OnLeftTriggerRelease != null)
                {
                    OnLeftTriggerRelease();
                }

                _leftTriggerPressed = false;
            }

            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) <= RightTriggerRelease && _rightTriggerPressed)
            {
                if (OnRightTriggerRelease != null)
                {
                    OnRightTriggerRelease();
                }

                _rightTriggerPressed = false;
            }
        }

        //Check if controllers are connected
        private bool CheckForControllers(bool currentValue)
        {
            bool controllersCheck = OVRInput.IsControllerConnected(OVRInput.Controller.LTouch) &&
                                    OVRInput.IsControllerConnected(OVRInput.Controller.RTouch);

            if (currentValue == controllersCheck)
            {
                return currentValue;
            }

            if (onHasController != null)
            {
                onHasController(controllersCheck);
            }

            return controllersCheck;

        }
        
        private void PlayerFound()
        {
            _inputActive = true;
        }

        private void PlayerLost()
        {
            _inputActive = false;
        }
    }
}


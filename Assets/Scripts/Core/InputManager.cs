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

    public enum Button
    {
        BUTTON_A,
        BUTTON_B,
        BUTTON_X,
        BUTTON_Y,
        RIGHT_TRIGGER,
        LEFT_TRIGGER,
        RIGHT_GRIP,
        LEFT_GRIP,
        RIGHT_JOYSTICK,
        LEFT_JOYSTICK
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

        //------------------------Right Controller Events--------------------------------------------------------//
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
        public UnityAction<Vector2> OnRightJoystickMonitor = null; //See how far the joystick has been moved; (Vector2 X/Y range of -1.0f to 1.0f)

        public UnityAction OnRightGripPress = null; //Right Grip Button Pressed
        public UnityAction OnRightGripRelease = null; //Right Grip Button Released
        public UnityAction<bool> OnRightGripTouch = null; //Right grip touch state has changed
        public UnityAction<float> RightGripMonitor = null; //Monitor force on right grip button

        public UnityAction OnRightTriggerPress = null; //Right Trigger Button Pressed
        public UnityAction OnRightTriggerRelease = null; //Right Trigger Button Released
        public UnityAction<bool> OnRightTriggerTouch = null; //Right trigger touch state has changed
        public UnityAction<float> RightTriggerMonitor = null; //Monitor force on right trigger button


        //------------------------Left Controller Events--------------------------------------------------------//
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
        public UnityAction<Vector2> OnLeftJoystickMonitor = null; //See how far the joystick has been moved; (Vector2 X/Y range of -1.0f to 1.0f)

        public UnityAction OnLeftGripPress = null; //Left grip button Pressed
        public UnityAction OnLeftGripRelease = null; //Left grip button Released
        public UnityAction<bool> OnLeftGripTouch = null; //Left grip touch state has changed
        public UnityAction<float> LeftGripMonitor = null; //Monitor force on left grip button

        public UnityAction OnLeftTriggerPress = null; //Left Trigger Button Pressed
        public UnityAction OnLeftTriggerRelease = null; //Left Trigger Button Released
        public UnityAction<bool> OnLeftTriggerTouch = null; //Left trigger touch state has changed
        public UnityAction<float> LeftTriggerMonitor = null; //Monitor force on left trigger button

        //------------------------Touch Booleans--------------------------------------------------------//
        public bool ButtonATouched { get; private set; } //whether button A is currently touched or not
        public bool ButtonBTouched { get; private set; } //whether button B is currently touched or not
        public bool ButtonXTouched { get; private set; } //whether button X is currently touched or not
        public bool ButtonYTouched { get; private set; } //whether button Y is currently touched or not
        public bool LeftJoystickTouched { get; private set; } //whether left joystick is touched or not
        public bool RightJoystickTouched { get; private set; } //whether right joystick is touched or not
        public bool LeftTriggerTouched { get; private set; } //whether left trigger is touched or not
        public bool RightTriggerTouched { get; private set; } //whether right trigger is touched or not
        public bool LeftGripTouched { get; private set; } //whether left grip is touched or not
        public bool RightGripTouched { get; private set; } //whether right grip is touched or not

        //------------------------Press Booleans--------------------------------------------------------//
        public bool ButtonAPressed { get; private set; } //whether button A is currently pressed or not
        public bool ButtonBPressed { get; private set; } //whether button B is currently pressed or not
        public bool ButtonXPressed { get; private set; } //whether button X is currently pressed or not
        public bool ButtonYPressed { get; private set; } //whether button Y is currently pressed or not
        public bool LeftJoystickPressed { get; private set; } //whether left joystick is pressed or not
        public bool RightJoystickPressed { get; private set; } //whether right joystick is pressed or not
        public bool LeftTriggerPressed { get; private set; } //whether left trigger is pressed or not
        public bool RightTriggerPressed { get; private set; } //whether right trigger is pressed or not
        public bool LeftGripPressed { get; private set; } //whether left grip is pressed or not
        public bool RightGripPressed { get; private set; } //whether right grip is pressed or not

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
        private float _gripTouchRegisterMinForce = 0.0000000001f; // Grip buttons are not touch capacitive, therefore the grip touch callbacks will be called
                                                           // if the user presses the grip buttons with a force >= _gripTouchRegisterMinForce

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

        //returns whether the button is currently pressed or not
        //the script calling this must call this function in its Update function, whether directly or by passing the function call up
        public bool IsButtonPressed(Button b)
        {
            switch (b)
            {
                case Button.BUTTON_A:
                    return OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Touch); 
                case Button.BUTTON_B:
                    return OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.Touch);
                case Button.BUTTON_X:
                    return OVRInput.Get(OVRInput.Button.Three, OVRInput.Controller.Touch);
                case Button.BUTTON_Y:
                    return OVRInput.Get(OVRInput.Button.Four, OVRInput.Controller.Touch);
                case Button.RIGHT_TRIGGER:
                    return OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Touch); //returns true if pressed more than halfway
                case Button.LEFT_TRIGGER:
                    return OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.Touch); //returns true if pressed more than halfway
                case Button.RIGHT_GRIP:
                    return OVRInput.Get(OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Touch); //returns true if pressed more than halfway
                case Button.LEFT_GRIP:
                    return OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.Touch); //returns true if pressed more than halfway
                case Button.RIGHT_JOYSTICK:
                    return OVRInput.Get(OVRInput.Button.SecondaryThumbstick, OVRInput.Controller.Touch);
                case Button.LEFT_JOYSTICK:
                    return OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.Touch);
                default:
                    return false;

            }
        }

        //returns whether the button is currently touched or not
        //the script calling this must call this function in its Update function, whether directly or by passing the function call up
        //grip buttons are not touch capacitive, so the user must be pressing the grip buttons with a force >= _gripTouchRegisterMinForce to return true
        public bool IsButtonTouched(Button b)
        {
            switch (b)
            {
                case Button.BUTTON_A:
                    return OVRInput.Get(OVRInput.Touch.One, OVRInput.Controller.Touch);
                case Button.BUTTON_B:
                    return OVRInput.Get(OVRInput.Touch.Two, OVRInput.Controller.Touch);
                case Button.BUTTON_X:
                    return OVRInput.Get(OVRInput.Touch.Three, OVRInput.Controller.Touch);
                case Button.BUTTON_Y:
                    return OVRInput.Get(OVRInput.Touch.Four, OVRInput.Controller.Touch);
                case Button.RIGHT_TRIGGER:
                    return OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger, OVRInput.Controller.Touch);
                case Button.LEFT_TRIGGER:
                    return OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.Touch);
                case Button.RIGHT_GRIP:
                    return OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch) >= _gripTouchRegisterMinForce;
                case Button.LEFT_GRIP:
                    return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch) >= _gripTouchRegisterMinForce;
                case Button.RIGHT_JOYSTICK:
                    return OVRInput.Get(OVRInput.Touch.SecondaryThumbstick, OVRInput.Controller.Touch);
                case Button.LEFT_JOYSTICK:
                    return OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, OVRInput.Controller.Touch);
                default:
                    return false;

            }
        }

        //----INTERNAL FUNCTIONS TO CHECK THE STATE OF BUTTONS, TRIGGERS, GRIPS, AND JOYSTICKS OF CONTROLLERS AND CALL CORRESPONDING CALLBACKS----//


        //Function to check for all button presses excluding grip and trigger buttons
        private void CheckButtonsPress()
        {
            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.Touch))
            {
                if (OnButtonAPress != null)
                {
                    OnButtonAPress();
                }

                ButtonAPressed = true;
            }

            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.Touch))
            {
                if (OnButtonBPress != null)
                {
                    OnButtonBPress();
                }

                ButtonBPressed = true;
            }

            if (OVRInput.GetDown(OVRInput.Button.Three, OVRInput.Controller.Touch))
            {
                if (OnButtonXPress != null)
                {
                    OnButtonXPress();
                }

                ButtonXPressed = true;
            }

            if (OVRInput.GetDown(OVRInput.Button.Four, OVRInput.Controller.Touch))
            {
                if (OnButtonYPress != null)
                {
                    OnButtonYPress();
                }

                ButtonYPressed = true;
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

                ButtonAPressed = false;
            }



            if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.Touch))
            {
                if (OnButtonBRelease != null)
                {
                    OnButtonBRelease();
                }

                ButtonBPressed = false;
            }



            if (OVRInput.GetUp(OVRInput.Button.Three, OVRInput.Controller.Touch))
            {
                if (OnButtonXRelease != null)
                {
                    OnButtonXRelease();
                }

                ButtonXPressed = false;
            }



            if (OVRInput.GetUp(OVRInput.Button.Four, OVRInput.Controller.Touch))
            {
                if (OnButtonYRelease != null)
                {
                    OnButtonYRelease();
                }

                ButtonYPressed = false;
            }
        }

        //Check if there is a touch or untouch sensed by the capacitive touch buttons
        private void CheckButtonsTouch()
        {

            if (OVRInput.Get(OVRInput.Touch.One, OVRInput.Controller.Touch) != ButtonATouched)
            {
                ButtonATouched = !ButtonATouched;
                if (OnButtonATouch != null)
                {
                    OnButtonATouch(ButtonATouched);
                }
            }


            if (OVRInput.Get(OVRInput.Touch.Two, OVRInput.Controller.Touch) != ButtonBTouched)
            {
                ButtonBTouched = !ButtonBTouched;
                if (OnButtonBTouch != null)
                {
                    OnButtonBTouch(ButtonBTouched);
                } 
            }

            if (OVRInput.Get(OVRInput.Touch.Three, OVRInput.Controller.Touch) != ButtonXTouched)
            {
                ButtonXTouched = !ButtonXTouched;
                if (OnButtonXTouch != null)
                {
                    OnButtonXTouch(ButtonXTouched);
                }
                
            }

            if (OVRInput.Get(OVRInput.Touch.Four, OVRInput.Controller.Touch) != ButtonYTouched)
            {
                ButtonYTouched = !ButtonYTouched;
                if (OnButtonYTouch != null)
                {
                    OnButtonYTouch(ButtonYTouched);
                }
                
            }

            //Check Triggers' touch state
            if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.Touch) != LeftTriggerTouched)
            {
                LeftTriggerTouched = !LeftTriggerTouched;
                if (OnLeftTriggerTouch != null)
                {
                    OnLeftTriggerTouch(LeftTriggerTouched);
                }
                
            }

            if (OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger, OVRInput.Controller.Touch) != RightTriggerTouched)
            {
                RightTriggerTouched = !RightTriggerTouched;
                if (OnRightTriggerTouch != null)
                {
                    OnRightTriggerTouch(RightTriggerTouched);
                }
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

                LeftJoystickPressed = true;
            }

            if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick, OVRInput.Controller.Touch))
            {
                if (OnRightJoystickPress != null)
                {
                    OnRightJoystickPress();
                }

                RightJoystickPressed = true;
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

                LeftJoystickPressed = false;
            }

            if (OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick, OVRInput.Controller.Touch))
            {
                if (OnRightJoystickRelease != null)
                {
                    OnRightJoystickRelease();
                }

                RightJoystickPressed = false;
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

            if (OnLeftJoystickMovement != null)  
            {
                if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp)) //true if left joystick has been moved upwards more than halfway
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
            if (OnLeftJoystickMonitor != null) OnLeftJoystickMonitor(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick));
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
            if (OnRightJoystickMonitor != null) OnRightJoystickMonitor(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick));
        }

        //Check if a touch/untouch has been sensed by the joysticks
        private void CheckJoysticksTouch()
        {

            if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, OVRInput.Controller.Touch) != LeftJoystickTouched)
            {
                LeftJoystickTouched = !LeftJoystickTouched;
                if (OnLeftJoystickTouch != null)
                {
                    OnLeftJoystickTouch(LeftJoystickTouched);
                }
                
            }

            if (OVRInput.Get(OVRInput.Touch.SecondaryThumbstick, OVRInput.Controller.Touch) != RightJoystickTouched)
            {
                RightJoystickTouched = !RightJoystickTouched;
                if (OnRightJoystickTouch != null)
                {
                    OnRightJoystickTouch(RightJoystickTouched);
                }
                
            }

        }

        //Checks if grip buttons have been pressed
        //Because grip buttons are not touch capacitive, the user must be pressing on the buttons with a force >= 0.01f for this function to be executed
        private void CheckGripsTouch(float _leftGripForce, float _rightGripForce)
        {
            bool _leftGripTouch = _leftGripForce >= _gripTouchRegisterMinForce ? true : false;
            bool _rightGripTouch = _rightGripForce >= _gripTouchRegisterMinForce ? true : false;

            if (_leftGripTouch != LeftGripTouched)
            {
                LeftGripTouched = !LeftGripTouched;
                if (OnLeftGripTouch != null)
                {
                    OnLeftGripTouch(LeftGripTouched);
                }
            }

            if (_rightGripTouch != RightGripTouched)
            {
                RightGripTouched = !RightGripTouched;
                if (OnRightGripTouch != null)
                {
                    OnRightGripTouch(RightGripTouched);
                }

            }

        }

        //Check if the grip buttons have been pressed with a force greater than or equal to the respective properties set in the inspector
        private void CheckGripsPress()
        {
            float _leftGripForce = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch);
            float _rightGripForce = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch);

            //Check for grips "touched"
            CheckGripsTouch(_leftGripForce, _rightGripForce);

            //Check for grips pressed
            if (_leftGripForce >= LeftGripPress && !LeftGripPressed)
            {
                if (OnLeftGripPress != null)
                {
                    OnLeftGripPress();
                }

                LeftGripPressed = true;
            }

            if (_rightGripForce >= RightGripPress && !RightGripPressed)
            {
                if (OnRightGripPress != null)
                {
                    OnRightGripPress();
                }

                RightGripPressed = true;
            }

            //access force currently being applied to grip buttons
            if (LeftGripMonitor != null) LeftGripMonitor(_leftGripForce);
            if (RightGripMonitor != null) RightGripMonitor(_rightGripForce);

        }

        //Check if grip buttons have been released
        private void CheckGripsRelease()
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) <= LeftGripRelease && LeftGripPressed)
            {
                if (OnLeftGripRelease != null)
                {
                    OnLeftGripRelease();
                }

                LeftGripPressed = false;
            }

            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) <= RightGripRelease && RightGripPressed)
            {
                if (OnRightGripRelease != null)
                {
                    OnRightGripRelease();
                }

                RightGripPressed = false;
            }
        }

        //Check if the trigger buttons have been pressed with a force greater than or equal to the respective properties set in the inspector
        private void CheckTriggersPress()
        {
            float _leftTriggerForce = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            float _rightTriggerForce = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

            if (_leftTriggerForce >= LeftTriggerPress && !LeftTriggerPressed)
            {
                if (OnLeftTriggerPress != null)
                {
                    OnLeftTriggerPress();
                }

                LeftTriggerPressed = true;
            }

            if (_rightTriggerForce >= RightTriggerPress && !RightTriggerPressed)
            {
                if (OnRightTriggerPress != null)
                {
                    OnRightTriggerPress();
                }

                RightTriggerPressed = true;
            }

            //access force currently being applied to trigger buttons
            if (LeftTriggerMonitor != null) LeftTriggerMonitor(_leftTriggerForce);
            if (RightTriggerMonitor != null) RightTriggerMonitor(_rightTriggerForce);


        }

        //Check if trigger buttons have been released
        private void CheckTriggersRelease()
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) <= LeftTriggerRelease && LeftTriggerPressed)
            {
                if (OnLeftTriggerRelease != null)
                {
                    OnLeftTriggerRelease();
                }

                LeftTriggerPressed = false;
            }

            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) <= RightTriggerRelease && RightTriggerPressed)
            {
                if (OnRightTriggerRelease != null)
                {
                    OnRightTriggerRelease();
                }

                RightTriggerPressed = false;
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


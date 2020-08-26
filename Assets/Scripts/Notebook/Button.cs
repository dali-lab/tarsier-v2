using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Anivision.Core;
using Anivision.PlayerInteraction;
using TMPro;

namespace Anivision.NotebookSystem
{
    /// <summary>
    /// Controls all the physical pressable buttons in the scene.
    /// Haptics on button interaction.
    /// Changes the button color, the controller color, and selector sphere color on button hover
    /// </summary>
    public class Button : MonoBehaviour
    {
        [TextArea(3, 10)] public string buttonText;
        public GameObject rightController;

        [Tooltip("How many seconds to wait before the button can register another press.")]
        public float buttonCooldownSeconds = 0.5f;

        public Color defaultButtonColor;
        public Color hoverButtonColor;

        [Tooltip("The haptic frequency when the selector sphere enters the button.")]
        public float hoverHapticFrequency = 1;
        [Tooltip("The haptic strength when the selector sphere enters the button.")]
        public float hoverHapticAmplitude = 0.25f;
        [Tooltip("The haptic duration when the selector sphere enters the button.")]
        public float hoverHapticDuration = 0.1f;

        [Tooltip("The haptic frequency when the button is pressed")]
        public float selectionHapticFrequency = 1;
        [Tooltip("The haptic strength when the button is pressed")]
        public float selectionHapticAmplitude = 0.25f;
        [Tooltip("The haptic duration when the button is pressed")]
        public float selectonHapticDuration = 0.1f;

        [HideInInspector] public UnityEvent onClick;
        [HideInInspector] public UnityEvent onButtonEnter;

        private InputManager _inputManager;
        private HapticsController _hapticsController;
        private TeleportController _teleportController;
        private ColorController _rightColorController;
        private TextMeshPro _TMP;
        private MaterialPropertyBlock _propBlock;
        private Renderer _renderer;
        private bool _turnOnTeleport = false;
        private bool buttonCooldownRunning;


        private void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            _renderer = gameObject.GetComponent<Renderer>();
            _rightColorController = rightController.GetComponent<ColorController>();
        }

        private void OnEnable()
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");

            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            _TMP = gameObject.GetComponent<TextMeshPro>();
            ChangeText(buttonText);
            ChangeButtonColor(defaultButtonColor);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "selector")
            {
                if (_teleportController.enabled)                // to prevent accidental teleport when trying to select a button
                {
                    _turnOnTeleport = true;                     // keeps track of whether the teleporterController is suppose to be on to set its state back when OnTriggerExit is called
                    _teleportController.enabled = false;        // turn off ability to teleport
                }

                // trigger button hover haptics
                _hapticsController.Haptics(hoverHapticFrequency, hoverHapticAmplitude, hoverHapticDuration, OVRInput.Controller.RTouch);

                // update hover colors of the button, the right controller, and the selector sphere on the right controller
                ChangeButtonColor(hoverButtonColor);
                _rightColorController.ToHoverControllerColor();
                _rightColorController.ToHoverSelectorColor();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "selector" && _inputManager.IsButtonPressed(InputManager.Button.RIGHT_TRIGGER))
            {
                // trigger button select haptics
                _hapticsController.Haptics(selectionHapticFrequency, selectionHapticAmplitude, selectonHapticDuration, OVRInput.Controller.RTouch);

                if (!buttonCooldownRunning)
                {
                    StartCoroutine(ButtonCooldown(buttonCooldownSeconds));
                }
            
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (_turnOnTeleport) _teleportController.enabled = true;            // turn on ability to teleport if player had teleport before interacting with button

            // update colors of the button, the right controller, and the selector sphere on the right controller back to the default
            ChangeButtonColor(defaultButtonColor);
            _rightColorController.ToDefaultControllerColor();
            _rightColorController.ToDefaultSelectorColor();
        }

        IEnumerator ButtonCooldown(float seconds)
        {
            buttonCooldownRunning = true;
            yield return new WaitForSecondsRealtime(seconds);
            onClick.Invoke();

            if (_turnOnTeleport) _teleportController.enabled = true;            // turn on ability to teleport if player had teleport before interacting with button

            // reset the controller and selector color to default if the button is pressed
            _rightColorController.ToDefaultControllerColor();
            _rightColorController.ToDefaultSelectorColor();
            buttonCooldownRunning = false;
        }


        public void ChangeText (string s)
        {
            _TMP.text = s;
        }

        public void ChangeButtonColor (Color c)
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_BaseColor", c);
            _renderer.SetPropertyBlock(_propBlock);
        }

        private void OnDestroy()
        {
            onClick.RemoveAllListeners();
            onButtonEnter.RemoveAllListeners();
        }
    }
}
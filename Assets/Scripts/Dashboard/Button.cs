using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Anivision.Core;
using Anivision.PlayerInteraction;
using TMPro;

public class Button : MonoBehaviour
{
    [TextArea(3, 10)] public string buttonText;
    public GameObject rightController;

    [Tooltip("How many seconds to wait before the button can register another press.")]
    public float buttonCooldownSeconds = 0.5f;

    public Color defaultButtonColor;
    public Color hoverButtonColor;

    [HideInInspector] public UnityEvent onClick;
    [HideInInspector] public UnityEvent onButtonEnter;

    private InputManager _inputManager;
    private HapticsController _hapticsController;
    private TeleportController _teleportController;
    private TextMeshPro _TMP;
    private MaterialPropertyBlock _propBlock;
    private Renderer _renderer;


    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = gameObject.GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _inputManager = InputManager.Instance;
        if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");

        _hapticsController = HapticsController.Instance;
        if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

        _teleportController = TeleportController.Instance;
        if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

        _TMP = gameObject.transform.Find("TMP").gameObject.GetComponent<TextMeshPro>();
        
        ChangeText(buttonText);
        ChangeButtonColor(defaultButtonColor);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "selector")
        {
            if (_teleportController != null) _teleportController.enabled = false;
            _hapticsController.Haptics(1, 0.25f, 0.1f, OVRInput.Controller.RTouch);

            // update hover colors of the button, the right controller, and the selector sphere on the right controller
            ChangeButtonColor(hoverButtonColor);
            rightController.GetComponent<ColorController>().ToHoverControllerColor();
            rightController.GetComponent<ColorController>().ToHoverSelectorColor();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "selector" && _inputManager.IsButtonPressed(InputManager.Button.RIGHT_TRIGGER))
        {
            _hapticsController.Haptics(1, 0.25f, 0.1f, OVRInput.Controller.RTouch);

            // reset the controller and selector color to default if the button is pressed
            rightController.GetComponent<ColorController>().ToDefaultControllerColor();
            rightController.GetComponent<ColorController>().ToDefaultSelectorColor();

            StartCoroutine(ButtonCooldown(buttonCooldownSeconds));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_teleportController != null) _teleportController.enabled = true;

        // update colors of the button, the right controller, and the selector sphere on the right controller back to the default
        ChangeButtonColor(defaultButtonColor);
        rightController.GetComponent<ColorController>().ToDefaultControllerColor();
        rightController.GetComponent<ColorController>().ToDefaultSelectorColor();
    }

    IEnumerator ButtonCooldown(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        onClick.Invoke();
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

    private void OnDisable()
    {
        onClick.RemoveAllListeners();
        onButtonEnter.RemoveAllListeners();
    }
}

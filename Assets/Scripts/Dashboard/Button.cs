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
    

    public float buttonCooldownSeconds = 0.5f;

    public Color defaultButtonColor;
    public Color hoverButtonColor;

    [HideInInspector] public UnityEvent onClick;
    [HideInInspector] public UnityEvent onButtonEnter;

    private InputManager _inputManager;
    private HapticsController _hapticsController;
    private TeleportController _teleportController;
    private TextMeshPro _TMP;

    private void OnEnable()
    {
        _inputManager = InputManager.Instance;
        if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");

        _hapticsController = HapticsController.Instance;
        if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

        _teleportController = TeleportController.Instance;

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
            StartCoroutine(ButtonCooldown(buttonCooldownSeconds));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_teleportController != null) _teleportController.enabled = true;
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
        gameObject.GetComponent<Renderer>().material.color = c;
    }
}

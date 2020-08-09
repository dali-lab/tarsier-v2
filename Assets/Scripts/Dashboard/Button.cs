﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Anivision.Core;

public class Button : MonoBehaviour
{
    public Color defaultColor;
    public Color hoverColor;
    [HideInInspector] public UnityEvent onClick;

    private InputManager _inputManager;
    private HapticsController _hapticsController;

    private void OnEnable()
    {
        _inputManager = InputManager.Instance;
        if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");
        _hapticsController = HapticsController.Instance;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "selector")
        {
            _hapticsController.Haptics(1, 0.25f, 0.1f, OVRInput.Controller.RTouch);
            ChangeColor(hoverColor);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "selector" && _inputManager.IsButtonPressed(InputManager.Button.RIGHT_TRIGGER))
        {
            _hapticsController.Haptics(1, 0.25f, 0.1f, OVRInput.Controller.RTouch);
            StartCoroutine(ButtonCooldown(1));
        }
    }

    IEnumerator ButtonCooldown(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        onClick.Invoke();
    }
    public void OnTriggerExit(Collider other)
    {
        ChangeColor(defaultColor);
    }

    public void ChangeText (string s)
    {

    }

    public void ChangeColor (Color c)
    {
        gameObject.GetComponent<Renderer>().material.color = c;
    }

    public void ChangeOutline (Color c)
    {

    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

public class BeeLeftControls : MonoBehaviour
{
    public GameObject LController;
    public GameObject controlsButton;                                                   // button to tell players to press X to pull up the instructions/controls
    public GameObject controlsPanel;                                                    // the instruction/controls panel

    private InputManager _inputManager;

    public void OnEnable()
    {
        _inputManager = InputManager.Instance;

        if (_inputManager == null)
        {
            throw new System.Exception("Must have an input manager script in the scene");
        }

        if (_inputManager != null) _inputManager.AttachInputHandler(SetControlsPanel, InputManager.InputState.ON_PRESS, InputManager.Button.X);
        controlsButton.SetActive(true);
        controlsPanel.SetActive(false);
    }

    private void SetControlsPanel()
    {
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }

    public void OnDisable()
    {
        if (_inputManager != null) _inputManager.DetachInputHandler(SetControlsPanel, InputManager.InputState.ON_PRESS, InputManager.Button.X);
        controlsButton.SetActive(false);
        controlsPanel.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

public class BeeTutorialVision : MonoBehaviour
{
    public GameObject RController;
    public GameObject nextPanel;
    public GameObject buttonBHighlight;                                                     // indicates which button to click

    private InputManager _inputManager;

    public void OnEnable()
    {
        _inputManager = InputManager.Instance;

        if (_inputManager == null)
        {
            throw new System.Exception("Must have an input manager script in the scene");
        }

        if (_inputManager != null)
        {
            _inputManager.OnButtonBPress += DoRightButtonTwoPressed;
        }
            
        buttonBHighlight.SetActive(true);
    }

    private void DoRightButtonTwoPressed()   // turns off button highlight, moves on the next tutorial panel
    {
        buttonBHighlight.SetActive(false);
        gameObject.SetActive(false);
        nextPanel.SetActive(true);
    }

    public void OnDisable()
    {
        if (_inputManager != null) _inputManager.OnButtonBPress -= DoRightButtonTwoPressed;
    }
}

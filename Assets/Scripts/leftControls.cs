using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

public class leftControls : MonoBehaviour
{
    public GameObject exitPanel;
    private InputManager _inputManager;

    private void OnEnable()
    {
        exitPanel.SetActive(true);

        //Get singleton instance of input manager
        _inputManager = InputManager.Instance;

        if (_inputManager == null)
        {
            throw new System.Exception("Must have Input Manager script in scene");
        }

        _inputManager.OnButtonXPress += DoButtonOnePressed;

    }

    private void OnDisable()
    {
        if (_inputManager != null)
        {
            _inputManager.OnButtonXPress -= DoButtonOnePressed;
        }
    }

    private void DoButtonOnePressed()
    {
        if (!exitPanel.activeSelf)
        {
            exitPanel.SetActive(true);
        }
        else
        {
            exitPanel.SetActive(false);
        }
        
    }
}

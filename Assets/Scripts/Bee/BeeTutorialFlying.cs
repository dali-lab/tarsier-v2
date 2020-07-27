using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

public class BeeTutorialFlying : MonoBehaviour
{
    public GameObject RController;
    public GameObject beeLeftControls;                                                      // sets controls panels to false on start
    public GameObject nextPanel;
    public GameObject hapticCube;                                                           // haptic functionality for the controllers
    public GameObject buttonAHighlight;                                                     // indicates which button to click

    private bool haptics = false;
    private InputManager _inputManager;

    public void OnEnable()
    {
        _inputManager = InputManager.Instance;

        if (_inputManager == null)
        {
            throw new System.Exception("Must have an input manager script in the scene");
        }

        if (_inputManager != null) _inputManager.AttachInputHandler(StartHaptics, InputManager.InputState.ON_PRESS, InputManager.Button.A);

        beeLeftControls.SetActive(false);
        buttonAHighlight.SetActive(true);
    }

    public void FixedUpdate()
    {
        if (haptics)
        {
            hapticCube.transform.position = RController.transform.position;
        }
    }

    private void StartHaptics()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;                                // turns off instructions panel
        StartCoroutine(WaitHaptics());
    }

    IEnumerator WaitHaptics()                                                                   // turns on haptics so players stop flying, moves on to next tutorial panel
    {
        yield return new WaitForSeconds(6);
        haptics = true;
        yield return new WaitForSeconds(3);
        haptics = false;

        buttonAHighlight.SetActive(false);
        nextPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Disable()
    {
        buttonAHighlight.SetActive(false);
        if (_inputManager != null) _inputManager.DetachInputHandler(StartHaptics, InputManager.InputState.ON_PRESS, InputManager.Button.A);
    }
}

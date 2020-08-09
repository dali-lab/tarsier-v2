using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.Core;

public class LobbyControllers : TutorialStep
{
    public GameObject tutorialControllers;
    public GameObject LHighlightRings;
    public GameObject RHighlightRings;

    private InputManager _inputManager;
    private HapticsController _hapticsController;

    private GameObject _currHighlight;
    private int _highlightsOn = 10;

    public override void Setup(TextMeshPro TMP)
    {
        _inputManager = InputManager.Instance;
        _hapticsController = HapticsController.Instance;

        if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");
        else
        {
            TMP.text = dashboardText;
            tutorialControllers.SetActive(true);
            LHighlightRings.SetActive(true);
            RHighlightRings.SetActive(true);
            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
        }
    }

    private void Update()
    {
        if (_highlightsOn == 0) OnDone.Invoke();

        if (_inputManager.IsButtonPressed(InputManager.Button.A)) _currHighlight = RHighlightRings.transform.Find("aRing").gameObject;
        if (_inputManager.IsButtonPressed(InputManager.Button.B)) _currHighlight = RHighlightRings.transform.Find("bRing").gameObject;
        if (_inputManager.IsButtonPressed(InputManager.Button.RIGHT_GRIP)) _currHighlight = RHighlightRings.transform.Find("gripRing").gameObject;
        if (_inputManager.IsButtonPressed(InputManager.Button.RIGHT_JOYSTICK)) _currHighlight = RHighlightRings.transform.Find("joystickRing").gameObject;
        if (_inputManager.IsButtonPressed(InputManager.Button.RIGHT_TRIGGER)) _currHighlight = RHighlightRings.transform.Find("triggerRing").gameObject;

        if (_inputManager.IsButtonPressed(InputManager.Button.X)) _currHighlight = LHighlightRings.transform.Find("xRing").gameObject;
        if (_inputManager.IsButtonPressed(InputManager.Button.Y)) _currHighlight = LHighlightRings.transform.Find("yRing").gameObject;
        if (_inputManager.IsButtonPressed(InputManager.Button.LEFT_GRIP)) _currHighlight = LHighlightRings.transform.Find("gripRing").gameObject;
        if (_inputManager.IsButtonPressed(InputManager.Button.LEFT_JOYSTICK)) _currHighlight = LHighlightRings.transform.Find("joystickRing").gameObject;
        if (_inputManager.IsButtonPressed(InputManager.Button.LEFT_TRIGGER)) _currHighlight = LHighlightRings.transform.Find("triggerRing").gameObject;

        if (_currHighlight != null)
        {
            if (_currHighlight.activeSelf == true)
            {
                _currHighlight.SetActive(false);
                _highlightsOn -= 1;
            }
        }
    }

    public override void Cleanup(TextMeshPro TMP)
    {
        TMP.text = "";
        tutorialControllers.SetActive(false);
        RHighlightRings.SetActive(false);
        LHighlightRings.SetActive(false);
    }
}

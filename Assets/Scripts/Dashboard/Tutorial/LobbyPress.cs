using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyPress : TutorialStep
{
    public GameObject welcomePanel;
    public GameObject continueButton;

    private HapticsController _hapticsController;


    public override void Setup(TextMeshPro TMP)
    {
        _hapticsController = HapticsController.Instance;

        TMP.text = dashboardText;
        welcomePanel.SetActive(true);
        continueButton.SetActive(true);
        continueButton.GetComponent<Button>().onClick.AddListener(Continue);
        _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
    }

    private void Continue()
    {
        OnDone.Invoke();
    }

    public override void Cleanup(TextMeshPro TMP)
    {
        TMP.text = "";
        welcomePanel.SetActive(false);
        continueButton.GetComponent<Button>().onClick.RemoveListener(Continue);
        continueButton.SetActive(false);
    }
}

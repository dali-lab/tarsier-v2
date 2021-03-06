﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialVision : MonoBehaviour
{
    public GameObject RController;
    public GameObject nextPanel;
    public GameObject buttonBHighlight;                                                     // indicates which button to click

    private VRTK_ControllerEvents RControllerEvents;


    public void OnEnable()
    {
        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        RControllerEvents.ButtonTwoPressed += DoRightButtonTwoPressed;                       // 'B' button on right controller
        buttonBHighlight.SetActive(true);
    }

    private void DoRightButtonTwoPressed(object sender, ControllerInteractionEventArgs e)   // turns off button highlight, moves on the next tutorial panel
    {
        buttonBHighlight.SetActive(false);
        gameObject.SetActive(false);
        nextPanel.SetActive(true);
    }

    public void OnDisable()
    {
        RControllerEvents.ButtonTwoPressed -= DoRightButtonTwoPressed;
    }
}

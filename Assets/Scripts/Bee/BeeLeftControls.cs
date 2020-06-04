using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeLeftControls : MonoBehaviour
{
    public GameObject LController;
    public GameObject controlsButton;                                                   // button to tell players to press X to pull up the instructions/controls
    public GameObject controlsPanel;                                                    // the instruction/controls panel

    private VRTK_ControllerEvents LControllerEvents;

    public void OnEnable()
    {
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents.ButtonOnePressed += DoLeftButtonOnePressed;                       // 'X' button on left controller
        controlsButton.SetActive(true);
        controlsPanel.SetActive(false);
    }

    private void DoLeftButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }

    public void OnDisable()
    {
        LControllerEvents.ButtonOnePressed += DoLeftButtonOnePressed;
        controlsButton.SetActive(false);
        controlsPanel.SetActive(false);
    }

}

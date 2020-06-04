using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialGo : MonoBehaviour
{
    public GameObject LController;
    public GameObject buttonXHighlight;                                                     // indicates which button to click
    public GameObject beeLeftControls;                                                      // turns on the instructions text so players can pull up an instruction panel

    private VRTK_ControllerEvents LControllerEvents;


    public void OnEnable()
    {
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents.ButtonOnePressed += DoLeftButtonOnePressed;                       // 'X' button on left controller
        buttonXHighlight.SetActive(true);
    }

    private void DoLeftButtonOnePressed(object sender, ControllerInteractionEventArgs e)    // turns of indicator of which button to press and the panel, turns on the instructions text
    {
        buttonXHighlight.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        beeLeftControls.SetActive(true);
    }

    public void OnDisable()
    {
        LControllerEvents.ButtonOnePressed -= DoLeftButtonOnePressed;
    }
}

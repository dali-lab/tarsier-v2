using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialGo : MonoBehaviour
{
    public GameObject LController;
    public GameObject buttonXHighlight;

    private VRTK_ControllerEvents LControllerEvents;


    public void OnEnable()
    {
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents.ButtonOnePressed += DoLeftButtonOnePressed;                       // 'X' button on left controller
        buttonXHighlight.SetActive(true);
    }

    private void DoLeftButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        buttonXHighlight.SetActive(false);
        gameObject.SetActive(false);
    }
}

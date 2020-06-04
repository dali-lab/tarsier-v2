using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialFlying : MonoBehaviour
{
    public GameObject RController;
    public GameObject beeLeftControls;                                                      // sets controls panels to false on start
    public GameObject nextPanel;
    public GameObject hapticCube;                                                           // haptic functionality for the controllers
    public GameObject buttonAHighlight;                                                     // indicates which button to click

    private bool haptics = false;
    private VRTK_ControllerEvents RControllerEvents;


    public void OnEnable()
    {
        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        RControllerEvents.ButtonOnePressed += DoRightButtonOnePressed;                       // 'A' button on right controller

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

    private void DoRightButtonOnePressed(object sender, ControllerInteractionEventArgs e)
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
        RControllerEvents.ButtonOnePressed -= DoRightButtonOnePressed;                       // 'A' button on right controller
    }
}

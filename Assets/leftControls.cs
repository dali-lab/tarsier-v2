using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class leftControls : MonoBehaviour
{
    public GameObject exitPanel;
    private VRTK_ControllerEvents controllerEvents;

    private void OnEnable()
    {
        exitPanel.SetActive(true);
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        if (controllerEvents == null)
        {
            VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
            return;
        }

        controllerEvents.ButtonOnePressed += DoButtonOnePressed;
    }

    private void OnDisable()
    {
        if (controllerEvents != null)
        {
            controllerEvents.ButtonOnePressed -= DoButtonOnePressed;
        }
    }

    private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
    {
        string debugString = "Controller on index '" + index + "' " + button + " has been " + action
                             + " with a pressure of " + e.buttonPressure + " / Primary Touchpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)" + " / Secondary Touchpad axis at: " + e.touchpadTwoAxis + " (" + e.touchpadTwoAngle + " degrees)";
        VRTK_Logger.Info(debugString);
    }

    private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
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

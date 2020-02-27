using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class tutorial : MonoBehaviour
{
    public GameObject RController;
    public GameObject LController;
    public GameObject[] glowingButtons;
    public GameObject[] textScreens;

    // Start is called before the first frame update
    void Start()
    {
        glowingButtons[0].SetActive(false);
        glowingButtons[1].SetActive(false);
        glowingButtons[2].SetActive(false);
        glowingButtons[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (textScreens[0].activeSelf)                  // on welcome screen
        {
            //[wait 5 seconds]

            // welcome message off, press buttons message on
            textScreens[0].SetActive(false);
            textScreens[1].SetActive(true);

            // turn on button glow
            glowingButtons[0].SetActive(true);
            glowingButtons[1].SetActive(true);
            glowingButtons[2].SetActive(true);
            glowingButtons[3].SetActive(true);
        }
        else if (textScreens[1].activeSelf)                  // on button screen
        {                  
            var RcontrollerEvents = RController.GetComponent<VRTK_ControllerEvents>();
            var LcontrollerEvents = LController.GetComponent<VRTK_ControllerEvents>();

            if (RcontrollerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress))
            {
                glowingButtons[0].SetActive(false); // turn off glow on R button 1
            }
            if (RcontrollerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress))
            {
                glowingButtons[1].SetActive(false);     // turn off glow on R button 2
            }
            if (LcontrollerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress))
            {
                glowingButtons[2].SetActive(false);     // turn off glow on L button 1
            }
            if (LcontrollerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress))
            {
                glowingButtons[3].SetActive(false);     // turn off glow on L button 2
            }
        }

    }
}

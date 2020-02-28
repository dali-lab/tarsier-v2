using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class tutorial : MonoBehaviour
{
    public GameObject RController;
    public GameObject LController;
    public GameObject R1;
    public GameObject R2;
    public GameObject L1;
    public GameObject L2;
    public GameObject RTrigger;
    public GameObject LTrigger;
    public GameObject RGrip;
    public GameObject LGrip;
    public GameObject table;
    public GameObject goggles;
    public GameObject[] textScreens;

    private VRTK_ControllerEvents RControllerEvents;
    private VRTK_ControllerEvents LControllerEvents;

    // Start is called before the first frame update
    void Start()
    {
        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();

        // buttons
        R1.SetActive(false);
        R2.SetActive(false);
        L1.SetActive(false);
        L2.SetActive(false);
        // triggers
        RTrigger.SetActive(false);
        LTrigger.SetActive(false);
        // grips
        RGrip.SetActive(false);
        LGrip.SetActive(false);
        // table and stuff
        table.SetActive(false);
        goggles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (textScreens[0].activeSelf)                  // on welcome screen
        {
            //[wait 5 seconds]

            // move to next screen
            textScreens[0].SetActive(false);
            textScreens[1].SetActive(true);

            // turn on button glow
            R1.SetActive(true);
            R2.SetActive(true);
            L1.SetActive(true);
            L2.SetActive(true);
        }
        else if (textScreens[1].activeSelf)                         // on button screen
        {
            if (RControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress))
            {
                R1.SetActive(false);                 // turn off glow on R button 1
            }
            if (RControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress))
            {
                R2.SetActive(false);                 // turn off glow on R button 2
            }
            if (LControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress))
            {
                L1.SetActive(false);                 // turn off glow on L button 1
            }
            if (LControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress))
            {
                L2.SetActive(false);                 // turn off glow on L button 2
            }

            // once all buttons are pressed
            if (!R1.activeSelf && !R2.activeSelf && !L1.activeSelf && !L2.activeSelf)           
            {
                // move to next screen
                textScreens[1].SetActive(false);
                textScreens[2].SetActive(true);

                // turn on trigger glow
                RTrigger.SetActive(true);
                LTrigger.SetActive(true);
            }
        }
        else if (textScreens[2].activeSelf)                         // on trigger screen
        {
            if (RControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress))
            {
                RTrigger.SetActive(false);                 // turn off glow on R trigger
            }
            if (LControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress))
            {
                LTrigger.SetActive(false);                 // turn off glow on L trigger
            }

            // once both triggers are pressed
            if (!RTrigger.activeSelf && !LTrigger.activeSelf)           
            {
                // move to next screen
                textScreens[2].SetActive(false);
                textScreens[3].SetActive(true);

                // turn on grip glow
                RGrip.SetActive(true);
                LGrip.SetActive(true);
            }
        }
        else if (textScreens[3].activeSelf)                         // on grip screen
        {
            if (RControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress))
            {
                RGrip.SetActive(false);                 // turn off glow on R grip
            }
            if (LControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress))
            {
                LGrip.SetActive(false);                 // turn off glow on L grip
            }

            // once both grips are pressed
            if (!RGrip.activeSelf && !LGrip.activeSelf)           
            {
                // move to next screen
                textScreens[3].SetActive(false);
                textScreens[4].SetActive(true);

                // turn on table and goggles
                table.SetActive(true);
                goggles.SetActive(true);

                // turn on components to grab
                RController.GetComponent<VRTK_InteractGrab>().enabled = true;
                RController.GetComponent<VRTK_InteractTouch>().enabled = true;

                
            }
        }
    }
}

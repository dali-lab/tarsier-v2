using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrabTutorial : TutorialBaseClass
{
    public GameObject RController;
    public GameObject LController;

    public GameObject[] textScreens;
    public GameObject table;
    public GameObject goggles;
    
    private VRTK_ControllerEvents RControllerEvents;
    private VRTK_ControllerEvents LControllerEvents;

    // Start is called before the first frame update
    public override void Start()
    {
        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();

        textScreens[0].SetActive(true);

        // turn on table and goggles
        table.SetActive(true);
        goggles.SetActive(true);

        // turn on components to grab
        RController.GetComponent<VRTK_InteractGrab>().enabled = true;
        RController.GetComponent<VRTK_InteractTouch>().enabled = true;

        isDone = false;

    }

    // Update is called once per frame
    public override void Update()
    {
        if (!isDone) // on grab screen
        {
            //grab goggles
            if (goggles == null)
            {
                // move to next screen
                textScreens[0].SetActive(false);

                isDone = true;

            }

        }
    
    }

    public override void Disable()
    {
        for (int i = 0; i < textScreens.Length; i++)
        {
            textScreens[i].SetActive(false);
        }

        if (goggles != null)
        {
            goggles.SetActive(false);
        }
        
        // turn off components to grab
        RController.GetComponent<VRTK_InteractGrab>().enabled = false;
        RController.GetComponent<VRTK_InteractTouch>().enabled = false;
    }
}

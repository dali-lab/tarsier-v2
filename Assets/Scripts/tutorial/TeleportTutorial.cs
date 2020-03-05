using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleportTutorial : TutorialBaseClass
{
    public GameObject RController;
    public GameObject LController;

    public GameObject[] textScreens;
    public GameObject[] platforms;
    public GameObject door;

    private VRTK_ControllerEvents RControllerEvents;
    private VRTK_ControllerEvents LControllerEvents;

    // Start is called before the first frame update
    public override void Start()
    {
        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();

        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }

        door.SetActive(true);
        RController.GetComponent<VRTK_Pointer>().enabled = true;
        RController.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;

        textScreens[0].SetActive(true);
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override bool IsDone()
    {
        return base.IsDone();
    }

    public override void Disable()
    {
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false); 
        }

        door.SetActive(false);
        RController.GetComponent<VRTK_Pointer>().enabled = false;
        RController.GetComponent<VRTK_StraightPointerRenderer>().enabled = false;
    }
}

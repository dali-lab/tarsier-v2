using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTutorial : TutorialBaseClass
{
    public GameObject RController;
    public GameObject LController;

    public GameObject[] textScreens;
    public GameObject[] platforms;
    public GameObject door;

    public Animator platformAnim;

    // Start is called before the first frame update
    public override void Start()
    {
        // TODO: Replace VRTK with own teleport system
        //RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        //LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();

        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }
        platformAnim.SetBool("up", true);

        door.SetActive(true);

        // TODO: Replace VRTK with own teleport system
        //RController.GetComponent<VRTK_Pointer>().enabled = true;
        //RController.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;

        textScreens[0].SetActive(true);
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Disable()
    {
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false); 
        }

        door.SetActive(false);

        //TODO: Replace VRTK with own teleport system
        //RController.GetComponent<VRTK_Pointer>().enabled = false;
        //RController.GetComponent<VRTK_StraightPointerRenderer>().enabled = false;
    }
}

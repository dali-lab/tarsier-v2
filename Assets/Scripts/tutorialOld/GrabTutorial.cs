using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTutorial : TutorialBaseClass
{
    public GameObject RController;
    public GameObject LController;
    public GameObject buttons;

    public GameObject[] textScreens;
    public GameObject table;
    public GameObject goggles;

    public Animator tableAnim;
    public Animator gogglesAnim;

    // Start is called before the first frame update
    public override void Start()
    {

        textScreens[0].SetActive(true);

        // turn on table and goggles
        table.SetActive(true);
        goggles.SetActive(true);

        //TODO: Replace VRTK Grab with own grab system
        // turn on components to grab
        //RController.GetComponent<VRTK_InteractGrab>().enabled = true;
        //RController.GetComponent<VRTK_InteractTouch>().enabled = true;

        tableAnim.SetBool("on", true);
        gogglesAnim.SetBool("on", true);

        buttons.SetActive(true);

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
                buttons.SetActive(false);

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

        //TODO: Replace VRTK Grab with own grab system
        // turn off components to grab
        //RController.GetComponent<VRTK_InteractGrab>().enabled = false;
        //RController.GetComponent<VRTK_InteractTouch>().enabled = false;
    }
}

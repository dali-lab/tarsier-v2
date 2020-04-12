using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SwitchVisionsTutorial : TutorialBaseClass
{
    public GameObject RController;
    public GameObject LController;

    public GameObject[] visionAffectedObjects;
    public GameObject[] textScreens;

    public GameObject hotCube;
    public GameObject coldCube;
    public GameObject coldCube2;
    public GameObject table;
    public GameObject visionButton;

    public GameObject grabButton;

    public Animator cubesAnim;
    public Animator tableAnim;

    public Material coolMaterial;
    public Material hotMaterial;
    public Material regularMaterial;

    private VRTK_ControllerEvents RControllerEvents;
    private VRTK_ControllerEvents LControllerEvents;

    private GameObject[] throwables;
    private bool thermalVisionOn = false;

    private int currentIndex = 0;
    private float waitTime = 0.5f;
    private float timer = 0.0f;

    public override void Start()
    {

        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();

        RControllerEvents.ButtonOnePressed += changeVisions;

        textScreens[0].SetActive(true);

        for (int i = 0; i < visionAffectedObjects.Length; i++)
        {
            visionAffectedObjects[i].SetActive(true);
        }
        visionButton.SetActive(true);
        grabButton.SetActive(true);
        table.SetActive(true);
        hotCube.SetActive(true);
        coldCube.SetActive(true);
        coldCube2.SetActive(true);
        cubesAnim.SetBool("up", true);

        // turn on components to grab
        RController.GetComponent<VRTK_InteractGrab>().enabled = true;
        RController.GetComponent<VRTK_InteractTouch>().enabled = true;

        currentIndex = 0;

        //throwables = new GameObject[3];
        //throwables[0] = hotCube;
        //throwables[1] = coldCube;
        //throwables[2] = coldCube2;

        isDone = false;

    }

    // Update is called once per frame
    public override void Update()
    {
        if (RControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress))
        {
            if (currentIndex == 0)
            {
                textScreens[0].SetActive(false);
                currentIndex = 1;
                textScreens[1].SetActive(true);
            }
        }

        if (currentIndex == 1 && !isDone)
        {
            //grab super hot cube
            if (RControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress))
            {
                if (RController.GetComponent<VRTK_InteractGrab>().GetGrabbedObject() == hotCube)
                {
                    
                    visionButton.SetActive(false);
                    grabButton.SetActive(false);
                    cubesAnim.SetBool("up", false);
                    tableAnim.SetBool("on", false);

                    isDone = true;
                    textScreens[1].SetActive(false);

                }
            }

            if (LControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress))
            {
                if (LController.GetComponent<VRTK_InteractGrab>().GetGrabbedObject() == hotCube)
                {
                    hotCube.SetActive(false);
                    coldCube.SetActive(false);
                    coldCube2.SetActive(false);
                    visionButton.SetActive(false);

                    isDone = true;
                    textScreens[1].SetActive(false);
                }
            }
        }
    }

    public override void Disable()
    {

        hotCube.SetActive(false);
        coldCube.SetActive(false);
        coldCube2.SetActive(false);

        for (int i = 0; i < textScreens.Length; i++)
        {
            textScreens[i].SetActive(false);
        }

        // turn off components to grab
        RController.GetComponent<VRTK_InteractGrab>().enabled = false;
        RController.GetComponent<VRTK_InteractTouch>().enabled = false;

        for (int i = 0; i < visionAffectedObjects.Length; i++)
        {
            visionAffectedObjects[i].SetActive(false);
        }

        if (RControllerEvents != null)
        {
            RControllerEvents.ButtonOnePressed -= changeVisions;
        }
    }

    public void changeVisions(object sender, ControllerInteractionEventArgs e)
    {
        if (!thermalVisionOn)
        {

            for (int i = 0; i < visionAffectedObjects.Length; i++)
            {
                if (visionAffectedObjects[i] == hotCube)
                {
                    visionAffectedObjects[i].GetComponent<Renderer>().material = hotMaterial;
                } else
                {
                    visionAffectedObjects[i].GetComponent<Renderer>().material = coolMaterial;
                }
                
            }
            thermalVisionOn = true;

        }
        else
        {
            for (int i = 0; i < visionAffectedObjects.Length; i++)
            {
                visionAffectedObjects[i].GetComponent<Renderer>().material = regularMaterial;
            }

            thermalVisionOn = false;
        }

    }

    public bool IsThermalVisionOn()
    {
        return thermalVisionOn;
    }

}

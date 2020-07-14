using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

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

    private GameObject[] throwables;
    private bool thermalVisionOn = false;

    private int currentIndex = 0;
    private float waitTime = 0.5f;
    private float timer = 0.0f;
    private InputManager _inputManager;

    public override void Start()
    {

        _inputManager = InputManager.Instance;

        if (_inputManager == null)
        {
            throw new System.Exception("There must be an InputManager.cs script in the scene");
        }

        if (_inputManager != null)
        {
            _inputManager.OnButtonAPress += changeVisions;
        }

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

        // TODO: Replace VRTK Grab with the grab system we implement
        // turn on components to grab
        //RController.GetComponent<VRTK_InteractGrab>().enabled = true;
        //RController.GetComponent<VRTK_InteractTouch>().enabled = true;

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
        if (_inputManager.IsButtonPressed(InputManager.Button.A))
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
            // TODO: Replace VRTK Grab with own grab system; furthermore, grab stuff should not be in this file
            //if (RControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress))
            //{
            //    if (RController.GetComponent<VRTK_InteractGrab>().GetGrabbedObject() == hotCube)
            //    {
                    
            //        visionButton.SetActive(false);
            //        grabButton.SetActive(false);
            //        cubesAnim.SetBool("up", false);
            //        tableAnim.SetBool("on", false);

            //        isDone = true;
            //        textScreens[1].SetActive(false);

            //    }
            //}

            //if (LControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress))
            //{
            //    if (LController.GetComponent<VRTK_InteractGrab>().GetGrabbedObject() == hotCube)
            //    {
            //        hotCube.SetActive(false);
            //        coldCube.SetActive(false);
            //        coldCube2.SetActive(false);
            //        visionButton.SetActive(false);

            //        isDone = true;
            //        textScreens[1].SetActive(false);
            //    }
            //}
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

        // TODO: Replace VRTK Grab with the grab system we implement
        // turn off components to grab
        //RController.GetComponent<VRTK_InteractGrab>().enabled = false;
        //RController.GetComponent<VRTK_InteractTouch>().enabled = false;

        for (int i = 0; i < visionAffectedObjects.Length; i++)
        {
            visionAffectedObjects[i].SetActive(false);
        }

        if (_inputManager != null)
        {
            _inputManager.OnButtonAPress -= changeVisions;
        }
    }

    public void changeVisions()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class beeTutorial : MonoBehaviour
{
    public GameObject hapticCube;
    public GameObject test;
    public GameObject centerEye;
    public GameObject LController;
    public GameObject RController;
    public GameObject introPanel;

    public GameObject statusPanel;
    public GameObject nectarBar;

    public GameObject[] tutorialPanels;                                                 //fly1, fly2, fly3, fly4, nectar, vision, tutorialEnd
    public GameObject flyingPanel;
    public GameObject landingPanel;
    public GameObject energyPanel;
    public GameObject nectarPanel;
    public GameObject visionPanel;
    public GameObject goPanel;

    VRTK_ControllerReference LControllerRef;
    private VRTK_ControllerEvents LControllerEvents;
    private VRTK_ControllerEvents RControllerEvents;
    private VRTK_InteractGrab grabScript;
    private VRTK_InteractTouch interactTouch;

    private bool tutorialInProgress;
    private bool onFlower;
    private bool UIHit;
    private int currPanel;
    private bool haptics;

    // Start is called before the first frame update
    void Start()
    {
        grabScript = RController.GetComponent<VRTK_InteractGrab>();
        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents.ButtonOnePressed += DoLeftButtonOnePressed;                       // 'X' button on left controller
        LControllerRef = LController.GetComponent<VRTK_ControllerReference>();
        interactTouch = LController.GetComponent<VRTK_InteractTouch>();
        interactTouch.ControllerStartTouchInteractableObject += flowerTrigger;
        StartTutorial();    
    }

    private void StartTutorial()
    {
        tutorialInProgress = true;
        onFlower = false;
        UIHit = false;
        currPanel = 0;
        haptics = true;

    statusPanel.SetActive(false);
        nectarBar.SetActive(false);

        introPanel.SetActive(true);
        tutorialPanels[0].SetActive(true);

        for (int i = 1; i < tutorialPanels.Length; i++)
        {
            tutorialPanels[i].SetActive(false);
        }

        StartCoroutine(WaitIntro());                                                         // turns off intro panel in 10 sec
    }

    private void Update()
    {
        if (haptics)
        {
            hapticCube.transform.position = LController.transform.position;
        }
        if (currPanel == 0)
        {
            if (RControllerEvents.buttonOnePressed)                                         // 'A' button on right controller
            {
                tutorialPanels[0].SetActive(false);                                         // fly1
                tutorialPanels[1].SetActive(true);                                          // fly2
                currPanel = 1;
                StartCoroutine(WaitFly());                                                  // turns off intro panel in 10 sec
            }
        }
        else if (currPanel == 2)
        {
            Ray ray = new Ray(centerEye.transform.position, centerEye.transform.forward);       // cast a ray from the center eye
            RaycastHit hit;                                                                     // returns the hit variable to indicate what and where the ray 
            if (Physics.Raycast(ray, out hit))
            {
                if (UIHit == false && hit.transform.gameObject.tag == "UI")
                {
                    test.SetActive(false);
                    UIHit = true;
                    StartCoroutine(WaitNectar());
                }
            }
        }
        else if (currPanel == 3)
        {
            if (onFlower)
            {
                tutorialPanels[3].SetActive(false);                                         // fly4
                tutorialPanels[4].SetActive(true);                                          // nectar
                currPanel = 4;
            }
        }
        else if (currPanel == 4)
        {
            if (grabScript.GetGrabbedObject().tag == "nectar")
            {
                grabScript.GetGrabbedObject().SetActive(false);
                nectarBar.GetComponent<NectarUI>().addHealth(1);                            // set health to max

                tutorialPanels[4].SetActive(false);                                         // nectar
                tutorialPanels[5].SetActive(true);                                          // vision
                currPanel = 5;
            }
        }
        else if (currPanel == 5)
        {
            if (RControllerEvents.buttonTwoPressed)                                         // 'B' button on right controller
            {
                tutorialPanels[5].SetActive(false);                                         // vision
                tutorialPanels[6].SetActive(true);                                          // tutorialEnd
                currPanel = 6;
            }
        }
        else if (currPanel == 6)
        {
            tutorialPanels[6].SetActive(false);                                         // tutorialEnd
            statusPanel.SetActive(true);                                                // status panel
            tutorialInProgress = false;
        }

    }

    IEnumerator WaitIntro()
    {
        yield return new WaitForSeconds(10);
        introPanel.SetActive(false);
    }

    IEnumerator WaitFly()
    {
        yield return new WaitForSeconds(15);
        tutorialPanels[1].SetActive(false);                                                 // fly2
        tutorialPanels[2].SetActive(true);                                                  // fly3
        nectarBar.SetActive(true);
        currPanel = 2;
    }

    IEnumerator WaitNectar()
    {
        yield return new WaitForSeconds(8);
        tutorialPanels[2].SetActive(false);                                         // fly3
        tutorialPanels[3].SetActive(true);                                          // fly4
        currPanel = 3;
    }

    private void DoLeftButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        if (!tutorialInProgress)
        {
            StartTutorial();
        }
    }

    private void flowerTrigger(object sender, VRTK.ObjectInteractEventArgs e)
    {
        //if (interactTouch.GetTouchedObject().tag == "flower")
        //{
            test.SetActive(!test.activeSelf);
            onFlower = true;
        //}
    }

}

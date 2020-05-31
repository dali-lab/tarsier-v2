using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialNectar : BeeTutorialBaseClass
{
    public GameObject RController;
    public GameObject nextPanel;
    public GameObject nectarBar;
    public GameObject gripHighlight;

    private VRTK_InteractGrab grabScript;


    public override void Start()
    {
    }

    public override void OnEnable()
    {
        grabScript = RController.GetComponent<VRTK_InteractGrab>();
        gripHighlight.SetActive(true);
        isDone = false;
    }

    public override void Update()
    {
        if (grabScript.GetGrabbedObject() != null && grabScript.GetGrabbedObject().tag == "nectar")
        {
            grabScript.GetGrabbedObject().SetActive(false);
            nectarBar.GetComponent<NectarUI>().addHealth(1);                            // set health to max
            gripHighlight.SetActive(false);
            gameObject.SetActive(false);
            nextPanel.SetActive(true);
        }
    }

    public override void Disable()
    {
    }
}

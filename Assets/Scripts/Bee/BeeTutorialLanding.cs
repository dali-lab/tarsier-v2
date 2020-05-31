using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialLanding : BeeTutorialBaseClass
{
    public GameObject LController;
    public GameObject nextPanel;

    private VRTK_ControllerEvents LControllerEvents;
    private VRTK_InteractTouch interactTouch;

    private bool onFlower;

    public override void Start()
    {
    }

    public override void OnEnable()
    {
        interactTouch = LController.GetComponent<VRTK_InteractTouch>();
        interactTouch.ControllerStartTouchInteractableObject += flowerTrigger;

        onFlower = false;

        isDone = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (onFlower)
        {
            isDone = true;
            //gameObject.SetActive(false);
            //nextPanel.SetActive(true);
        }
    }

    public override void Disable()
    {
    }

    private void flowerTrigger(object sender, VRTK.ObjectInteractEventArgs e)
    {
        onFlower = true;
    }
}

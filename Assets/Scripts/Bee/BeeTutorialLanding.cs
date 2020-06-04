using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialLanding : MonoBehaviour
{
    public GameObject LController;
    public GameObject nextPanel;

    private VRTK_ControllerEvents LControllerEvents;
    private VRTK_InteractTouch interactTouch;

    private bool onFlower;


    public void OnEnable()
    {
        interactTouch = LController.GetComponent<VRTK_InteractTouch>();
        interactTouch.ControllerStartTouchInteractableObject += flowerTrigger;

        onFlower = false;
    }

    public void Update()
    {
        if (onFlower)                                           // if player has teleported to flower, move to next tutorial panel
        {
            onFlower = false;
            gameObject.SetActive(false);
            nextPanel.SetActive(true);
        }
    }

    private void flowerTrigger(object sender, VRTK.ObjectInteractEventArgs e)
    {
        onFlower = true;
        interactTouch.ControllerStartTouchInteractableObject -= flowerTrigger;
    }

    public void OnDisable()
    {
        interactTouch.ControllerStartTouchInteractableObject -= flowerTrigger;
    }
}

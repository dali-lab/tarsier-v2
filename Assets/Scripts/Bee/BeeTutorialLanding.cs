using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeTutorialLanding : MonoBehaviour
{
    public GameObject LController;
    public GameObject nextPanel;

    // TODO: Replace VRTK grab with our own system
    //private VRTK_ControllerEvents LControllerEvents;
    //private VRTK_InteractTouch interactTouch;

    private bool onFlower;


    public void OnEnable()
    {
        // TODO: Replace VRTK grab with our own system
        //interactTouch = LController.GetComponent<VRTK_InteractTouch>();
        //interactTouch.ControllerStartTouchInteractableObject += flowerTrigger;

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

    // TODO: Replace VRTK grab with our own system
    //private void flowerTrigger(object sender, VRTK.ObjectInteractEventArgs e)
    //{
    //    onFlower = true;
    //    interactTouch.ControllerStartTouchInteractableObject -= flowerTrigger;
    //}

    // TODO: Replace VRTK grab with our own system
    //public void OnDisable()
    //{
    //    interactTouch.ControllerStartTouchInteractableObject -= flowerTrigger;
    //}
}

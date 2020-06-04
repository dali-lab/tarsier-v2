using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialNectar : MonoBehaviour
{
    public GameObject RController;
    public GameObject nextPanel;
    public GameObject nectarBar;
    public GameObject gripHighlight;
    public GameObject tutorialNectar;

    private VRTK_ControllerEvents RControllerEvents;
    private bool isPressing = false;
    private Vector3 RControllerPoint;
    private Collider nectarCollider;


    public void OnEnable()
    {
        gripHighlight.SetActive(true);
        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        RControllerEvents.GripPressed += DoRightGripPressed;
        RControllerEvents.GripReleased += DoRightGripReleased;
        nectarCollider = tutorialNectar.GetComponent<Collider>();
    }

    public void Update()
    {
        RControllerPoint = RController.transform.position;
        if (isPressing == true && nectarCollider.bounds.Contains(RControllerPoint))
        {
            tutorialNectar.SetActive(false);
            nectarBar.GetComponent<NectarUI>().addHealth(1);                            // set health to max
            gripHighlight.SetActive(false);
            gameObject.SetActive(false);
            nextPanel.SetActive(true);
        }
    }

    private void DoRightGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        isPressing = true;
    }

    private void DoRightGripReleased(object sender, ControllerInteractionEventArgs e)
    {
        isPressing = false;
    }

    public void OnDisable()
    {
        RControllerEvents.GripPressed -= DoRightGripPressed;
        RControllerEvents.GripReleased -= DoRightGripReleased;
    }
}

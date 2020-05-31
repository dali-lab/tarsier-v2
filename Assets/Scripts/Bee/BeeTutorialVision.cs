using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialVision : BeeTutorialBaseClass
{
    public GameObject RController;
    public GameObject nextPanel;
    public GameObject buttonBHighlight;

    private VRTK_ControllerEvents RControllerEvents;


    public override void Start()
    {
    }

    public override void OnEnable()
    {
        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        RControllerEvents.ButtonTwoPressed += DoRightButtonTwoPressed;                       // 'B' button on right controller
        buttonBHighlight.SetActive(true);
        isDone = false;
    }

    public override void Update()
    {
    }
    private void DoRightButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
    {
        buttonBHighlight.SetActive(false);
        gameObject.SetActive(false);
        nextPanel.SetActive(true);
    }

    public override void Disable()
    {
    }
}

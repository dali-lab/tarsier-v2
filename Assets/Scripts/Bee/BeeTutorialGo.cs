using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialGo : BeeTutorialBaseClass
{
    public GameObject LController;
    public GameObject buttonXHighlight;

    private VRTK_ControllerEvents LControllerEvents;


    public override void Start()
    {
    }

    public override void OnEnable()
    {
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents.ButtonOnePressed += DoLeftButtonOnePressed;                       // 'X' button on left controller
        buttonXHighlight.SetActive(true);
        isDone = false;
    }

    public override void Update()
    {
    }
    private void DoLeftButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        buttonXHighlight.SetActive(false);
        gameObject.SetActive(false);
    }

    public override void Disable()
    {
    }
}

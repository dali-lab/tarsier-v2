using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class beeTutorial : MonoBehaviour
{
    public GameObject LController;
    public GameObject UIPanel;

    public GameObject textProceed;
    public GameObject titleVision;
    public GameObject textStart;

    public GameObject titleFly;
    public GameObject textFly;
    public GameObject textFlyTilt;

    public GameObject titleNectar;
    public GameObject textNectar;
    public GameObject textNectarGrab;

    public GameObject textNectarVision;
    public GameObject textClose;

    private VRTK_ControllerEvents LControllerEvents;
    private int press = 0;

    // Start is called before the first frame update
    void Start()
    {
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents.ButtonOnePressed += DoButtonOnePressed;

        UIPanel.SetActive(true);
        textProceed.SetActive(true);
        titleVision.SetActive(true);
        textStart.SetActive(true);
    }

    private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        press += 1;
        if (press == 1)
        {
            titleVision.SetActive(false);
            textStart.SetActive(false);
            titleFly.SetActive(true);
            textFly.SetActive(true);
        }
        else if (press == 2)
        {
            textFly.SetActive(false);
            textFlyTilt.SetActive(true);
        }
        else if (press == 3)
        {
            titleFly.SetActive(false);
            textFlyTilt.SetActive(false);
            titleNectar.SetActive(true);
            textNectar.SetActive(true);
        }
        else if (press == 4)
        {
            textNectar.SetActive(false);
            textNectarGrab.SetActive(true);
        }
        else if (press == 5)
        {
            textProceed.SetActive(false);
            textNectarGrab.SetActive(false);
            textNectarVision.SetActive(true);
            textClose.SetActive(true);
        }
        else if (press == 6)
        {
            UIPanel.SetActive(false);
        }
    }
}

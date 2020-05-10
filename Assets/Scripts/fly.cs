using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class fly : MonoBehaviour
{
    public GameObject centerEye;
    public GameObject flyParticles;
    public GameObject LController;                                  // turn fly on/off
    public GameObject blackScreen;

    private VRTK_ControllerEvents LControllerEvents;
    private float speed = .075f;                                   // fly speed
    private bool isFlying = false;

    private void OnEnable()
    {
        flyParticles.SetActive(false);
        blackScreen.SetActive(false);

        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents.ButtonOnePressed += DoButtonOnePressed;
    }

    private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        isFlying = !isFlying;
        StartCoroutine(movementTransition());
    }

    private void Update()
    {
        Fly();
    }


    private void Fly()                                          // fly via head tilt (tracks headset)
    {
        if (isFlying == true)
        {
            Vector3 flyDir = centerEye.transform.forward;
            transform.position += flyDir.normalized * speed;
        }
    }

    private IEnumerator movementTransition()                            // transition that triggers when button is pressed
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        flyParticles.SetActive(!flyParticles.activeSelf);
        blackScreen.SetActive(false);
    }
}
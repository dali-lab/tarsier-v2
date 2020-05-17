﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class fly : MonoBehaviour
{
    public GameObject centerEye;
    public GameObject windParticles;
    public GameObject RController;
    public SceneFader sceneFader;
    public float speed = .06f;

    private VRTK_ControllerEvents RControllerEvents;
    private bool isFlying = false;

    private void OnEnable()
    {
        windParticles.SetActive(false);

        RController.GetComponent<VRTK_Pointer>().enabled = true;
        RController.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;

        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        RControllerEvents.ButtonOnePressed += DoButtonOnePressed;
    }

    private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)                        // trigger transition, toggle teleport
    {
        StartCoroutine(movementTransition());
        RController.GetComponent<VRTK_Pointer>().enabled = !RController.GetComponent<VRTK_Pointer>().enabled;
        RController.GetComponent<VRTK_StraightPointerRenderer>().enabled = !RController.GetComponent<VRTK_StraightPointerRenderer>().enabled;
    }

    private void Update()
    {
        Fly();
    }


    private void Fly()                                              // fly via head tilt (tracks headset)
    {
        if (isFlying == true)
        {
            Vector3 flyDir = centerEye.transform.forward;
            transform.position += flyDir.normalized * speed;
        }
    }

    private IEnumerator movementTransition()                        // fade to black and unfade for transition
    {
        sceneFader.StartFade();
        yield return new WaitForSeconds(1);
        windParticles.SetActive(!windParticles.activeSelf);
        isFlying = !isFlying;
    }
}
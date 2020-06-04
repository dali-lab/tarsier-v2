﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialEnergy : MonoBehaviour
{
    public GameObject nextPanel;
    public GameObject LController;
    public GameObject nectarBar;
    public GameObject hapticCube;

    private VRTK_InteractGrab grabScript;
    private bool haptics;

    public void OnEnable()
    {
        haptics = false;
        StartCoroutine(Wait());
    }

    public void FixedUpdate()
    {
        if (haptics)
        {
            hapticCube.transform.position = LController.transform.position;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        haptics = true;
        nectarBar.SetActive(true);
        nectarBar.GetComponent<NectarUI>().addHealth(1);                            // set health to max
        yield return new WaitForSeconds(3);
        haptics = false;
        hapticCube.SetActive(false);
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        nextPanel.SetActive(true);
    }
}

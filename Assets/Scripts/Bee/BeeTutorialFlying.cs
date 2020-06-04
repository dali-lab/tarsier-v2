using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialFlying : MonoBehaviour
{
    public GameObject RController;
    public GameObject beeLeftControls;
    public GameObject nextPanel;
    public GameObject hapticCube;
    public GameObject buttonAHighlight;

    private bool haptics = false;


    public void OnEnable()
    {
        beeLeftControls.SetActive(false);
        buttonAHighlight.SetActive(true);

        StartCoroutine(WaitHaptics());
    }

    public void FixedUpdate()
    {
        if (haptics)
        {
            hapticCube.transform.position = RController.transform.position;
        }
    }

    public void Disable()
    {
        buttonAHighlight.SetActive(false);
    }

    IEnumerator WaitHaptics()
    {
        yield return new WaitForSeconds(10);
        haptics = true;
        yield return new WaitForSeconds(6);
        haptics = false;
        buttonAHighlight.SetActive(false);
        nextPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

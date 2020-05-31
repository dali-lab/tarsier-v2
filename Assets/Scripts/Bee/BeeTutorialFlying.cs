using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialFlying : BeeTutorialBaseClass
{
    public GameObject RController;

    public GameObject nextPanel;
    public GameObject hapticCube;
    public GameObject buttonAHighlight;

    private bool haptics = false;

    public override void Start()
    {
    }
    public override void Update()
    {
    }

    public override void OnEnable()
    {
        buttonAHighlight.SetActive(false);

        isDone = false;
        StartCoroutine(WaitHaptics());
    }

    public void FixedUpdate()
    {
        if (haptics)
        {
            hapticCube.transform.position = RController.transform.position;
        }
    }

    public override void Disable()
    {
        buttonAHighlight.SetActive(false);
    }

    IEnumerator WaitHaptics()
    {
        yield return new WaitForSeconds(10);
        haptics = true;
        buttonAHighlight.SetActive(true);
        yield return new WaitForSeconds(6);
        haptics = false;
        buttonAHighlight.SetActive(false);
        isDone = true;
        nextPanel.SetActive(true);
    }
}

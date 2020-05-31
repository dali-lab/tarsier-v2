using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeTutorialEnergy : BeeTutorialBaseClass
{
    public GameObject nextPanel;
    public GameObject LController;
    public GameObject nectarBar;
    public GameObject hapticCube;

    private VRTK_InteractGrab grabScript;
    private bool haptics;

    public override void Start()
    {
    }

    public override void OnEnable()
    {
        isDone = false;
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
        yield return new WaitForSeconds(6);
        gameObject.SetActive(false);
        nextPanel.SetActive(true);
    }

    public override void Update()
    {
    }

    public override void Disable()
    {
    }
}

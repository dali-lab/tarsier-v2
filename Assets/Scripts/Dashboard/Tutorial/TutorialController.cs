using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public TextMeshPro TMP;
    public TutorialStep[] tutorialSteps;

    private int _currStep = 0;

    private void Awake()
    {
        foreach (TutorialStep tutorialStep in tutorialSteps)
        {
            tutorialStep.Cleanup(TMP);      // ensures that all linked gameobjects are turned off
            if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
            tutorialStep.OnDone.AddListener(Next);
        }
        tutorialSteps[_currStep].gameObject.SetActive(true);
        tutorialSteps[_currStep].Setup(TMP);
    }

    public void Next()
    {
        tutorialSteps[_currStep].Cleanup(TMP);
        if (tutorialSteps[_currStep].AllowActiveFalse == true) tutorialSteps[_currStep].gameObject.SetActive(false);
        _currStep += 1;
        tutorialSteps[_currStep].gameObject.SetActive(true);
        tutorialSteps[_currStep].Setup(TMP);
    }
}

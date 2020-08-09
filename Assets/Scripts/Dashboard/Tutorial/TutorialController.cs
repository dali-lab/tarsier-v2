using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public TextMeshPro TMP;
    public TutorialStep[] tutorialSteps;
    public GameObject lobbyDefault;

    private int _currStep;

    private void OnEnable()
    {
        _currStep = 0;
        foreach (TutorialStep tutorialStep in tutorialSteps)
        {
            tutorialStep.Cleanup(TMP);      // ensures that all linked gameobjects are turned off
            if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
            tutorialStep.OnDone.AddListener(Next);
        }
        lobbyDefault.SetActive(false);
        tutorialSteps[_currStep].gameObject.SetActive(true);
        tutorialSteps[_currStep].Setup(TMP);
    }

    public void Next()
    {
        tutorialSteps[_currStep].Cleanup(TMP);
        if (tutorialSteps[_currStep].AllowActiveFalse == true) tutorialSteps[_currStep].gameObject.SetActive(false);
        _currStep += 1;
        
        if (_currStep < tutorialSteps.Length)
        {
            tutorialSteps[_currStep].gameObject.SetActive(true);
            tutorialSteps[_currStep].Setup(TMP);
        }
        else
        {
            lobbyDefault.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }
}

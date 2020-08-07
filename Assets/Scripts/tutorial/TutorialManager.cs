using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialStep[] tutorialSteps;
    public TextMeshPro TMP;
    private int currStep = 0;

    private void Awake()
    {
        foreach (TutorialStep tutorialStep in tutorialSteps)
        {
            tutorialStep.gameObject.SetActive(false);
            tutorialStep.TMP = TMP;
            tutorialStep.OnDone.AddListener(Next);
        }
        tutorialSteps[currStep].gameObject.SetActive(true);
    }
    public void Next()
    {
        tutorialSteps[currStep].gameObject.SetActive(false);
        currStep += 1;
        tutorialSteps[currStep].gameObject.SetActive(true);
    }
}

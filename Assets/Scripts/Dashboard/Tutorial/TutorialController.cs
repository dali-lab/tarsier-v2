using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

public class TutorialController : MonoBehaviour
{
    public TextMeshPro TMP;
    public TutorialStep[] tutorialSteps;
    public GameObject defaultLobbyDashboard;                 // default dashboard for the lobby

    public GameObject skipButton;
    public GameObject cameraRig;
    public GameObject mainIslandSpawnPoint;

    private TeleportController _teleportController;
    private int _currStep;

    private void OnEnable()
    {
        _teleportController = TeleportController.Instance;
        _currStep = 0;
        foreach (TutorialStep tutorialStep in tutorialSteps)
        {
            tutorialStep.Cleanup(TMP);
            if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
            tutorialStep.OnDone.AddListener(Next);
        }
        defaultLobbyDashboard.SetActive(false);

        skipButton.SetActive(true);
        skipButton.GetComponent<Button>().onClick.AddListener(Skip);

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
            defaultLobbyDashboard.SetActive(true);
            skipButton.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void Skip()
    {
        foreach (TutorialStep tutorialStep in tutorialSteps)
        {
            tutorialStep.Cleanup(TMP);
            if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
        }
        skipButton.SetActive(false);
        defaultLobbyDashboard.SetActive(true);
        gameObject.SetActive(false);
        _teleportController.enabled = true;
        cameraRig.transform.position = mainIslandSpawnPoint.transform.position;
    }
}

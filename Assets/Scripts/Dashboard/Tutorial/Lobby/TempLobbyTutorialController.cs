using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;
using Anivision.SceneManagement;

public class TempLobbyTutorialController : MonoBehaviour
{
    public TextMeshPro TMP;
    public TutorialStep[] tutorialSteps;
    public GameObject defaultDashboard;                 // default dashboard for the scene

    public GameObject skipButton;
    public GameObject replayButton;
    public GameObject cameraRig;
    public GameObject spawnPoint;

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
        defaultDashboard.SetActive(false);

        replayButton.SetActive(false);
        skipButton.SetActive(true);
        skipButton.GetComponent<Button>().onClick.AddListener(End);

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
            End();
        }
    }

    private void End()
    {
        foreach (TutorialStep tutorialStep in tutorialSteps)
        {
            tutorialStep.Cleanup(TMP);
            if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
        }
        skipButton.SetActive(false);
        skipButton.GetComponent<Button>().onClick.RemoveListener(End);

        replayButton.SetActive(true);
        replayButton.GetComponent<Button>().onClick.AddListener(ReplayTutorial);
        defaultDashboard.SetActive(true);

        _teleportController.enabled = true;
        cameraRig.transform.position = spawnPoint.transform.position;
    }

    private void ReplayTutorial()
    {
        gameObject.GetComponent<SceneSwitch>().StartTransition();
    }
}
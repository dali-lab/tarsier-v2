using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.SceneManagement;

public class LobbyDefault : MonoBehaviour
{
    [TextArea(3, 10)] public string dashboardText;
    public TextMeshPro TMP;
    public GameObject replayTutorialButton;
    public GameObject tutorialController;

    private HapticsController _hapticsController;


    private void OnEnable()
    {
        _hapticsController = HapticsController.Instance;
        _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);

        TMP.text = dashboardText;
        replayTutorialButton.SetActive(true);
        replayTutorialButton.GetComponent<Button>().onClick.AddListener(ReplayTutorial);
    }

    private void ReplayTutorial()
    {
        gameObject.GetComponent<SceneSwitch>().StartTransition();
    }

    private void OnDisable()
    {
        TMP.text = "";
        replayTutorialButton.SetActive(false);
        replayTutorialButton.GetComponent<Button>().onClick.RemoveListener(ReplayTutorial);
    }
}

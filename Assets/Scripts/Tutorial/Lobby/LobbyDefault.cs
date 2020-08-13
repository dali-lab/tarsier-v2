using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.SceneManagement;
using Anivision.PlayerInteraction;

public class LobbyDefault : MonoBehaviour
{
    [TextArea(3, 10)] public string dashboardText;
    public TextMeshPro TMP;
    public GameObject tutorialController;

    private HapticsController _hapticsController;


    private void OnEnable()
    {
        _hapticsController = HapticsController.Instance;
        _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);

        TMP.text = dashboardText;
    }

    private void OnDisable()
    {
        TMP.text = "";
    }
}

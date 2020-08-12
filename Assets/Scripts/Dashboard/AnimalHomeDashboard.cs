using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.SceneManagement;

public class AnimalHomeDashboard : Dashboard
{
    public new DashboardType dashboardType;
    [TextArea(3, 10)] public string dashboardText;
    public TextMeshPro TMP;
    public GameObject toLobbyButton;
    public GameObject otherVisionsButton;

    private DashboardController _dashboardController;


    public override void Setup()
    {
        _dashboardController = DashboardController.Instance;

        TMP.text = dashboardText;

        toLobbyButton.SetActive(true);
        toLobbyButton.GetComponent<Button>().onClick.AddListener(ToLobby);

        otherVisionsButton.SetActive(true);
        otherVisionsButton.GetComponent<Button>().onClick.AddListener(ToVisionSelectDashboard);
    }

    private void ToLobby()
    {
        gameObject.GetComponent<SceneSwitch>().StartTransition();
    }

    private void ToVisionSelectDashboard()
    {
        _dashboardController.UpdateDashboard(DashboardType.VisionSelect);
    }

    public override void Cleanup()
    {
        TMP.text = "";
        toLobbyButton.GetComponent<Button>().onClick.RemoveListener(ToLobby);
        toLobbyButton.SetActive(false);
    }
}

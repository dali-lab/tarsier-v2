using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.SceneManagement;

namespace Anivision.Dashboard
{
    public class AnimalHomeDashboard : Dashboard
    {
        public override DashboardType dashboardType => DashboardType.Home;

        [TextArea(3, 10)] public string dashboardText;
        public TextMeshPro TMP;

        public Button replayButton;
        [Tooltip("The name of this current scene to reload.")]
        public string replayScene = "ForestScene";

        public GameObject toLobbyButton;
        [Tooltip("The name of the lobby scene to back to.")]
        public string lobbyScene = "LobbyScene";

        public GameObject otherVisionsButton;

        private DashboardController _dashboardController;


        public override void Setup()
        {
            _dashboardController = DashboardController.Instance;

            TMP.text = dashboardText;

            replayButton.gameObject.SetActive(true);
            replayButton.onClick.AddListener(ReplayTutorial);

            toLobbyButton.SetActive(true);
            toLobbyButton.GetComponent<Button>().onClick.AddListener(ToLobby);

            otherVisionsButton.SetActive(true);
            otherVisionsButton.GetComponent<Button>().onClick.AddListener(ToVisionSelectDashboard);
        }

        private void ReplayTutorial()
        {
            gameObject.GetComponent<SceneSwitch>().sceneName = replayScene;
            gameObject.GetComponent<SceneSwitch>().StartTransition();
        }

        private void ToLobby()
        {
            gameObject.GetComponent<SceneSwitch>().sceneName = lobbyScene;
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
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.SceneManagement;

namespace Anivision.Dashboard
{
    public class HomeDashboard : Dashboard
    {
        public override DashboardType dashboardType => DashboardType.Home;

        [TextArea(3, 10)] public string dashboardText;
        public TextMeshPro TMP;
        [Tooltip("Is this the lobby scene? If so, hide the 'back to lobby button' and 'other visions' button")]
        public bool isLobby;

        public Button replayButton;
        [Tooltip("The name of this current scene to reload.")]
        public string replayScene = "ForestScene";

        public Button toLobbyButton;
        [Tooltip("The name of the lobby scene to back to.")]
        public string lobbyScene = "LobbyScene";

        public Button otherVisionsButton;

        private DashboardController _dashboardController;


        public override void Setup()
        {
            _dashboardController = DashboardController.Instance;

            TMP.text = dashboardText;

            replayButton.gameObject.SetActive(true);
            replayButton.onClick.AddListener(ReplayTutorial);

            if (!isLobby)
            {
                toLobbyButton.gameObject.SetActive(true);
                toLobbyButton.onClick.AddListener(ToLobby);

                otherVisionsButton.gameObject.SetActive(true);
                otherVisionsButton.onClick.AddListener(ToVisionSelectDashboard);
            }
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
            replayButton.onClick.RemoveListener(ToLobby);
            replayButton.gameObject.SetActive(false);

            if (!isLobby)
            {
                toLobbyButton.onClick.RemoveListener(ToLobby);
                toLobbyButton.gameObject.SetActive(false);

                otherVisionsButton.onClick.RemoveListener(ToVisionSelectDashboard);
                otherVisionsButton.gameObject.SetActive(false);
            }
        }
    }
}

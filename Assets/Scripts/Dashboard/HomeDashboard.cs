using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using Anivision.NotebookSystem;
using TMPro;
using UnityEngine;
using Anivision.SceneManagement;
using UnityEngine.SceneManagement;

namespace Anivision.Dashboard
{
    /// <summary>
    /// The dashboard for the home panel that listens for presses that replays the tutorial, goes back to the lobby, and switches to the other animal visions dashboard
    /// </summary>
    public class HomeDashboard : Dashboard
    {
        public override DashboardType dashboardType => DashboardType.Home;

        [TextArea(3, 10)] public string dashboardText;
        public TextMeshPro TMP;
        [Tooltip("If this is the lobby scene, the 'back to lobby button' and 'other visions' button will be hidden.")]
        public bool isLobby;

        public Button replayButton;
        [Tooltip("The name of this current scene to reload.")]
        public string replayScene = "ForestScene";

        public Button toLobbyButton;
        [Tooltip("The name of the lobby scene to go back to.")]
        public string lobbyScene = "LobbyScene";

        [Tooltip("The name of the button that pulls up the animal visions dashboard.")]
        public Button otherVisionsButton;

        private DashboardController _dashboardController;


        public override void Setup()
        {
            _dashboardController = DashboardController.Instance;

            TMP.text = dashboardText;

            replayButton.gameObject.SetActive(true);
            replayButton.onClick.AddListener(ReplayTutorial);

            if (isLobby)                                                                    // the home dashboard in the lobby hides the 'to lobby' and 'other vision' buttons
            {
                toLobbyButton.gameObject.SetActive(false);
                otherVisionsButton.gameObject.SetActive(false);
            }
            else
            {
                toLobbyButton.gameObject.SetActive(true);
                toLobbyButton.onClick.AddListener(ToLobby);

                otherVisionsButton.gameObject.SetActive(true);
                otherVisionsButton.onClick.AddListener(ToVisionSelectDashboard);
            }
        }

        private void ReplayTutorial()                                                       // reloads the current scene to reset the tutorial
        {
            gameObject.GetComponent<SceneSwitch>().sceneName = replayScene;
            if (Save.Instance != null) Save.Instance.RemoveActiveSceneFromPreviouslyVisited(); // remove from previously visited scenes so that we can spawn in proper place
            gameObject.GetComponent<SceneSwitch>().StartTransition();
        }

        private void ToLobby()                                                              // loads into the lobby scene
        {
            gameObject.GetComponent<SceneSwitch>().sceneName = lobbyScene;
            gameObject.GetComponent<SceneSwitch>().StartTransition();
        }

        private void ToVisionSelectDashboard()                                              // swap out this current home dashboard for the animal visions dashboard
        {
            _dashboardController.UpdateDashboard(DashboardType.VisionSelect);
        }

        public override void Cleanup()
        {
            TMP.text = "";
            replayButton.onClick.RemoveListener(ReplayTutorial);
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

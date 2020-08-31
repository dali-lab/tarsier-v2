using Anivision.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Tutorial;
using UnityEngine.SceneManagement;
using Anivision.Core;

/// <summary>
/// This script is attached to the button that will reload the scene and replay the tutorial
/// </summary>
namespace Anivision.NotebookSystem
{
    public class ReplayTutorial : MonoBehaviour
    {
        private Button _replayButton;

        private void OnEnable()
        {
            _replayButton = gameObject.GetComponent<Button>();
            if (_replayButton == null) throw new System.Exception("Must have a button script on this object");
            _replayButton.onClick.AddListener(Replay);
        }

        private void Replay()                                                       // reloads the current scene to reset the tutorial
        {
            if (Save.Instance != null) Save.Instance.RemoveActiveSceneFromPreviouslyVisited(); // remove from previously visited scenes so that we can spawn in proper place
            gameObject.GetComponent<SceneSwitch>().StartTransition();
        }
    }
}

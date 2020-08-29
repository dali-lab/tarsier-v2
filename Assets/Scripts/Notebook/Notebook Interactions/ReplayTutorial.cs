using Anivision.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private void Replay()
        {
            gameObject.GetComponent<SceneSwitch>().StartTransition();
        }
    }
}

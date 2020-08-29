using Anivision.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is attached to the button that will move the player back to the main island.
/// </summary>
namespace Anivision.NotebookSystem
{
    public class ToMainIsland : MonoBehaviour
    {
        private Button _toMainIslandButton;

        private void OnEnable()
        {
            _toMainIslandButton = gameObject.GetComponent<Button>();
            if (_toMainIslandButton == null) throw new System.Exception("Must have a button script on this object");
            _toMainIslandButton.onClick.AddListener(Transition);
        }

        private void Transition()
        {
            gameObject.GetComponent<SceneSwitch>().StartTransition();
        }
    }
}

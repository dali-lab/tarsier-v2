using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Teaches the player how to teleport.
    /// Invokes OnDone to move on the the next step once the player reaches the main animal island.
    /// </summary>
    public class LobbyTeleport : TutorialStep
    {
        public GameObject platforms;
        public GameObject RTriggerHighlight;

        private TeleportController _teleportController;


        public override void Setup(TextMeshPro TMP)
        {
            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            //TMP.text = dashboardText;
            
            // turn on the relevant tutorial items
            platforms.SetActive(true);
            RTriggerHighlight.SetActive(true);

            _teleportController.enabled = true;
        }

        public void OnTriggerExit(Collider other)           // triggers when player reaches the main island
        {
            if (other.tag == "MainCamera")
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup(TextMeshPro TMP)
        {
            TMP.text = "";
            platforms.SetActive(false);
            RTriggerHighlight.SetActive(false);
        }
    }
}

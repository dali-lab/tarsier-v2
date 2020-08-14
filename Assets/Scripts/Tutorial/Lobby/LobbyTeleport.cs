using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    public class LobbyTeleport : TutorialStep
    {
        public GameObject platforms;
        public GameObject RTriggerHighlight;

        private TeleportController _teleportController;


        public override void Setup(TextMeshPro TMP)
        {
            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            TMP.text = dashboardText;
            _teleportController.enabled = true;
            platforms.SetActive(true);
            RTriggerHighlight.SetActive(true);
        }

        public void OnTriggerExit(Collider other)
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    public class LobbyTeleport : TutorialStep
    {
        public TeleportController teleportController;
        public GameObject platforms;
        public GameObject RTriggerHighlight;

        public override void Setup(TextMeshPro TMP)
        {
            TMP.text = dashboardText;
            teleportController.enabled = true;
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

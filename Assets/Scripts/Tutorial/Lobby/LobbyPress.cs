using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    public class LobbyPress : TutorialStep
    {
        public GameObject welcomePanel;
        public GameObject startButton;
        public GameObject RTriggerHighlight;

        private TeleportController _teleportController;
        private HapticsController _hapticsController;


        public override void Setup(TextMeshPro TMP)
        {
            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            TMP.text = dashboardText;
            welcomePanel.SetActive(true);
            RTriggerHighlight.SetActive(true);

            startButton.SetActive(true);
            startButton.GetComponent<Button>().onClick.AddListener(Continue);

            _teleportController.enabled = false;
            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
        }

        private void Continue()
        {
            OnDone.Invoke();
        }

        public override void Cleanup(TextMeshPro TMP)
        {
            TMP.text = "";
            welcomePanel.SetActive(false);
            RTriggerHighlight.SetActive(false);

            startButton.GetComponent<Button>().onClick.RemoveListener(Continue);
            startButton.SetActive(false);
        }
    }
}

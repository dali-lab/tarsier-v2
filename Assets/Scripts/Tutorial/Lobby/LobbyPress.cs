﻿using System;
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

        private HapticsController _hapticsController;
        private bool done;


        public override void Setup(TextMeshPro TMP)
        {
            _hapticsController = HapticsController.Instance;
            done = false;

            TMP.text = dashboardText;
            welcomePanel.SetActive(true);
            startButton.SetActive(true);
            startButton.GetComponent<Button>().onClick.AddListener(Continue);

            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
        }

        private void Continue()
        {
            if (!done)
            {
                done = true;
                OnDone.Invoke();
            }
        }

        public override void Cleanup(TextMeshPro TMP)
        {
            TMP.text = "";
            welcomePanel.SetActive(false);
            startButton.GetComponent<Button>().onClick.RemoveListener(Continue);
            startButton.SetActive(false);
        }
    }
}

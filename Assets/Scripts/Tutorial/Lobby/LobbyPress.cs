﻿using System;
using System.Collections;
using System.Collections.Generic;
using Anivision.NotebookSystem;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Teaches the player how to press physical buttons in the world.
    /// Invokes OnDone to move on the the next step once the player presses the start tutorial button.
    /// </summary>
    public class LobbyPress : TutorialStep
    {
        public Button startButton;
        public GameObject RTriggerHighlight;

        private TeleportController _teleportController;
        private HapticsController _hapticsController;
        private AudioSource _audioSource;
        private bool _stepDone = false;


        public override void Setup()
        {
            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            // set up the corresponding page of the tutorial notebook
            chapter.PresentPage(page);

            // turn on the relevant tutorial items
            RTriggerHighlight.SetActive(true);
            startButton.gameObject.SetActive(true);
            startButton.onClick.AddListener(Continue);

            _teleportController.enabled = false;                                        // turn off teleport until teleport step of tutorial

            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
            _audioSource.Play();
        }

        private void Continue()
        {
            _stepDone = true;
        }

        private void Update()
        {
            // if player has pressed the button and voiceover is done, move on
            if (_stepDone && !_audioSource.isPlaying) 
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup()
        {
            RTriggerHighlight.SetActive(false);

            startButton.GetComponent<Button>().onClick.RemoveListener(Continue);
            startButton.gameObject.SetActive(false);

            page.Cleanup();
        }
    }
}

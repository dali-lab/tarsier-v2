﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.Core;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Teaches the player how to teleport between saplings.
    /// Moves on to the next step of the tutorial when the player leaves the collider of this gameobject.
    /// </summary>
    public class TutorialSaplings : TutorialStep
    {
        public GameObject triggerHighlightRing;

        private InputManager _inputManager;
        private TeleportController _teleportController;
        private AudioSource _audioSource;
        private bool _stepDone = false;


        public override void Setup()
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");

            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            // set up the corresponding page of the tutorial notebook
            chapter.PresentPage(page);

            triggerHighlightRing.SetActive(true);

            _teleportController.enabled = true;             // turn on ability to teleport
            _audioSource.Play();
        }

        private void OnTriggerExit(Collider other)          // triggers when player leaves capsule collider
        {
            if (other.tag == "MainCamera")
            {
                _stepDone = true;
            }
        }

        private void Update()
        {
            if (_stepDone && !_audioSource.isPlaying)       // if player had exited trigger and voiceover is done, move on
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup()
        {
            triggerHighlightRing.SetActive(false);
            page.Cleanup();
        }
    }
}

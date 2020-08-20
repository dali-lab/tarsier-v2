using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Changes the player to the size of the animal.
    /// Invokes OnDone to move on the the next step once the player presses the button on the dashboard by their left hand.
    /// </summary>
    public class TutorialSize : TutorialStep
    {
        public Button startButton;
        public GameObject RTriggerHighlight;

        private HapticsController _hapticsController;
        private AudioSource _audioSource;
        private bool _stepDone = false;


        public override void Setup(TextMeshPro TMP)
        {
            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            TMP.text = dashboardText;

            // turn on the relevant tutorial items
            RTriggerHighlight.SetActive(true);
            startButton.gameObject.SetActive(true);

            startButton.onClick.AddListener(Continue);

            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
            _audioSource.Play();
        }

        private void Continue()
        {
            _stepDone = true;
        }

        private void Update()
        {
            if (_stepDone && !_audioSource.isPlaying)       // if player had exited trigger and voiceover is done, move on
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup(TextMeshPro TMP)
        {
            TMP.text = "";
            RTriggerHighlight.SetActive(false);

            startButton.GetComponent<Button>().onClick.RemoveListener(Continue);
            startButton.gameObject.SetActive(false);
        }
    }
}

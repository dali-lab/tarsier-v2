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
        private AudioSource _audioSource;
        private bool _stepDone = false;


        public override void Setup()
        {
            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            // set up the corresponding page of the tutorial notebook
            chapter.PresentPage(page);

            // turn on the relevant tutorial items
            platforms.SetActive(true);
            RTriggerHighlight.SetActive(true);

            _teleportController.enabled = true;
            _audioSource.Play();
        }

        // triggers when player reaches the main island
        public void OnTriggerExit(Collider other)
        {
            if (other.tag == "MainCamera")
            {
                _stepDone = true;
            }
        }

        private void Update()
        {
            // if player has left the collider and voiceover is done, move on
            if (_stepDone && !_audioSource.isPlaying) 
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup()
        {
            platforms.SetActive(false);
            RTriggerHighlight.SetActive(false);

            page.Cleanup();
        }
    }
}

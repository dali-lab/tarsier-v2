using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.Core;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{    
     /// <summary>
     /// Teaches the player how to fly
     /// Moves on to the next step of the tutorial when the player presses the B button.
     /// </summary>
    public class TutorialFly : TutorialStep
    {
        public GameObject bHighlightRing;

        private InputManager _inputManager;
        private AudioSource _audioSource;
        private bool _stepDone = false;


        public override void Setup()
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            // set up the corresponding page of the tutorial notebook
            chapter.PresentPage(page);

            bHighlightRing.SetActive(true);

            _audioSource.Play();
        }

        private void Update()
        {
            if (_inputManager.IsButtonPressed(InputManager.Button.B))
            {
                _stepDone = true;
            }
            if (_stepDone && !_audioSource.isPlaying) // if player had pressed B and voiceover is done, move on
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup()
        {
            bHighlightRing.SetActive(false);
            page.Cleanup();
        }
    }
}

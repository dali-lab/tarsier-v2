using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.Core;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Teaches the player how to press buttons on the controllers.
    /// Invokes OnDone to move on the the next step once the player presses all the buttons.
    /// </summary>
    public class LobbyControllerPress : TutorialStep
    {
        public GameObject LHighlightRings;
        public GameObject RHighlightRings;

        private InputManager _inputManager;
        private HapticsController _hapticsController;
        private AudioSource _audioSource;

        private GameObject _currHighlight;
        private int _highlightsOn = 10;

        public override void Setup()
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) throw new System.Exception("Must have an input manager in the scene");

            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            // set up the corresponding page of the tutorial notebook
            chapter.PresentPage(page);

            // turn on the relevant tutorial items
            foreach (Transform child in LHighlightRings.transform) child.gameObject.SetActive(true);
            foreach (Transform child in RHighlightRings.transform) child.gameObject.SetActive(true);

            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
            _audioSource.Play();
        }

        private void Update()                                                   // turn off the highlight when corresponding button is pressed
        {
            if (_highlightsOn == 0 && !_audioSource.isPlaying) OnDone.Invoke(); // if player has pressed all the buttons and voiceover is done, move on

            if (_inputManager.IsButtonPressed(InputManager.Button.A)) _currHighlight = RHighlightRings.transform.Find("aRing").gameObject;
            if (_inputManager.IsButtonPressed(InputManager.Button.B)) _currHighlight = RHighlightRings.transform.Find("bRing").gameObject;
            if (_inputManager.IsButtonPressed(InputManager.Button.RIGHT_GRIP)) _currHighlight = RHighlightRings.transform.Find("gripRing").gameObject;
            if (_inputManager.IsButtonPressed(InputManager.Button.RIGHT_JOYSTICK)) _currHighlight = RHighlightRings.transform.Find("joystickRing").gameObject;
            if (_inputManager.IsButtonPressed(InputManager.Button.RIGHT_TRIGGER)) _currHighlight = RHighlightRings.transform.Find("triggerRing").gameObject;

            if (_inputManager.IsButtonPressed(InputManager.Button.X)) _currHighlight = LHighlightRings.transform.Find("xRing").gameObject;
            if (_inputManager.IsButtonPressed(InputManager.Button.Y)) _currHighlight = LHighlightRings.transform.Find("yRing").gameObject;
            if (_inputManager.IsButtonPressed(InputManager.Button.LEFT_GRIP)) _currHighlight = LHighlightRings.transform.Find("gripRing").gameObject;
            if (_inputManager.IsButtonPressed(InputManager.Button.LEFT_JOYSTICK)) _currHighlight = LHighlightRings.transform.Find("joystickRing").gameObject;
            if (_inputManager.IsButtonPressed(InputManager.Button.LEFT_TRIGGER)) _currHighlight = LHighlightRings.transform.Find("triggerRing").gameObject;

            if (_currHighlight != null)
            {
                if (_currHighlight.activeSelf == true)
                {
                    _currHighlight.SetActive(false);
                    _highlightsOn -= 1;
                }
            }
        }

        public override void Cleanup()
        {
            foreach (Transform child in LHighlightRings.transform) child.gameObject.SetActive(false);
            foreach (Transform child in RHighlightRings.transform) child.gameObject.SetActive(false);

            page.Cleanup();
        }
    }
}

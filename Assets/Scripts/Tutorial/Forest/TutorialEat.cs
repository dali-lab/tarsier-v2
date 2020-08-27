using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.Core;
using Anivision.PlayerInteraction;
using System;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Teaches the player how to grab and eat.
    /// Moves on to the next step of the tutorial when the player eats something tagged "edible".
    /// </summary>
    public class TutorialEat : TutorialStep
    {
        public GameObject gripHighlightRing;
        public HeadsetCollide headsetCollide;

        private InputManager _inputManager;
        private AudioSource _audioSource;
        private bool _stepDone = false;

        private Grabber[] _Grabbers;
        private Grabber _RGrabber;
        private Grabber _LGrabber;



        public override void Setup()
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            _Grabbers = FindObjectsOfType<Grabber>();
            if (_Grabbers.Length > 0)
            {
                _RGrabber = _Grabbers[0];

                if (_Grabbers.Length > 1)
                {
                    _LGrabber = _Grabbers[1];
                }
            }

            page.Setup();

            // relevant tutorial items
            gripHighlightRing.SetActive(true);
            headsetCollide.onCollide.AddListener(Done);

            _audioSource.Play();
        }

        private void Done(Collider other)
        {
            // if the object being grabbed is the object that the headset is colliding with
            if ((_Grabbers.Length > 0 && _RGrabber.GrabbedObject == other.gameObject) || (_Grabbers.Length > 1 && _LGrabber.GrabbedObject == other.gameObject))
            {
                if (other.gameObject.tag == "edible")           // if the object the headset is colliding with is tagged as edible
                {
                    _stepDone = true;
                }
            }
        }

        private void Update()
        {
            if (_stepDone && !_audioSource.isPlaying)           // if player has eaten a katydid and voiceover is done, move on
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup()
        {
            gripHighlightRing.SetActive(false);
            headsetCollide.onCollide.RemoveListener(Done);

            page.Cleanup();
        }
    }
}

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
        private Grabber[] _Grabbers;
        private Grabber _RGrabber;
        private Grabber _LGrabber;


        public override void Setup(TextMeshPro TMP)
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");

            _Grabbers = FindObjectsOfType<Grabber>();
            if (_Grabbers.Length > 0)
            {
                _RGrabber = _Grabbers[0];

                if (_Grabbers.Length > 1)
                {
                    _LGrabber = _Grabbers[1];
                }
            }

            TMP.text = dashboardText;
            gripHighlightRing.SetActive(true);

            headsetCollide.onCollide.AddListener(Done);
        }

        private void Done(Collider other)
        {
            // if the object being grabbed is the object that the headset is colliding with
            if ((_Grabbers.Length > 0 && _RGrabber.GrabbedObject == other.gameObject) || (_Grabbers.Length > 1 && _LGrabber.GrabbedObject == other.gameObject))
            {
                if (other.gameObject.tag == "edible")           // if the object the headset is colliding with is tagged as edible
                {
                    OnDone.Invoke();
                }
            }
        }

        public override void Cleanup(TextMeshPro TMP)
        {
            TMP.text = "";
            gripHighlightRing.SetActive(false);

            headsetCollide.onCollide.RemoveListener(Done);
        }
    }
}

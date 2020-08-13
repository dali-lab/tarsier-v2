using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Anivision.PlayerInteraction;

namespace Anivision.PlayerInteraction
{
    public class HeadsetCollide : MonoBehaviour
    {
        public AudioSource katydidEatSound;

        private Grabber[] _Grabbers;
        private Grabber _RGrabber;
        private Grabber _LGrabber;

        [HideInInspector] public UnityEvent onCollide;

        private void Start()
        {
            _Grabbers = FindObjectsOfType<Grabber>();
            if (_Grabbers.Length > 0)
            {
                _RGrabber = _Grabbers[0];

                if (_Grabbers.Length > 1)
                {
                    _LGrabber = _Grabbers[1];
                }
            }
            katydidEatSound = GetComponent<AudioSource>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if ((_Grabbers.Length > 0 && _RGrabber.GrabbedObject == other.gameObject) || (_Grabbers.Length > 1 && _LGrabber.GrabbedObject == other.gameObject))
            {
                if (other.gameObject.tag == "edible")
                {
                    katydidEatSound.Play();
                    other.gameObject.GetComponent<Grabbable>().EndGrab();
                    Destroy(other.gameObject);
                    onCollide.Invoke();
                }
            }
        }
    }
}

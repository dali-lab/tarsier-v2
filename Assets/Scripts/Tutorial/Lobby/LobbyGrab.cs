using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Teaches the player how to grab objects in the environment
    /// This stamp is attached to the collider on the notebook
    /// Invokes OnDone to move on the the next step once the player grabs the stamp and stamps the notebook
    /// </summary>
    public class LobbyGrab : TutorialStep
    {
        public GameObject stampColliderPosition;            // an empty gameobject that holds the position that this collider needs to be at so the notebook that detects that it has been stamped
        public GameObject stand;                            // mini table that appears with the stamp on it
        public GameObject stamp;                            // the stamp itself
        public GameObject accessGrantedMark;                // what appears on the notebook when it has been stamped
        public GameObject RGripHighlight;

        private HapticsController _hapticsController;
        private AudioSource _audioSource;
        private bool _stepDone = false;


        public override void Setup()
        {
            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            // set up the corresponding page of the tutorial notebook
            chapter.PresentPage(page);

            // turn on the relevant tutorial items
            stand.SetActive(true);
            stamp.SetActive(true);
            accessGrantedMark.SetActive(true);
            RGripHighlight.SetActive(true);

            // sets the position of this object to the position it needs to be on the notebook at to detect that it has been stamped
            transform.parent = stampColliderPosition.transform;
            transform.localPosition = Vector3.zero;
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.identity;

            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.RTouch);
            _audioSource.Play();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == stamp)
            {
                accessGrantedMark.transform.parent = stampColliderPosition.transform;  // when the stamp hits the notebook, parent the logo to the notebook instead of the stamp
                _hapticsController.Haptics(1, 0.5f, 0.25f, OVRInput.Controller.RTouch);
                _stepDone = true;
            }
        }

        private void Update()
        {
            // if player has stamped the page and voiceover is done, move on
            if (_stepDone && !_audioSource.isPlaying) 
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup()
        {
            stand.SetActive(false);
            stamp.SetActive(false);
            accessGrantedMark.SetActive(false);
            RGripHighlight.SetActive(false);

            page.Cleanup();
        }
    }
}

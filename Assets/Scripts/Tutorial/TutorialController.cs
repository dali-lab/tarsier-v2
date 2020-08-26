﻿using Anivision.Core;
using Anivision.Dashboard;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Anivision.PlayerInteraction;
using Anivision.Notebook;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Increments through all the tutorial steps.
    /// Listens for the OnDone UnityEvent from each individual step to clean up the previous step and set up the next one.
    /// When the tutorial is over (either completed or skipped), the TutorialEnd event is invoked.
    /// </summary>
    public class TutorialController : MonoBehaviour
    {
        public TutorialStep[] tutorialSteps;
        public Chapter TutorialChapter;
        public GameObject cameraRig;
        public GameObject spawnPoint;
        [Tooltip("Whether to move the player to the spawn point when skipping tutorial")]
        public bool moveToSpawn;

        public bool playTutorialEveryTime = true;

        private TeleportController _teleportController;
        private HapticsController _hapticsController;
        private AudioSource _audioSource;
        //private Notebook _notebookController;

        private int _currStep;
        private bool _skipped = false;

        private void OnEnable()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");


            if (playTutorialEveryTime || !Save.Instance.PreviouslyVisited(SceneManager.GetActiveScene()))   // moves player directly to main island if have already done tutorial
            {
                // clean up all tutorial objects and add OnDone listener
                foreach (TutorialStep tutorialStep in tutorialSteps)
                {
                    tutorialStep.Cleanup();
                    if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
                    tutorialStep.OnDone.AddListener(Next);
                }
                Setup();
            }
            else
            {
                enabled = false;
            }            
        }
        private void Setup()
        {
            TutorialChapter.gameObject.SetActive(true);
            TutorialChapter.Setup();

            foreach (TutorialStep tutorialStep in tutorialSteps)
            {
                tutorialStep.Cleanup();
                tutorialStep.chapter = TutorialChapter;
                if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
            }
            // set up the first tutorial item
            _currStep = 0;
            tutorialSteps[_currStep].gameObject.SetActive(true);
            tutorialSteps[_currStep].Setup();
            tutorialSteps[_currStep].OnDone.AddListener(Next);

            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);

        }

        public void Next()
        {
            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);

            // clean up the current step
            tutorialSteps[_currStep].Cleanup();
            if (tutorialSteps[_currStep].AllowActiveFalse == true) tutorialSteps[_currStep].gameObject.SetActive(false);

            // set up the next step
            _currStep += 1;
            if (_currStep < tutorialSteps.Length)
            {
                tutorialSteps[_currStep].gameObject.SetActive(true);
                tutorialSteps[_currStep].Setup();
                tutorialSteps[_currStep].OnDone.AddListener(Next);
            }
            else
            {
                enabled = false;
            }
        }

        public void Skip()
        {
            _skipped = true;
            enabled = false;
        }

        private void OnDisable()
        {
            Cleanup();           
        }

        private void Cleanup()
        {
            foreach (TutorialStep tutorialStep in tutorialSteps)
            {
                tutorialStep.Cleanup();
                tutorialStep.OnDone.RemoveListener(Next);
                if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
            }
            _teleportController.enabled = true;
            if (moveToSpawn && _skipped) cameraRig.transform.position = spawnPoint.transform.position;          // only move to spawn point if skipping, otherwise player will already be on main island
            // _audioSource.Play();
        }
    }
}

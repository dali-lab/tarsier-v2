using Anivision.Core;
using Anivision.Dashboard;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Anivision.PlayerInteraction;
using UnityEngine.SceneManagement;

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
        //public TutorialDashboard TutorialDashboard;
        //public Chapter TutorialChapter;
        public GameObject cameraRig;
        public GameObject spawnPoint;
        [Tooltip("Whether to move the player to the spawn point when skipping tutorial")]
        public bool moveToSpawn;

        public bool playTutorialEveryTime = true;

        private TeleportController _teleportController;
        private HapticsController _hapticsController;
        private AudioSource _audioSource;

        private int _currStep;
        private bool _skipped = false;

        private void OnEnable()
        {
            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            if (playTutorialEveryTime || !Save.Instance.PreviouslyVisited(SceneManager.GetActiveScene()))
            {
                // clean up all tutorial objects and add OnDone listener
                foreach (TutorialStep tutorialStep in tutorialSteps)
                {
                    //tutorialStep.Cleanup(TutorialDashboard.TMP);
                    if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
                    tutorialStep.OnDone.AddListener(Next);
                }
            
                //TutorialDashboard.Setup();

                // set up the first tutorial item
                _currStep = 0;
                tutorialSteps[_currStep].gameObject.SetActive(true);
                //tutorialSteps[_currStep].Setup(TutorialDashboard.TMP);
                
                _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
            }
            else
            {
                End();
            }

            Setup();
            
        }
        private void Setup()
        {
            //TutorialChapter.Setup();
            foreach (TutorialStep tutorialStep in tutorialSteps)
            {
                //tutorialStep.Cleanup(TutorialDashboard.TMP);
                if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
                //tutorialStep.chapter = TutorialChapter;
                tutorialStep.OnDone.AddListener(Next);
            }

        }

        public void Next()
        {
            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);

            // clean up the current step
            //tutorialSteps[_currStep].Cleanup(TutorialDashboard.TMP);
            if (tutorialSteps[_currStep].AllowActiveFalse == true) tutorialSteps[_currStep].gameObject.SetActive(false);

            // set up the next step
            _currStep += 1;
            if (_currStep < tutorialSteps.Length)
            {
                tutorialSteps[_currStep].gameObject.SetActive(true);
                //tutorialSteps[_currStep].Setup(TutorialDashboard.TMP);
            }
            else
            {
                End();
            }
        }

        public void Skip()
        {
            _skipped = true;
            End();
        }

        public void End()
        {
            foreach (TutorialStep tutorialStep in tutorialSteps)
            {
                //tutorialStep.Cleanup(TutorialDashboard.TMP);
                if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
            }

            _teleportController.enabled = true;
            if (moveToSpawn && _skipped) cameraRig.transform.position = spawnPoint.transform.position;          // only move to spawn point if skipping, otherwise player will already be on main island
            // _audioSource.Play();
            DashboardController.Instance.UpdateDashboard(Dashboard.Dashboard.DashboardType.Home);
        }

        private void OnDisable()
        {
            foreach (TutorialStep tutorialStep in tutorialSteps)
            {
                //tutorialStep.Cleanup(TutorialDashboard.TMP);
                tutorialStep.OnDone.RemoveListener(Next);
            }
            
            //TutorialDashboard.Cleanup();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        public TextMeshPro TMP;
        public TutorialStep[] tutorialSteps;


        public Button skipButton;
        public GameObject cameraRig;
        public GameObject spawnPoint;
        [Tooltip("Whether to move the player to the spawn point when skipping tutorial")]
        public bool moveToSpawn;

        private TeleportController _teleportController;
        private HapticsController _hapticsController;

        private int _currStep;
        private bool _skipped = false;

        [HideInInspector] public UnityEvent tutorialEnd;


        private void OnEnable()
        {
            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            _currStep = 0;
            foreach (TutorialStep tutorialStep in tutorialSteps)
            {
                tutorialStep.Cleanup(TMP);
                if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
                tutorialStep.OnDone.AddListener(Next);
            }
            tutorialSteps[_currStep].gameObject.SetActive(true);
            tutorialSteps[_currStep].Setup(TMP);

            skipButton.gameObject.SetActive(true);
            skipButton.onClick.AddListener(Skip);

            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
        }

        public void Next()
        {
            tutorialSteps[_currStep].Cleanup(TMP);
            if (tutorialSteps[_currStep].AllowActiveFalse == true) tutorialSteps[_currStep].gameObject.SetActive(false);
            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);

            _currStep += 1;
            if (_currStep < tutorialSteps.Length)
            {
                tutorialSteps[_currStep].gameObject.SetActive(true);
                tutorialSteps[_currStep].Setup(TMP);
            }
            else
            {
                End();
            }
        }

        private void Skip()
        {
            _skipped = true;
            End();
        }

        private void End()
        {
            foreach (TutorialStep tutorialStep in tutorialSteps)
            {
                tutorialStep.Cleanup(TMP);
                if (tutorialStep.AllowActiveFalse == true) tutorialStep.gameObject.SetActive(false);
            }
            skipButton.gameObject.SetActive(false);
            skipButton.onClick.RemoveListener(End);

            _teleportController.enabled = true;
            tutorialEnd.Invoke();

            if (moveToSpawn && _skipped) cameraRig.transform.position = spawnPoint.transform.position;
        }

        private void OnDisable()
        {
            foreach (TutorialStep tutorialStep in tutorialSteps)
            {
                tutorialStep.Cleanup(TMP);
                tutorialStep.OnDone.RemoveListener(Next);
            }
            skipButton.gameObject.SetActive(false);
            skipButton.onClick.RemoveListener(Skip);
        }
    }
}

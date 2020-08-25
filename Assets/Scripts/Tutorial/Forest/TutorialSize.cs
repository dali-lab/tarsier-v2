using Anivision.Notebook;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Changes the player to the size of the animal.
    /// Invokes OnDone to move on the the next step once the player presses the button on the dashboard by their left hand.
    /// </summary>
    public class TutorialSize : TutorialStep
    {
        public Button startButton;
        public GameObject RTriggerHighlight;
        public ScaleController scaleController;
        
        private TeleportController _teleportController;
        private HapticsController _hapticsController;
        private AudioSource _audioSource;
        private bool _stepDone = false;


        public override void Setup(TextMeshPro TMP)
        {
            _teleportController = TeleportController.Instance;
            if (_teleportController == null) throw new System.Exception("Must have a teleport controller in the scene");

            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            _audioSource = gameObject.GetComponent<AudioSource>();
            if (_audioSource == null) UnityEngine.Debug.LogError("Trying to access the audio source on this object, but there is none.");

            //TMP.text = dashboardText;

            // turn on the relevant tutorial items
            RTriggerHighlight.SetActive(true);
            startButton.gameObject.SetActive(true);

            startButton.onClick.AddListener(StartScaleChange);

            _teleportController.enabled = false;                // turn off ability to teleport
            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
            _audioSource.Play();
        }

        private void StartScaleChange()
        {
            scaleController.ScaleDone.AddListener(DoneScaling);
            scaleController.StartScaleChange();
        }

        private void DoneScaling()
        {
            _stepDone = true;
            scaleController.ScaleDone.RemoveListener(DoneScaling);
        }

        private void Update()
        {
            if (_stepDone && !_audioSource.isPlaying)           // if player had exited trigger and voiceover is done, move on
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup(TextMeshPro TMP)
        {
            TMP.text = "";
            RTriggerHighlight.SetActive(false);

            startButton.GetComponent<Button>().onClick.RemoveListener(StartScaleChange);
            startButton.gameObject.SetActive(false);
        }
    }
}

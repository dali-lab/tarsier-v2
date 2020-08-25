using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    /// <summary>
    /// Teaches the player how to grab objects in the environment
    /// Invokes OnDone to move on the the next step once the player grabs a cube and places it at the end point.
    /// </summary>
    public class LobbyGrab : TutorialStep
    {
        public GameObject cube;
        public GameObject RGripHighlight;

        private HapticsController _hapticsController;
        

        public override void Setup(TextMeshPro TMP)
        {
            _hapticsController = HapticsController.Instance;
            if (_hapticsController == null) throw new System.Exception("Must have a haptics controller in the scene");

            //TMP.text = dashboardText;

            // turn on the relevant tutorial items
            cube.SetActive(true);
            RGripHighlight.SetActive(true);

            _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.RTouch);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == cube)
            {
                _hapticsController.Haptics(1, 0.5f, 0.25f, OVRInput.Controller.RTouch);
                OnDone.Invoke();
            }
        }

        public override void Cleanup(TextMeshPro TMP)
        {
            TMP.text = "";
            cube.SetActive(false);
            RGripHighlight.SetActive(false);
        }
    }
}

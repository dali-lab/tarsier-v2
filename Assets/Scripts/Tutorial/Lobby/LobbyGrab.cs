using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    public class LobbyGrab : TutorialStep
    {
        public GameObject cube;
        public GameObject RGripHighlight;

        private HapticsController _hapticsController;

        public override void Setup(TextMeshPro TMP)
        {
            _hapticsController = HapticsController.Instance;

            TMP.text = dashboardText;
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

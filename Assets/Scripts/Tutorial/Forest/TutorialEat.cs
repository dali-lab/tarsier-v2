using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.Core;
using Anivision.PlayerInteraction;

namespace Anivision.Tutorial
{
    public class TutorialEat : TutorialStep
    {
        public GameObject gripHighlightRing;
        public HeadsetCollide headsetCollide;

        private InputManager _inputManager;


        public override void Setup(TextMeshPro TMP)
        {
            _inputManager = InputManager.Instance;
            if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");

            TMP.text = dashboardText;
            gripHighlightRing.SetActive(true);

            headsetCollide.onCollide.AddListener(Done);
        }

        private void Done()
        {
            OnDone.Invoke();
            gripHighlightRing.SetActive(false);
        }

        public override void Cleanup(TextMeshPro TMP)
        {
            TMP.text = "";
            //gripHighlightRing.SetActive(false);

            headsetCollide.onCollide.RemoveListener(Done);
        }
    }
}

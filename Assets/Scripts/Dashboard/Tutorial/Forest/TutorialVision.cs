using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.Core;

namespace Anivision.Tutorial
{
    public class TutorialVision : TutorialStep
    {
        public GameObject aHighlightRing;
        private InputManager _inputManager;


        public override void Setup(TextMeshPro TMP)
        {
            _inputManager = InputManager.Instance;

            if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");
            else
            {
                TMP.text = dashboardText;
                aHighlightRing.SetActive(true);
            }
        }

        private void Update()
        {
            if (_inputManager.IsButtonPressed(InputManager.Button.A)) // and voiceover is done
            {
                OnDone.Invoke();
            }
        }

        public override void Cleanup(TextMeshPro TMP)
        {
            TMP.text = "";
            aHighlightRing.SetActive(false);
        }
    }
}

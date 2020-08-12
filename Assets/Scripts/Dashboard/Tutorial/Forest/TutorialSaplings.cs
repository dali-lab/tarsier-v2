using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.Core;

public class TutorialSaplings : TutorialStep
{
    public GameObject triggerHighlightRing;

    private InputManager _inputManager;


    public override void Setup(TextMeshPro TMP)
    {
        _inputManager = InputManager.Instance;

        if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");
        else
        {
            TMP.text = dashboardText;
            triggerHighlightRing.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            OnDone.Invoke();
        }
    }

    public override void Cleanup(TextMeshPro TMP)
    {
        TMP.text = "";
        triggerHighlightRing.SetActive(false);
    }
}

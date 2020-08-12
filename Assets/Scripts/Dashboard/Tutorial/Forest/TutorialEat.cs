using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.Core;

public class TutorialEat : TutorialStep
{
    public GameObject gripHighlightRing;
    public GameObject centerEye;

    private InputManager _inputManager;


    public override void Setup(TextMeshPro TMP)
    {
        _inputManager = InputManager.Instance;

        if (_inputManager == null) throw new System.Exception("Must have an input manager script in the scene");
        else
        {
            TMP.text = dashboardText;
            gripHighlightRing.SetActive(true);
            centerEye.GetComponent<headsetCollide>().onCollide.AddListener(Eat);
        }
    }

    private void Eat()
    {
        OnDone.Invoke();
    }

    public override void Cleanup(TextMeshPro TMP)
    {
        TMP.text = "";
        gripHighlightRing.SetActive(false);
    }
}

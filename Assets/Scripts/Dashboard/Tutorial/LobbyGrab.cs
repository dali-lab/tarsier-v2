using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        _hapticsController.Haptics(1, 0.5f, 1, OVRInput.Controller.LTouch);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube")
        {
            _hapticsController.Haptics(1, 0.5f, 0.25f, OVRInput.Controller.LTouch);
            //OVRInput.SetControllerVibration(0.1f, 0.5f, OVRInput.Controller.RTouch);
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

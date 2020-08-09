using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticsController : MonoBehaviour
{
    private static HapticsController _hapticsController;
    //singleton instance
    public static HapticsController Instance
    {
        get
        {
            if (!_hapticsController) _hapticsController = FindObjectOfType(typeof(HapticsController)) as HapticsController;
            return _hapticsController;
        }
    }

    public void Haptics(float frequency, float amplitude, float duration, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        StartCoroutine(Duration(duration, controller));
    }
    IEnumerator Duration(float duration, OVRInput.Controller controller)
    {
        yield return new WaitForSecondsRealtime(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}

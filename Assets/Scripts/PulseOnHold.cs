using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PulseOnHold : VRTK_InteractableObject
{
    public float pulseInterval = 1f;
    public float pulseDuration = 1f;
    public float pulseStrength = 1f;
    public AudioClip hapticAudioClip;

    private VRTK_ControllerReference controllerReference;
    private IEnumerator pulseRoutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DoPulsing()
    {
        while (gameObject.activeSelf)
        {
            //VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, pulseStrength, pulseDuration, 0.01f);
            //OVRInput.SetControllerVibration(pulseDuration, pulseStrength, OVRInput.Controller.RTouch);
            OVRHapticsClip hapticsClip = new OVRHapticsClip(hapticAudioClip);
            OVRHaptics.RightChannel.Preempt(hapticsClip);
            yield return new WaitForSeconds(pulseInterval);
        }
    }
    private void OnEnable() // 1
    {
        base.OnEnable();
        pulseRoutine = DoPulsing();
    }

    public override void Grabbed(VRTK_InteractGrab grabbingObject) // 2
    {
        base.Grabbed(grabbingObject);
        controllerReference =
            VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
        StartCoroutine(pulseRoutine);
    }

    public override void Ungrabbed(VRTK_InteractGrab grabbingObject) // 3
    {
        base.Ungrabbed(grabbingObject);
        controllerReference = null;
        StopCoroutine(pulseRoutine);
    }
}

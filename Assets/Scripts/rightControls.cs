using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class rightControls : MonoBehaviour
{
    public Material[] materials;
    public AudioClip humanKatydidAudio;
    public AudioClip tarsierKatydidAudio;
    //public AudioSource audioSource;

    public Light directionalLight;

    private VRTK_ControllerEvents controllerEvents;

    private bool tarsierVision = false;

    private void OnEnable()
    {
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        if (controllerEvents == null)
        {
            VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
            return;
        }

        controllerEvents.ButtonOnePressed += DoButtonOnePressed;
    }

    private void OnDisable()
    {
        if (controllerEvents != null)
        {
            controllerEvents.ButtonOnePressed -= DoButtonOnePressed;
        }
    }

    private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
    {
        string debugString = "Controller on index '" + index + "' " + button + " has been " + action
                             + " with a pressure of " + e.buttonPressure + " / Primary Touchpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)" + " / Secondary Touchpad axis at: " + e.touchpadTwoAxis + " (" + e.touchpadTwoAngle + " degrees)";
        VRTK_Logger.Info(debugString);
    }

    private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
    {
        tarsierVision = !tarsierVision;
        // material changes
        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].GetFloat("_ColorblindOn") == 1.0f)
            {
                materials[i].SetFloat("_ColorblindOn", 0.0f);
            }
            else
            {
                materials[i].SetFloat("_ColorblindOn", 1.0f);

            }
        }

        if (tarsierVision)
        {
            directionalLight.intensity = 0.25f;
        } else
        {
            directionalLight.intensity = 0.05f;
        }

        // sound changes
        GameObject[] katydids;
        katydids = GameObject.FindGameObjectsWithTag("edible");
        foreach (GameObject katydid in katydids)
        {
            AudioSource katydidAudio = katydid.GetComponent<AudioSource>();
            // switch to tarsier katydid sound
            if (katydidAudio.clip == humanKatydidAudio)
            {
                
                if (katydidAudio.isPlaying)
                {
                    katydidAudio.Stop();
                }
                katydidAudio.loop = true;
                katydidAudio.clip = tarsierKatydidAudio;
                katydidAudio.Play();
            }

            // switch to human katydid sound
            else if (katydidAudio.clip == tarsierKatydidAudio)
            {
                if (katydidAudio.isPlaying)
                {
                    katydidAudio.Stop();
                }
                katydidAudio.loop = true;
                katydidAudio.clip = humanKatydidAudio;
                katydidAudio.Play();
            }
        }

    }
}
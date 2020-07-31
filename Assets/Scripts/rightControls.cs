using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

public class rightControls : MonoBehaviour
{
    public Material[] materials;
    public AudioClip humanKatydidAudio;
    public AudioClip tarsierKatydidAudio;
    //public AudioSource audioSource;

    public Light directionalLight;
    private InputManager _inputManager;

    private bool tarsierVision = false;

    void OnEnable()
    {
        //Get singleton instance of input manager
        _inputManager = InputManager.Instance;

        if (_inputManager == null) throw new System.Exception("Must have Input Manager script in scene");
        if (_inputManager != null) _inputManager.AttachInputHandler(DoButtonOnePressed, InputManager.InputState.ON_PRESS, InputManager.Button.A);

    }

    private void OnDisable()
    {
        if (_inputManager != null)
        {
            _inputManager.DetachInputHandler(DoButtonOnePressed, InputManager.InputState.ON_PRESS, InputManager.Button.A);
        }
        
    }

    

    private void DoButtonOnePressed()
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
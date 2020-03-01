using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// This script controls the playing of the audio depending on the static humanVision boolean
/// </summary>

public class KatydidSoundManager : MonoBehaviour {

    public AudioClip humanKatydidAudio;
    public AudioClip tarsierKatydidAudio;
    public AudioSource audioSource;
    public GameObject[] katydids;
    private VRTK_ControllerEvents controllerEvents;

    private void Start()
    {
        controllerEvents = GetComponent<VRTK_ControllerEvents>();

        if (humanKatydidAudio == null || tarsierKatydidAudio == null)
        {
            string error = "";
            if (tarsierKatydidAudio == null)
                error = "The Tarsier Katydid Audio Clip has not been initialized in the inspector, please do this now. ";
            if (humanKatydidAudio == null)
                error += "The Human Katydid Audio Clip has not been initialized in the inspector, please do this now. ";
            Debug.LogError(error);
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from this gameobject.");
        }

    }

    public void PlayHumanAudioClip()
    {

            for (int i = 0; i < katydids.Length; i++)
            {
                AudioSource katydidAudio = katydids[i].GetComponent<AudioSource>();
                if (katydidAudio.clip != humanKatydidAudio)
                {
                    if (katydidAudio.isPlaying)
                    {
                        katydidAudio.Stop();
                    }

                    katydidAudio.loop = true;
                    katydidAudio.clip = humanKatydidAudio;
                    Debug.Log("human clip: " + katydidAudio.clip.name);
                    katydidAudio.Play();
                }
            }       

    }

    public void PlayTarsierAudioClip()
    {

            for (int i = 0; i < katydids.Length; i++)
            {
                if (katydids[i] == null)
                {
                    Debug.Log("KATYDID IS NULL");
                }
                AudioSource katydidAudio = katydids[i].GetComponent<AudioSource>();
                if (katydidAudio.clip != tarsierKatydidAudio)
                {
                    if (katydidAudio.isPlaying)
                    {
                        katydidAudio.Stop();
                    }

                    katydidAudio.loop = true;
                    katydidAudio.clip = tarsierKatydidAudio;
                    Debug.Log("human clip: " + katydidAudio.clip.name);
                    katydidAudio.Play();
                }
            }
    }

    // Update is called once per frame
    void Update()
    {
        //if (VRTK.Controller_Menu_Popup.humanVision)
        //{
        //    PlayHumanAudioClip();
        //}
        //else
        {
            PlayTarsierAudioClip();
        }
    }
}


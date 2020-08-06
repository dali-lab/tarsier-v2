using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

public class NectarUI : MonoBehaviour
{
    public GameObject player;
    public GameObject startSpawn;           // location at the hive
    public GameObject nectarUISlider;       // the progress bar
    public HeadsetFade headsetFade;
    public float fadeSpeed;

    private float progress;
    private Vector3 maxScale;
    private Vector3 startLocation;


    void Start()
    {
        startLocation = startSpawn.transform.position;

        progress = 1.0f;
        maxScale = nectarUISlider.transform.localScale;                             // sets nectar bar to 0 on start, auto updated in updateHealth IEnumerator
        Vector3 zeroProgress = new Vector3(maxScale.x, 0.0f, maxScale.z);
        nectarUISlider.transform.localScale = zeroProgress;
        StartCoroutine(UpdateHealth());
    }

    void OnEnable()
    {
        headsetFade.OnFadeEnd += OnFadeEnd;
    }

    void OnDisable()
    {
        headsetFade.OnFadeEnd -= OnFadeEnd;
    }

    private IEnumerator UpdateHealth()                                              // health decreases over time
    {
        while ((progress > 0f))
        {
            yield return new WaitForSeconds(0.5f);
            progress -= 0.005f;                                                     // 100 seconds total
            nectarUISlider.transform.localScale = new Vector3(maxScale.x, maxScale.y * progress, maxScale.z);
        }
        BackToHive();             
    }

    public float AddHealth(float health)                    // function to increase health when picking up nectar globs
    {
        progress += health;
        if (progress > 1.0f)
        {
            progress = 1.0f;
        }
        return progress;
    }

    // fade to black and unfade for transition
    private void BackToHive()                        
    {
        headsetFade.StartFade(fadeSpeed);
    }

    private void OnFadeEnd()
    {
        headsetFade.StartUnfade(fadeSpeed);
        progress = 1.0f;
        player.transform.position = startLocation;
        StartCoroutine(UpdateHealth());
    }
}

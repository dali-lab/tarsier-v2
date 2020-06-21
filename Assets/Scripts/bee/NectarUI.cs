using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarUI : MonoBehaviour
{
    public GameObject player;
    public GameObject startSpawn;           // location at the hive
    public GameObject nectarUISlider;       // the progress bar
    public SceneFader sceneFader;

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
        StartCoroutine(updateHealth());
    }

    private IEnumerator updateHealth()                                              // health decreases over time
    {
        while ((progress > 0f))
        {
            yield return new WaitForSeconds(0.5f);
            progress -= 0.005f;                                                     // 100 seconds total
            nectarUISlider.transform.localScale = new Vector3(maxScale.x, maxScale.y * progress, maxScale.z);
        }
        StartCoroutine(BackToHive());                
    }

    public float addHealth(float health)                    // function to increase health when picking up nectar globs
    {
        progress += health;
        if (progress > 1.0f)
        {
            progress = 1.0f;
        }
        return progress;
    }

    private IEnumerator BackToHive()                        // fade to black and unfade for transition
    {
        sceneFader.StartFade();
        yield return new WaitForSeconds(1);
        progress = 1.0f;
        player.transform.position = startLocation;
        StartCoroutine(updateHealth());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarUI : MonoBehaviour
{
    public GameObject nectarUISlider;
    private float progress;
    private Vector3 maxScale;
    private bool decrease = true;           // needs to be turned true upon leaving hive

    // Start is called before the first frame update
    void Start()
    {
        progress = 1.0f;
        maxScale = nectarUISlider.transform.localScale;
        Vector3 zeroProgress = new Vector3(maxScale.x, 0.0f, maxScale.z);
        nectarUISlider.transform.localScale = zeroProgress;
        StartCoroutine(updateHealth());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator updateHealth()
    {
        while (decrease)
        {
            yield return new WaitForSeconds(0.5f);
            if ((progress > 0f))
            {
                progress -= 0.005f;                             // 100 seconds total
                nectarUISlider.transform.localScale = new Vector3(maxScale.x, maxScale.y * progress, maxScale.z);
            }
            else
            {
                progress = 1.0f;
            }
        }
    }

    public float addHealth(float health)
    {
        progress += health;
        if (progress > 1.0f)
        {
            progress = 1.0f;
        }
        return progress;
    }
}

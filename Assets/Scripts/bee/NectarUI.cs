using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarUI : MonoBehaviour
{
    public GameObject nectarUISlider;
    private float progress;
    private Vector3 maxScale;

    // Start is called before the first frame update
    void Start()
    {
        progress = 0.0f;
        maxScale = nectarUISlider.transform.localScale;
        Vector3 zeroProgress = new Vector3(maxScale.x, 0.0f, maxScale.z);
        nectarUISlider.transform.localScale = zeroProgress;
    }

    // Update is called once per frame
    void Update()
    {
        if(progress < 1) {
            progress += 0.01f;
        } else {
            progress = 0.0f;
        }
        nectarUISlider.transform.localScale = new Vector3(maxScale.x, maxScale.y * progress, maxScale.z);
    }
}

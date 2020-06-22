using UnityEngine;
using System.Collections;
using System;

public class NectarGlob : MonoBehaviour 
{
    public GameObject nectarBar;                                                                        // nectar health bar on left hand
    public NectarShaderIntensityController controller;

    void Start()
    {
        if(controller != null) {
            controller.incrementGlob();
        }
    }

    void Update()
    {
        if (nectarBar != null){
            if (Vector3.Distance(transform.position, nectarBar.transform.position) <= 0.05)
            {
                onUse();
            }
        }
    }

    void onUse()
    {
        if(controller != null) {
            controller.globConsumed();
            Destroy(gameObject);
            nectarBar.GetComponent<NectarUI>().addHealth(0.1f);
        }
    }
}
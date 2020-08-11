using UnityEngine;
using System.Collections;
using System;
using Anivision.PlayerInteraction;

public class NectarGlob : MonoBehaviour 
{
    public GameObject nectarBar;                                                                        // nectar health bar on left hand
    public NectarShaderIntensityController controller;

    public Grabber Grabber;
    void Start()
    {
        if(controller != null) {
            controller.incrementGlob();
        }

        // Grabber = FindObjectOfType<Grabber>();
    }

    void Update()
    {
        // if (Vector3.Distance(transform.position, Grabber.transform.position) <= 0.05)
        // {
        //     onUse();
        // }
        // if (nectarBar != null){
        //     if (Vector3.Distance(transform.position, Grabber.transform.position) <= 0.05)
        //     {
        //         onUse();
        //     }
        // }
    }

    void onUse()
    {
        if(controller != null) {
            controller.globConsumed();
            Destroy(gameObject);
            // nectarBar.GetComponent<NectarUI>().AddHealth(0.1f);
        }
    }
}
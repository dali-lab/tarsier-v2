using UnityEngine;
using System.Collections;
using System;
using Anivision.PlayerInteraction;
using JetBrains.Annotations;

public class NectarGlob : MonoBehaviour 
{
    public NectarShaderIntensityController controller;

    private Grabber[] Grabbers;
    [CanBeNull] private Grabber RGrabber;
    [CanBeNull] private Grabber LGrabber;
    void Start()
    {
        if (controller != null) {
            controller.incrementGlob();
        }

        Grabbers = FindObjectsOfType<Grabber>();
        if (Grabbers.Length > 0)
        {
            RGrabber = Grabbers[0];

            if (Grabbers.Length > 1)
            {
                LGrabber = Grabbers[1];
            }
        }
        
    }

    void Update()
    {
        if ((Grabbers.Length > 0 && RGrabber.GrabbedObject == gameObject) || (Grabbers.Length > 1 && LGrabber.GrabbedObject == gameObject))
        {
            onUse();
        }
        
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
            gameObject.GetComponent<Grabbable>().EndGrab();
            gameObject.SetActive(false);
            // Destroy(gameObject);
            // nectarBar.GetComponent<NectarUI>().AddHealth(0.1f);
        }
    }
}
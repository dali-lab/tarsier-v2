using UnityEngine;
using System.Collections;
using System;

public class NectarGlob : MonoBehaviour 
{
    public GameObject nectarBar;                                                                        // nectar health bar on left hand
    private NectarShaderIntensityController controller;

    void Start()
    {
        controller = transform.root.GetComponent<NectarShaderIntensityController>();
        if(controller != null) {
            controller.incrementGlob();
        }
    }

    void Update()
    {
        NectarUI ui = GetComponent<NectarUI>();
        if(ui != null) {
            GameObject uiBar = ui.gameObject;
            if(Vector3.Distance(transform.position, uiBar.transform.position) <= 0.0005)
            {
                onUse();
                Destroy(gameObject);
                nectarBar.GetComponent<NectarUI>().addHealth(0.1f);
            }
        }
    }

    void onUse()
    {
        if(controller != null) {
            controller.globConsumed();
            Debug.Log("Cronch");
        }
    }
}
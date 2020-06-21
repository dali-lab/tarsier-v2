using UnityEngine;
using System.Collections;
using System;

public class NectarGlob : MonoBehaviour 
{
    public GameObject nectarBar;                                                                        // nectar health bar on left hand
    public NectarShaderIntensityController controller;
    //private NectarShaderIntensityController controller;

    void Start()
    {
        //controller = transform.root.GetComponent<NectarShaderIntensityController>();
        if(controller != null) {
            controller.incrementGlob();
        }
        Debug.Log("nectarglob running");
    }

    void Update()
    {
        //NectarUI ui = GetComponent<NectarUI>();
        //if(ui != null) {
        if (nectarBar != null){
            //GameObject uiBar = ui.gameObject;
            //if(Vector3.Distance(transform.position, uiBar.transform.position) <= 0.0005)
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
            Debug.Log("Cronch");
        }
    }
}
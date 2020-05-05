using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class fly : MonoBehaviour
{
    public GameObject centerEye;
    public GameObject LController;
    private VRTK_ControllerEvents LControllerEvents;



    private float thrust = .075f;
    private bool isFlying = false;

    private void OnEnable()
    {
        Debug.Log("starting");
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
    }
 
    private void Update()
    {
        if (LControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress))
        {
            isFlying = !isFlying;
            Debug.Log("flying: " + isFlying);
        }
        Fly();
    }
    private void Fly()
    {
        if (isFlying == true)
        {
            //Vector3 flyDir = LController.transform.position - centerEye.transform.position;
            //transform.position += flyDir.normalized * thrust;
            Vector3 flyDir = centerEye.transform.forward;
            transform.position += flyDir.normalized * thrust;
            //transform.position += centerEye.transform.position Rotate(0, increment, 0);
            //Debug.Log(transform.position);
        }
    }
}
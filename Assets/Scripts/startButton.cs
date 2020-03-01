using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class startButton : MonoBehaviour
{
    // an array allows the same trigger to cause multiple actions
    // "SerializeField" forces Unity to expose the private variable in editor
    private Light spotlightObject;
    public GameObject leftController;
    public GameObject rightController;
    public Animator anim;
    public bool pressing;

    void Start()
    {
      spotlightObject = GameObject.Find("Tarsier Spotlight").GetComponent(typeof(Light)) as Light;
    }

    void Update()
    {
      Transform leftT = leftController.GetComponent(typeof(Transform)) as Transform;
      Transform rightT = rightController.GetComponent(typeof(Transform)) as Transform;
      Transform buttonT = gameObject.GetComponent(typeof(Transform)) as Transform;

      VRTK_ControllerEvents leftControllerEvents = leftController.GetComponent<VRTK_ControllerEvents>();

      
      if(Vector3.Distance(buttonT.position, leftT.position) <= 1 && leftControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress)) 
      {
        if(pressing == false) {
          spotlightObject.enabled = !spotlightObject.enabled;
          anim.SetBool("Press", true);
          pressing = true;
        }
      }
      if(!leftControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress)) {
        pressing = false;
        anim.SetBool("Press", false);
      }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //   if(!pressing)
    //   {
    //     spotlightObject.enabled = !spotlightObject.enabled;
    //   }
    //   pressing = true;
    //   anim.SetBool("Press", true);
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //   anim.SetBool("Press", false);
    //   pressing = false;
    // }

}
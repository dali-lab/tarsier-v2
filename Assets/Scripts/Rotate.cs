using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Rotate : MonoBehaviour
{
    [Tooltip("The camera rig that will be rotated, most likely the OVRCameraRig")]
    public GameObject cameraRig;
    [Tooltip("The controller events script to base joystick moves on. Dragging in the left or right controller script alias should work")]
    public VRTK_ControllerEvents controllerEvents;
    [Tooltip("How many degrees to rotate by every time a rotation is triggered")]
    public float increment = 15;
    [Tooltip("How far you need to move the joystick by before it triggers a rotation. 0.0 means any joystick movement will trigger it, while 1.0 means no joystick movement will trigger it")]
    [Range(0, .99f)]
    public float deadZone = 0;
    [Tooltip("How long (in seconds) to wait between rotations if the joystick is continuously held to one side")]
    public float waitTime = .2f;

    private float wait = 0;

    void Update()
    {
        if (wait <= 0)
        {
            if (controllerEvents.touchpadTouched)
            {
                Vector2 axes = controllerEvents.GetTouchpadAxis();
                if (axes.x > deadZone)
                {
                    cameraRig.transform.Rotate(0, increment, 0);
                    wait = waitTime;
                }
                else if (axes.x < -deadZone)
                {
                    cameraRig.transform.Rotate(0, -increment, 0);
                    wait = waitTime;
                }
            }        
        }
        else
        {
            wait -= Time.deltaTime;
        }
        
    }
}

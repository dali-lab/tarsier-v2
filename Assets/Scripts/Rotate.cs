using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

public class Rotate : MonoBehaviour
{
    [Tooltip("The camera rig that will be rotated, most likely the OVRCameraRig")]
    public GameObject cameraRig;

    private InputManager _inputManager;
    public bool RightHand; //if true, then rotate using right joystick
    public bool LeftHand; //if true, then rotate using left joystick

    [Tooltip("How many degrees to rotate by every time a rotation is triggered")]
    public float increment = 15;
    [Tooltip("How far you need to move the joystick by before it triggers a rotation. 0.0 means any joystick movement will trigger it, while 1.0 means no joystick movement will trigger it")]
    [Range(0, .99f)]
    public float deadZone = 0;
    [Tooltip("How long (in seconds) to wait between rotations if the joystick is continuously held to one side")]
    public float waitTime = .2f;

    private float wait = 0;

    private void Start()
    {
        _inputManager = InputManager.Instance;

        if (_inputManager == null)
        {
            throw new System.Exception("Must have an InstanceManager script in the scene");
        }

        if (_inputManager != null)
        {
            if (RightHand)
            {
                _inputManager.OnRightJoystickMonitor += RotateCamera;
            }

            if (LeftHand)
            {
                _inputManager.OnLeftJoystickMonitor += RotateCamera;
            }
        }
    }

    void RotateCamera(Vector2 rotationAxis)
    {

        if (wait <= 0)
        {
            if (rotationAxis.x > deadZone)
            {
                cameraRig.transform.Rotate(0, increment, 0);
                wait = waitTime;
            }
            else if (rotationAxis.x < -deadZone)
            {
                cameraRig.transform.Rotate(0, -increment, 0);
                wait = waitTime;
            }

        } else
        {
            wait -= Time.deltaTime;
        }
        
    }

    private void OnDestroy()
    {
        if (_inputManager != null)
        {
            if (RightHand)
            {
                _inputManager.OnRightJoystickMonitor -= RotateCamera;
            }

            if (LeftHand)
            {
                _inputManager.OnLeftJoystickMonitor -= RotateCamera;
            }
        }
    }
}

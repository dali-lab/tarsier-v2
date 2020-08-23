using System;
using Anivision.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Anivision.PlayerInteraction
{
    /// <summary>
    /// Scales camera rig to the proper scale
    /// </summary>
    public class ScaleController : MonoBehaviour
    {
        [Tooltip("OVRCameraRig that needs to be scaled")]
        public Transform OVRCameriaRig;
        [Tooltip("The final scale of the camera, in world space")]
        public Vector3 scale;
        [Tooltip("A headset fader script to use to fade before scaling. Should be a unique instance used only in this script.")]
        public HeadsetFade headsetFade;
        [Tooltip("How quickly to fade the headset.")]
        public float headsetFadeSpeed = 1;
        [HideInInspector]
        public UnityEvent ScaleDone = new UnityEvent(); //called when scaling is finished

        // start headset fade and scale when fade is done, then unfade
        // invokes ScaleDone event when unfade is finished
        public void StartScaleChange()
        {
            if (GetSize() != scale)
            {
                headsetFade.FadeUnfadeCustomCallback(headsetFadeSpeed, null, ScaleCamera, null, ScaleFinished);
            }
        }

        //get current scale of the ovr camera rig, in world space
        public Vector3 GetSize()
        {
            Transform parent = null;
            if (OVRCameriaRig.parent)
            {
                parent = OVRCameriaRig.parent;
                OVRCameriaRig.parent = null;
            }

            Vector3 cameraScale = OVRCameriaRig.localScale;

            if (parent)
            {
                OVRCameriaRig.parent = parent;
            }

            return cameraScale;
        }

        // scales camera 
        // unparents it from parent first, if parent exists, so that scaling is done in world space
        private void ScaleCamera()
        {
            Transform parent = null;
            if (OVRCameriaRig.parent)
            {
                parent = OVRCameriaRig.parent;
                OVRCameriaRig.parent = null;
            }
            
            OVRCameriaRig.localScale = scale;
            
            if (parent)
            {
                OVRCameriaRig.parent = parent;
            }

        }

        private void ScaleFinished()
        {
            ScaleDone.Invoke();
        }
        
        private void OnDisable()
        {
            ScaleDone.RemoveAllListeners();
        }
    }
}


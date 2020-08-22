using System;
using Anivision.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Anivision.PlayerInteraction
{
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
        public UnityEvent ScaleDone = new UnityEvent();

        public void StartScaleChange()
        {
            if (GetSize() != scale)
            {
                headsetFade.FadeUnfadeCustomCallback(headsetFadeSpeed, null, ScaleCamera, null, ScaleFinished);
            }
        }

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


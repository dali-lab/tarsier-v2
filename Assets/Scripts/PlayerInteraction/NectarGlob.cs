using System;
using UnityEngine;
using Anivision.PlayerInteraction;
using Anivision.Vision;

namespace Anivision.PlayerInteraction
{
    public class NectarGlob : MonoBehaviour 
    {
        public NectarShaderIntensity controller;
        
        private HeadsetCollide _headsetCollide;
        private Grabber[] Grabbers;
        private Grabber RGrabber;
        private Grabber LGrabber;
        void Start()
        {
            if (controller != null) {
                controller.IncrementGlob();
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
            
            _headsetCollide = FindObjectOfType<HeadsetCollide>();
            if (_headsetCollide == null) throw new System.Exception("Must have a headset collide script in the scene");
            
            _headsetCollide.onCollide.AddListener(DestroyGlob);                 // listens for the onCollide event from the HeadsetCollide script
        
        }

        void DestroyGlob(Collider other)
        {
            // if the object being grabbed is this gameobject
            if ((Grabbers.Length > 0 && RGrabber.GrabbedObject == gameObject) || (Grabbers.Length > 1 && LGrabber.GrabbedObject == gameObject))
            {

                if (controller) {
                    controller.GlobConsumed();
                }
                    
                Destroy(gameObject);
                
                
            }
        }
    }
}

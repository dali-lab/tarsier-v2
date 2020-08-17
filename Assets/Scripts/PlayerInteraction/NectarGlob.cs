using UnityEngine;
using Anivision.PlayerInteraction;
using Anivision.Vision;

namespace Anivision.PlayerInteraction
{
    public class NectarGlob : MonoBehaviour 
    {
        public NectarShaderIntensity controller;

        private Grabber[] Grabbers;
        private Grabber RGrabber;
        private Grabber LGrabber;
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
        }

        void onUse()
        {
            if (controller != null) {
                controller.globConsumed();
                gameObject.SetActive(false);
           
            }
        }
    }
}

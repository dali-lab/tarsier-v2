using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.PlayerInteraction;

public class KatydidEat : MonoBehaviour
{
    public AudioSource katydidEatSound;
    private HeadsetCollide _headsetCollide;

    // Start is called before the first frame update
    void Start()
    {
        _headsetCollide.onCollide.AddListener(Eat);
    }

    private void Eat()
    {
        //katydidEatSound.Play();

        //// end the grab and destroy the gameobject being eaten
        //if (_Grabbers.Length > 0 && _RGrabber.GrabbedObject == _grabbedObject)
        //{
        //    Destroy(_grabbedObject);
        //    _RGrabber.gameObject.GetComponent<Grabbable>().EndGrab();
        //}
        //else if (_Grabbers.Length > 1 && _LGrabber.GrabbedObject == _grabbedObject)
        //{
        //    Destroy(_grabbedObject);
        //    _LGrabber.gameObject.GetComponent<Grabbable>().EndGrab();
        //}
    }
}

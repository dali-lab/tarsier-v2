using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.NotebookSystem;
using Anivision.PlayerInteraction;

public class Counter : MonoBehaviour
{
    public TextMeshPro count;
    public HeadsetCollide headsetCollide;

    private Chapter _chapter;
    private Grabber[] _Grabbers;
    private Grabber _RGrabber;
    private Grabber _LGrabber;
    private int _currCount;
    private string _countString;

    private void OnEnable()
    {
        _chapter = gameObject.GetComponent<Chapter>();
        if (_chapter == null) UnityEngine.Debug.LogError("Trying to access the chapter of this gameobject, but there is none.");

        _Grabbers = FindObjectsOfType<Grabber>();
        if (_Grabbers.Length > 0)
        {
            _RGrabber = _Grabbers[0];

            if (_Grabbers.Length > 1)
            {
                _LGrabber = _Grabbers[1];
            }
        }

        headsetCollide.onCollide.AddListener(Eat);
        _currCount = 0;
    }

    // Update the count of number eaten
    private void Eat(Collider other)
    {
        // if the object being grabbed is the object that the headset is colliding with
        if ((_Grabbers.Length > 0 && _RGrabber.GrabbedObject == other.gameObject) || (_Grabbers.Length > 1 && _LGrabber.GrabbedObject == other.gameObject))
        {
            if (other.gameObject.tag == "edible")           // if the object the headset is colliding with is tagged as edible
            {
                _currCount += 1;
                _countString = _currCount + "/10";
                _chapter.ChangeText(count, _countString);
            }
        }   
    }

    private void OnDisable()
    {
        headsetCollide.onCollide.RemoveListener(Eat);
    }

}

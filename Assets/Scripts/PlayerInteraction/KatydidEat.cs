using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.PlayerInteraction;

public class KatydidEat : MonoBehaviour
{
    /// <summary>
    /// Listens for the UnityEvent onCollide from the HeadsetCollide script on the center eye.
    /// If the headset collides with the object being grabbed and the object is a katydid (tagged edible),
    /// Play the katydid cronch sound and destroy the katydid.
    /// </summary>
    public AudioClip katydidEatSound;

    private AudioSource _audioSource;
    private HeadsetCollide _headsetCollide;
    private Grabber[] _Grabbers;
    private Grabber _RGrabber;
    private Grabber _LGrabber;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        if (_audioSource == null) throw new System.Exception("Must have an audio source on this gameobject");

        _headsetCollide = FindObjectOfType<HeadsetCollide>();
        if (_headsetCollide == null) throw new System.Exception("Must have a headset collide script in the scene");

        _Grabbers = FindObjectsOfType<Grabber>();
        if (_Grabbers.Length > 0)
        {
            _RGrabber = _Grabbers[0];

            if (_Grabbers.Length > 1)
            {
                _LGrabber = _Grabbers[1];
            }
        }

        _headsetCollide.onCollide.AddListener(Eat);                 // listens for the onCollide event from the HeadsetCollide script
    }

    private void Eat(Collider other)
    {
        // if the object being grabbed is this gameobject
        if ((_Grabbers.Length > 0 && _RGrabber.GrabbedObject == gameObject) || (_Grabbers.Length > 1 && _LGrabber.GrabbedObject == gameObject))
        {
            if (other.gameObject.tag == "edible")                   // if the object the headset is colliding with is tagged as edible
            {
                if (_audioSource.isPlaying)                         // stop the current katydid noise
                {
                    _audioSource.Stop();
                }
                _audioSource.clip = katydidEatSound;                // swap it out for the cronch sound
                _audioSource.volume = 0.3f;                         // set volume to quieter
                _audioSource.loop = false;                          // play cronch only once
                _audioSource.Play();                                // play the cronch

                // destroy the gameobject only after the sound finishes playing
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(WaitForAudio(katydidEatSound.length));
            }
        }
    }

    IEnumerator WaitForAudio(float time)
    {
        yield return new WaitForSecondsRealtime(time);              // wait until clip finishes playing
        Destroy(gameObject);                                        // destroy the bug
    }
}

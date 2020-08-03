using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class headsetCollide : MonoBehaviour
{
    public AudioSource bugSound;

    private void Start()
    {
        bugSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "edible")                      // checks for objects tagged as katydid, if yes then delete katydid
        {
            bugSound.Play();                                            // play eat sound
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "goggles")
        {

            Destroy(other.gameObject);
        }
    }
}

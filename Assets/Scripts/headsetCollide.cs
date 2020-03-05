using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class headsetCollide : MonoBehaviour
{
    public AudioSource bugSound;
    public Material coolMaterial;

    private void Start()
    {
        bugSound = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "edible")                      // checks for objects tagged as katydid, if yes then delete katydid
        {
            bugSound.Play();                                            // play eat sound
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "goggles")
        {

            Destroy(collision.gameObject);
        }
    }
}

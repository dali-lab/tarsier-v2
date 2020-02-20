using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bugEat : MonoBehaviour
{
    public AudioSource bugSound;

    private void Start()
    {
        bugSound = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "katydid")                      // checks for objects tagged as katydid, if yes then delete katydid
        {
            bugSound.Play();                                            // play eat sound
            Destroy(collision.gameObject);
        }
    }
}

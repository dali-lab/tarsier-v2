using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class headsetCollide : MonoBehaviour
{
    public AudioSource bugSound;
    [HideInInspector] public UnityEvent onCollide;

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
            onCollide.Invoke();
        }
        if (other.gameObject.tag == "goggles")
        {
            Destroy(other.gameObject);
        }
        
    }
}

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
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "katydid")
        {
            bugSound.Play();
            Destroy(collision.gameObject);
        }
    }
}

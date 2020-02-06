using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour
{
    public Animator anim;

    // Use this for initialization
    void Start()
    {


    }

    void Activate()
    {

        anim.SetBool("Press", true);

    }

    void Deactivate()
    {

        anim.SetBool("Press", false);

    }
}
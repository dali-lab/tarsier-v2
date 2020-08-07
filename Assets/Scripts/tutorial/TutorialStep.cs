using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class TutorialStep : MonoBehaviour
{
    public TextMeshPro TMP;
    public UnityEvent OnDone = new UnityEvent();
    

    public abstract void Setup();
    public abstract void Cleanup();

    private void OnEnable()
    {
        Setup();
    }

    private void OnDisable()
    {
        Cleanup();
    }

}

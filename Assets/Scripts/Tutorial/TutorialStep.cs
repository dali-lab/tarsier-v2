using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class TutorialStep : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnDone;
    //public Chapter chapter;
    //public Page page;
    public bool AllowActiveFalse = true;
    public virtual void Setup(TextMeshPro TMP)
    {
        gameObject.SetActive(true);
        //chapter.presentPage(page);
    }
    public virtual void Cleanup(TextMeshPro TMP)
    {
        //chapter.ResetSection();
        if (AllowActiveFalse) gameObject.SetActive(false);
    }
}

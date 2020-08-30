using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Anivision.NotebookSystem;

public abstract class TutorialStep : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnDone;
    [HideInInspector] public Chapter chapter;
    public Page page;
    public bool AllowActiveFalse = true;


    public virtual void Setup()
    {
        gameObject.SetActive(true);
        chapter.PresentPage(page);
    }
    public virtual void Cleanup()
    {
        page.Cleanup();
        if (AllowActiveFalse) gameObject.SetActive(false);
    }
}

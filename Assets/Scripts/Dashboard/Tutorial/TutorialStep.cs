using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class TutorialStep : MonoBehaviour
{
    [TextArea(3, 10)] public string dashboardText;
    [HideInInspector] public UnityEvent OnDone;
    public bool AllowActiveFalse = true;
    public abstract void Setup(TextMeshPro TMP);
    public abstract void Cleanup(TextMeshPro TMP);
}

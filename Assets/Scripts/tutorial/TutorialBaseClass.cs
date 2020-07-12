using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBaseClass : MonoBehaviour
{

    public enum Tag { BUTTON1, BUTTON2, TRIGGER, GRIP }

    public enum Hand { LEFT, RIGHT }

    public enum HashKeys { LEFT_BUTTON1, LEFT_BUTTON2, RIGHT_BUTTON1, RIGHT_BUTTON2, LEFT_TRIGGER, RIGHT_TRIGGER, LEFT_GRIP, RIGHT_GRIP }

    protected bool isDone = false;

    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract void Disable();

    public virtual bool IsDone()
    {
        return isDone;
    }

}

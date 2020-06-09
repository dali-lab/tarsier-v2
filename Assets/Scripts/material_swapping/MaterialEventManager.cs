using UnityEngine;
using System.Collections;
using VRTK;
public class MaterialEventManager : MonoBehaviour 
{
    public delegate void MaterialSwapAction(bool uVmode);
    public static event MaterialSwapAction OnMaterialSwap;

    public GameObject rightControllerAlias;

    public static bool isUV = false;
    private static bool locked = false;

    void Start()
    {
        rightControllerAlias.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(DoButtonTwoPress);
        rightControllerAlias.GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoButtonTwoRelease);
    }

    void DoButtonTwoPress(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("we got a press here");
        SendMaterialSwapEvent();
        locked = true;
    }

    void DoButtonTwoRelease(object sender, ControllerInteractionEventArgs e)
    {
        locked = false;
    }
    public static void SendMaterialSwapEvent()
    {
        if(locked) {
            return;
        }
        isUV = !isUV;
        if(OnMaterialSwap != null)
            OnMaterialSwap(isUV);
    }

    public static void LockPress() {
        locked = true;
    }

    public static void UnlockPress() {
        locked = false;
    }
}


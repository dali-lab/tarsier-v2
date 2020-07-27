using UnityEngine;
using System.Collections;
using Anivision.Core;

public class MaterialEventManager : MonoBehaviour 
{
    public delegate void MaterialSwapAction(bool uVmode);
    public static event MaterialSwapAction OnMaterialSwap;

    public GameObject rightControllerAlias;

    public static bool isUV = false;
    private static bool locked = false;

    private InputManager _inputManager;

    void Start()
    {
        _inputManager = InputManager.Instance;

        if (_inputManager == null)
        {
            throw new System.Exception("There must be an input manager script in the scene");
        }

        if (_inputManager != null)
        {
            _inputManager.AttachInputHandler(StartMaterialSwap, InputManager.InputState.ON_PRESS, InputManager.Button.B);
            _inputManager.AttachInputHandler(UnlockMaterialSwap, InputManager.InputState.ON_RELEASE, InputManager.Button.B);
        }
           
       
    }

    void StartMaterialSwap()
    {
        SendMaterialSwapEvent();
        locked = true;
    }

    void UnlockMaterialSwap()
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

    private void OnDestroy()
    {
        if (_inputManager != null)
        {
            _inputManager.AttachInputHandler(StartMaterialSwap, InputManager.InputState.ON_PRESS, InputManager.Button.B);
            _inputManager.AttachInputHandler(UnlockMaterialSwap, InputManager.InputState.ON_RELEASE, InputManager.Button.B);
        }
    }
}


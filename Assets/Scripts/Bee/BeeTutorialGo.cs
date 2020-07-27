using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Anivision.Core;

public class BeeTutorialGo : MonoBehaviour
{
    public GameObject LController;
    public GameObject buttonXHighlight;                                                     // indicates which button to click
    public GameObject beeLeftControls;                                                      // turns on the instructions text so players can pull up an instruction panel
    public SceneFader sceneFader;

    private InputManager _inputManager;

    public void OnEnable()
    {
        _inputManager = InputManager.Instance;

        if (_inputManager == null)
        {
            throw new System.Exception("Must have an input manager script in the scene");
        }

        if (_inputManager != null) _inputManager.AttachInputHandler(StartSceneChange, InputManager.InputState.ON_PRESS, InputManager.Button.X);
        buttonXHighlight.SetActive(true);
    }

    private void StartSceneChange()    // turns of indicator of which button to press and the panel, turns on the instructions text
    {
        StartCoroutine(MoveToScene());
    }

    private IEnumerator MoveToScene()                        // fade to black and unfade for transition
    {
        sceneFader.StartFade();
        yield return new WaitForSeconds(1);
        buttonXHighlight.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        beeLeftControls.SetActive(true);
        SceneManager.LoadScene("BeeScene");
    }

    public void OnDisable()
    {
        if (_inputManager != null) _inputManager.DetachInputHandler(StartSceneChange, InputManager.InputState.ON_PRESS, InputManager.Button.X);
    }
}

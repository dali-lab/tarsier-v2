using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class BeeTutorialGo : MonoBehaviour
{
    public GameObject LController;
    public GameObject buttonXHighlight;                                                     // indicates which button to click
    public GameObject beeLeftControls;                                                      // turns on the instructions text so players can pull up an instruction panel
    public SceneFader sceneFader;

    private VRTK_ControllerEvents LControllerEvents;


    public void OnEnable()
    {
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
        LControllerEvents.ButtonOnePressed += DoLeftButtonOnePressed;                       // 'X' button on left controller
        buttonXHighlight.SetActive(true);
    }

    private void DoLeftButtonOnePressed(object sender, ControllerInteractionEventArgs e)    // turns of indicator of which button to press and the panel, turns on the instructions text
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
        LControllerEvents.ButtonOnePressed -= DoLeftButtonOnePressed;
    }
}

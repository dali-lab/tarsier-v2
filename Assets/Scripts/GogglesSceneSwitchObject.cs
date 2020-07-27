using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GogglesSceneSwitchObject : MonoBehaviour
{
    [Tooltip("Name of the scene to switch to when the gogles are put on")]
    public string sceneName = "ForestScene";
    [Tooltip("Game object used to determine the position of the headset")]
    public GameObject centerEyeAnchor;
    [Tooltip("How close the headset must be to the goggles before a scene switch is initiated")]
    public float threshold = .5f;
    [Tooltip("The scene fader object in the scene. Used to fade to blck before switching scenes. This component is necessary")]
    public SceneFader sceneFader;

    void Update()
    {
        // Get the distance between the headset and the goggles
        float dist = Vector3.Distance(gameObject.transform.position, centerEyeAnchor.transform.position);
        // If the distance is within the threshold, and a fade has not yet been initialized, initialize a fade
        if (dist <= threshold && !sceneFader.IsFading())
        {
            sceneFader.StartFade();
        }
    }

    void OnEnable()
    {
        // Add a callback so that when the fade finishes, the scene switches
        SceneFader.OnFadeComplete += SwitchScene;
    }

    void OnDisable()
    {
        SceneFader.OnFadeComplete -= SwitchScene;
    }

    // Function that switches to the scene with the name given by "sceneName"
    private void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class GogglesSceneSwitchObject : MonoBehaviour
{
    public string sceneName = "ForestScene";
    public VRTK_HeadsetCollision headsetCollision;
    public SceneFader sceneFader;

    void Update()
    {
        if (headsetCollision.IsColliding() && !sceneFader.IsFading())
        {
            Debug.Log("starting fade");
            sceneFader.StartFade();
        }
    }
    void OnEnable()
    {
        SceneFader.OnFadeComplete += SwitchScene;
    }

    void OnDisable()
    {
        SceneFader.OnFadeComplete -= SwitchScene;
    }

    private void SwitchScene()
    {
        Debug.Log("switching scenes");
        SceneManager.LoadScene(sceneName);
    }
}

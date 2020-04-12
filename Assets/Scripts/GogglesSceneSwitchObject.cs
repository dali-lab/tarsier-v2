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
        SceneManager.LoadScene(sceneName);
    }
}
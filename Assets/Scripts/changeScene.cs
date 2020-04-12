using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public string sceneName = "LobbyScene";
    public GameObject headset;
    public SceneFader sceneFader;
    public float requiredDistance = 1;

    private void OnEnable()
    {
        SceneFader.OnFadeComplete += SwitchScene;
    }

    private void OnDisable()
    {
        SceneFader.OnFadeComplete -= SwitchScene;
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, headset.transform.position) <= requiredDistance)
        {
            sceneFader.StartFade();
        }
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

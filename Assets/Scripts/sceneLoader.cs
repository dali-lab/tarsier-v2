using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class sceneLoader : MonoBehaviour
{
    public SceneAsset[] scenes;

    // Start is called before the first frame update
    void Start()
    {
    
        for (int i = 0; i < scenes.Length; i++)
        {
            if (Application.isEditor)
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    LoadSceneParameters parameters = new LoadSceneParameters();
                    parameters.loadSceneMode = LoadSceneMode.Additive;
                    SceneManager.UnloadSceneAsync(scenes[i].name);
                    EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/" + scenes[i].name + ".unity", parameters);
                }
                else
                {
                    EditorSceneManager.OpenScene("Assets/Scenes/" + scenes[i].name + ".unity", OpenSceneMode.Additive);
                }
            }

        }

    }
}

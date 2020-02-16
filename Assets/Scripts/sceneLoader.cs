using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class sceneLoader : MonoBehaviour
{
    public Object[] scenes;
    public string scenesFolder;

    // Start is called before the first frame update
    public void Start()
    {

        if (Application.isEditor)
        {
#if UNITY_EDITOR
            for (int i = 0; i < scenes.Length; i++)
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    LoadSceneParameters parameters = new LoadSceneParameters();
                    parameters.loadSceneMode = LoadSceneMode.Additive;
                    SceneManager.UnloadSceneAsync(scenes[i].name);
                    EditorSceneManager.LoadSceneInPlayMode("Assets/Scenes/" + scenesFolder + "/" + scenes[i].name + ".unity", parameters);
                }
                else
                {
                    EditorSceneManager.OpenScene("Assets/Scenes/" + scenesFolder + "/" + scenes[i].name + ".unity", OpenSceneMode.Additive);
                }
            }
#endif
        }
        else
        {
            for (int j = 0; j < scenes.Length; j++)
            {
                SceneManager.LoadSceneAsync(scenes[j].name, LoadSceneMode.Additive);
            }

        }
      
    }
}

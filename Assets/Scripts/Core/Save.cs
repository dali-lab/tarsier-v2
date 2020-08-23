using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Anivision.Core
{
    /// <summary>
    /// Script to save various game objects and information that should not be destroyed between scenes
    /// </summary>
    public class Save : MonoBehaviour
    {

        private static Save _saveManager;
        //singleton instance
        public static Save Instance { get
        {
            if (!_saveManager)
            {
                _saveManager = FindObjectOfType (typeof (Save)) as Save;

                if (!_saveManager)
                {
                    UnityEngine.Debug.LogError("There needs to be one active Save script on a GameObject in your scene.");
                }
            }

            return _saveManager;
        } } 
        
        public GameObject[] objectsToSave;
        private readonly HashSet<int> previousLevelBuildIndexes = new HashSet<int>(); // hashset to determine which scenes have been visited
        private bool addScene = true;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            foreach (GameObject g in objectsToSave)
            {
                DontDestroyOnLoad(g);
            }

            SceneManager.sceneUnloaded += LogPreviousScene;
        }
        private void LogPreviousScene(Scene scene)
        {
            if (addScene)
            {
                previousLevelBuildIndexes.Add(scene.buildIndex); // add scene to build index when scene is unloaded

            }
            else
            {
                addScene = true;
            }
        }

        // removes active scene from previously visited scenes
        public void RemoveSceneFromPreviouslyVisited(Scene scene)
        {
            previousLevelBuildIndexes.Remove(scene.buildIndex);
        }
        
        // removes active scene from previously visited scenes
        // this is helpful when you want to replay tutorials
        public void RemoveActiveSceneFromPreviouslyVisited()
        {
            previousLevelBuildIndexes.Remove(SceneManager.GetActiveScene().buildIndex);
            addScene = false;
        }

        //check if a scene has been previously visited
        public bool PreviouslyVisited(Scene scene)
        {
            return previousLevelBuildIndexes.Contains(scene.buildIndex); 
        }


    }
}


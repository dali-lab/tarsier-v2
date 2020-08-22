using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Anivision.Core
{
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
        private readonly HashSet<int> previousLevelBuildIndexes = new HashSet<int>();
        
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
            previousLevelBuildIndexes.Add(scene.buildIndex);
        }

        public bool PreviouslyVisited(Scene scene)
        {
            return previousLevelBuildIndexes.Contains(scene.buildIndex);
        }


    }
}


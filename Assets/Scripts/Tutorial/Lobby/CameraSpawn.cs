using System;
using System.Collections.Generic;
using Anivision.Core;
using Anivision.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Anivision.Tutorial
{
    
    /// <summary>
    /// Where to spawn camera depending on if the scene was already visited or not
    /// </summary>
    public class CameraSpawn : MonoBehaviour
    {
        public Transform finalSpawnPoint;
        private void Start()
        {
            if (Save.Instance.PreviouslyVisited(SceneManager.GetActiveScene()))
            {
                FindObjectOfType<OVRCameraRig>().gameObject.transform.position = finalSpawnPoint.position;
            }
        }
    }
}
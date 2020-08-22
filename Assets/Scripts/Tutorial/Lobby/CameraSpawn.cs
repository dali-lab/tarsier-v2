using System;
using System.Collections.Generic;
using Anivision.Core;
using Anivision.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Anivision.Tutorial
{
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
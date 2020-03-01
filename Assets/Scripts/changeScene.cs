using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "OVRCameraRig")                      // checks for objects tagged as katydid, if yes then delete katydid
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
}

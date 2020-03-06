using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider collision)
    {
            SceneManager.LoadScene("ForestScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warpSphereScript : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
      Debug.Log("collided");
      Debug.Log(other.tag);
      Debug.Log(other.name);
      if(true){
          SceneManager.LoadScene("ForestScene", LoadSceneMode.Single);
          Scene sceneToLoad = SceneManager.GetSceneByName("ForestScene");
          SceneManager.MoveGameObjectToScene(other.gameObject, sceneToLoad);
      }
    }
}

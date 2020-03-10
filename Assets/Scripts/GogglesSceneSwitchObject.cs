using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class GogglesSceneSwitchObject : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    public VRTK_HeadsetCollision headsetCollision;
    void Start()
    {
    }

    void Update()
    {
      Transform leftT = leftController.GetComponent(typeof(Transform)) as Transform;
      Transform rightT = rightController.GetComponent(typeof(Transform)) as Transform;
      Transform buttonT = gameObject.GetComponent(typeof(Transform)) as Transform;

      VRTK_ControllerEvents leftControllerEvents = leftController.GetComponent<VRTK_ControllerEvents>();
      if(headsetCollision.headsetColliding == true){
        Debug.Log("COLLIDED");
        SceneManager.LoadScene("ForestScene");
        //StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "ForestScene"));
      }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class warpSphereScript : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    void Start()
    {
    }

    void Update()
    {
      Transform leftT = leftController.GetComponent(typeof(Transform)) as Transform;
      Transform rightT = rightController.GetComponent(typeof(Transform)) as Transform;
      Transform buttonT = gameObject.GetComponent(typeof(Transform)) as Transform;

      VRTK_ControllerEvents leftControllerEvents = leftController.GetComponent<VRTK_ControllerEvents>();
      if(Vector3.Distance(buttonT.position, leftT.position) <= 1 && leftControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress)) 
      {
        SceneManager.LoadScene("TarsierMasterScene", LoadSceneMode.Single);
          // Scene sceneToLoad = SceneManager.GetSceneByName("TarsierMasterScene");
          // SceneManager.MoveGameObjectToScene(other.gameObject, sceneToLoad);
      }
    }
}

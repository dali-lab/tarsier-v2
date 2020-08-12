using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.SceneManagement;

public class ToLobby : MonoBehaviour
{
    public Button tempLobbyButton;
    public GameObject tempSceneObj;

    // Start is called before the first frame update
    void Start()
    {
        tempLobbyButton.onClick.AddListener(BackToLobby);
    }

    private void BackToLobby()
    {
        tempSceneObj.GetComponent<SceneSwitch>().StartTransition();
    }
}

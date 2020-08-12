using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Anivision.SceneManagement;

public class TempHomeDashboard : MonoBehaviour
{
    [TextArea(3, 10)] public string dashboardText;
    public TextMeshPro TMP;
    public GameObject toLobbyButton;
    public GameObject otherVisionsButton;


    private void OnEnable()
    {
        TMP.text = dashboardText;

        toLobbyButton.SetActive(true);
        toLobbyButton.GetComponent<Button>().onClick.AddListener(ToLobby);

        otherVisionsButton.SetActive(true);
        //otherVisionsButton.GetComponent<Button>().onClick.AddListener(ToVisionSelectDashboard);
    }

    private void ToLobby()
    {
        gameObject.GetComponent<SceneSwitch>().StartTransition();
    }

    private void ToVisionSelectDashboard()
    {
        
    }

    private void OnDisable()
    {
        TMP.text = "";
        toLobbyButton.GetComponent<Button>().onClick.RemoveListener(ToLobby);
        toLobbyButton.SetActive(false);
    }
}

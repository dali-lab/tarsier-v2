using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TutorialGameController : MonoBehaviour
{
    public GameObject[] tutorialObjects;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            tutorialObjects[i].SetActive(false);
        }
        BeeTutorialBaseClass.done += switchPanels;

        tutorialObjects[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void switchPanels()
    {
        tutorialObjects[currentIndex].SetActive(false);
        currentIndex += 1;
        if (currentIndex < tutorialObjects.Length)
        {
            tutorialObjects[currentIndex].SetActive(true);
        }
    }

    private void OnDestroy()
    {
        BeeTutorialBaseClass.done -= switchPanels;
    }
}

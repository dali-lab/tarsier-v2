﻿using System.Collections;
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
            tutorialObjects[i].GetComponent<BeeTutorialBaseClass>().Disable();
            tutorialObjects[i].SetActive(false);
        }

        tutorialObjects[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        BeeTutorialBaseClass script = tutorialObjects[currentIndex].GetComponent<BeeTutorialBaseClass>();

        if (script.IsDone() && currentIndex < tutorialObjects.Length - 1)
        {
            tutorialObjects[currentIndex].GetComponent<BeeTutorialBaseClass>().Disable();
            tutorialObjects[currentIndex].SetActive(false);
            currentIndex += 1;
            tutorialObjects[currentIndex].SetActive(true);
        }
    }
}

using System;
using UnityEngine;
using Anivision.Core;
using Anivision.Vision;
using TMPro;

public class VisionTracker : MonoBehaviour
{
    public TextMeshPro currentVisionTMP;
    private AnimalManager _animalManager;

    private void OnEnable()
    {
        _animalManager = AnimalManager.Instance;

        if (_animalManager == null)
        {
            throw new Exception("There must be an instance of the AnimalManager script in the scene");
        }
        _animalManager.VisionSwitch.AddListener(UpdateText);
    }

    private void UpdateText(VisionParameters visionParameters)
    {
        currentVisionTMP.text = _animalManager.currentVision.ToString().ToUpper();
    }
}

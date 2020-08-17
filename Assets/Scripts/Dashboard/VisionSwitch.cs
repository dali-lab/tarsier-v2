using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSwitch : MonoBehaviour
{
    private Button _button;

    private void OnEnable()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(SwitchToAnimal);
    }

    private void SwitchToAnimal()
    {
        //TODO
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SwitchToAnimal);
    }
}

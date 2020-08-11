using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public GameObject rightControllerModel;
    public GameObject selectorObject;

    public Color defaultControllerColor;
    public Color hoverControllerColor;
    public Color defaultSelectorColor;
    public Color hoverSelectorColor;

    private void OnEnable()
    {
        ToDefaultControllerColor();
        ToDefaultSelectorColor();
    }


    public void ToDefaultControllerColor()
    {
        rightControllerModel.GetComponent<Renderer>().material.SetColor("_BaseColor", defaultControllerColor);
    }
    public void ToHoverControllerColor()
    {
        rightControllerModel.GetComponent<Renderer>().material.SetColor("_BaseColor", hoverControllerColor);
    }

    public void ToDefaultSelectorColor()
    {
        selectorObject.GetComponent<Renderer>().material.color = defaultSelectorColor;
    }
    public void ToHoverSelectorColor()
    {
        selectorObject.GetComponent<Renderer>().material.color = hoverSelectorColor;
    }
}

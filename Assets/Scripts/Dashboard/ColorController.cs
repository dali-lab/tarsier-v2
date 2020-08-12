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

    private MaterialPropertyBlock _controllerPropBlock;
    private MaterialPropertyBlock _selectorPropBlock;
    private Renderer _controllerRenderer;
    private Renderer _selectorRenderer;

    private void OnEnable()
    {
        _controllerPropBlock = new MaterialPropertyBlock();
        _selectorPropBlock = new MaterialPropertyBlock();
        _controllerRenderer = rightControllerModel.GetComponent<Renderer>();
        _selectorRenderer = selectorObject.GetComponent<Renderer>();

        ToDefaultControllerColor();
        ToDefaultSelectorColor();
    }


    public void ToDefaultControllerColor()
    {
        _controllerRenderer.GetPropertyBlock(_controllerPropBlock);
        _controllerPropBlock.SetColor("_BaseColor", defaultControllerColor);
        _controllerRenderer.SetPropertyBlock(_controllerPropBlock);
    }
    public void ToHoverControllerColor()
    {
        _controllerRenderer.GetPropertyBlock(_controllerPropBlock);
        _controllerPropBlock.SetColor("_BaseColor", hoverControllerColor);
        _controllerRenderer.SetPropertyBlock(_controllerPropBlock);
    }

    public void ToDefaultSelectorColor()
    {
        _selectorRenderer.GetPropertyBlock(_selectorPropBlock);
        _controllerPropBlock.SetColor("_BaseColor", defaultSelectorColor);
        _selectorRenderer.SetPropertyBlock(_selectorPropBlock);
    }
    public void ToHoverSelectorColor()
    {
        _selectorRenderer.GetPropertyBlock(_selectorPropBlock);
        _controllerPropBlock.SetColor("_BaseColor", hoverSelectorColor);
        _selectorRenderer.SetPropertyBlock(_selectorPropBlock);
    }
}

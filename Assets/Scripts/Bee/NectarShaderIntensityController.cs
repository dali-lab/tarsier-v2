using UnityEngine;
using System.Collections;
using System;

public class NectarShaderIntensityController : MonoBehaviour 
{
    private int globcount = 0;

    private Renderer[] _renderers;
    private MaterialPropertyBlock _propBlock;

    void Start()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderers = GetComponentsInChildren<Renderer>();
    }

    public void incrementGlob()
    {
        globcount++;
        updateMaterial();
    }

    public void globConsumed()
    {
        globcount--;
        updateMaterial();
    }

    private void updateMaterial()
    {
        _propBlock.SetInt("_Intensity", globcount * 2 + 1);
        foreach(Renderer r in _renderers) {
            r.SetPropertyBlock(_propBlock);
        }
    }
}
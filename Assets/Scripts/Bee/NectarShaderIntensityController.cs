using UnityEngine;
using System.Collections;
using System;
using Anivision.Core;
using Anivision.Vision;

public class NectarShaderIntensityController : MaterialEffect
{
    public override VisionEffect Effect => VisionEffect.NectarIntensity;
    private float maxGlobs = 1;
    private float globcount = 0;

    private Renderer[] _renderers;
    private MaterialPropertyBlock _propBlock;

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderers = GetComponentsInChildren<Renderer>();
        UpdateMaterial();
    }

    public void IncrementGlob()
    {
        globcount++;
        if (globcount > maxGlobs)
        {
            maxGlobs = globcount;
        }

        UpdateMaterial();
    }

    public void GlobConsumed()
    {
        globcount--;
        UpdateMaterial();
    }

    private float GetIntensity()
    {
        return globcount / maxGlobs;
    }

    public override void ApplyEffect(MaterialPropertyBlock propBlock, int materialIndex, Renderer renderer,
        VisionParameters visionParameters)
    {
        propBlock.SetFloat("_Intensity", GetIntensity());
    }

    public override void RevertToOriginal(Renderer r)
    {
        _propBlock.Clear();
        r.SetPropertyBlock(_propBlock);
    }

    private void UpdateMaterial()
    {
        _propBlock.SetFloat("_Intensity", GetIntensity());
        foreach(Renderer r in _renderers) {
            r.SetPropertyBlock(_propBlock);
        }
    }
}
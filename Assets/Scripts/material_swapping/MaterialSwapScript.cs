using UnityEngine;
using System.Collections;
using System;

public class MaterialSwapScript : MonoBehaviour 
{
    public Material[] nUVmats;
    public Material[] UVmats;

    void OnEnable()
    {
        MaterialEventManager.OnMaterialSwap += SwapMaterial;
    }


    void OnDisable()
    {
        MaterialEventManager.OnMaterialSwap -= SwapMaterial;
    }

    int GetIndexOfMaterial(Material mat)
    {
        for(int i = 0; i < nUVmats.Length; i++) {
            if((nUVmats[i].name + " (Instance)").Equals(mat.name)) {
                return i;
            }
            if((UVmats[i].name + " (Instance)").Equals(mat.name)) {
                return i;
            }
        }
        return -1;
    }

    void SwapMaterial(bool uvMode)
    {
        SwapMaterialRecursive(gameObject.transform, uvMode);
    }

    void SwapMaterialRecursive(Transform t, bool uvMode)
    {
        GameObject currGameObject = t.gameObject;
        // if current gameobject has a renderer
        if(currGameObject.GetComponent<Renderer>() != null){
            // switch to the correct texture, if valid and switchable
            int index = GetIndexOfMaterial(currGameObject.GetComponent<Renderer>().material);
            if (index > -1)
            {
                if (uvMode)
                {
                    currGameObject.GetComponent<Renderer>().material = UVmats[index];
                }
                else
                {
                    currGameObject.GetComponent<Renderer>().material = nUVmats[index];
                }
            }
        }
        // recurse over children
        foreach( Transform child in t) {
            SwapMaterialRecursive(child, uvMode);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;
    public GameObject button;
    public Animator startButtonAnim;
    public Animator tarsierSpotlightAnim;
    public Animator ambientLightAnim;
    public Material skybox;
    private float _exposure = 0;
    private bool _startPressed = false;

    void Start()
    {
        RenderSettings.skybox.SetFloat("_Exposure", _exposure);
    }

    IEnumerator turnOnLights()
    {
        yield return new WaitForSeconds(0.5f);
        tarsierSpotlightAnim.SetBool("on", true);
        yield return new WaitForSeconds(3);
        ambientLightAnim.SetBool("on", true);
        yield return new WaitForSeconds(0.5f);
        while (_exposure < 1)                       // increase brightness of skybox
        {
            yield return new WaitForSeconds(0.0005f);
            brightenSkybox();
        }

    }
    private void brightenSkybox()
    {
            if (_exposure < 1)
            {
                _exposure += 0.005f;
                RenderSettings.skybox.SetFloat("_Exposure", _exposure);
            }
        
    }
    void Update()
    {
        Transform leftT = leftController.GetComponent(typeof(Transform)) as Transform;
        Transform rightT = rightController.GetComponent(typeof(Transform)) as Transform;
        Transform buttonT = button.GetComponent(typeof(Transform)) as Transform;

        if (Vector3.Distance(rightT.position, buttonT.position) <= .1 || Vector3.Distance(leftT.position, buttonT.position) <= .1)
        {
            if (_startPressed == false)
            {
                _startPressed = true;
                StartCoroutine(turnOnLights());
            }
            startButtonAnim.SetBool("Press", true);
        }
        else
        {
            startButtonAnim.SetBool("Press", false);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class fly : MonoBehaviour
{
    public GameObject centerEye;
    public GameObject flyParticles;
    public GameObject LController;
    public GameObject blackScreen;
    //public Material skybox;

    private VRTK_ControllerEvents LControllerEvents;
    //private float _exposure = 1;



    private float thrust = .075f;
    private bool isFlying = false;

    private void OnEnable()
    {
        Debug.Log("starting");
        LControllerEvents = LController.GetComponent<VRTK_ControllerEvents>();
        //RenderSettings.skybox.SetFloat("_Exposure", _exposure);
        flyParticles.SetActive(false);
        blackScreen.SetActive(false);
    }
 
    private void Update()
    {
        if (LControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonOnePress))
        {
            isFlying = !isFlying;
            StartCoroutine(movementTransition());
        }
        Fly();
    }
    private void Fly()
    {
        if (isFlying == true)
        {
            //Vector3 flyDir = LController.transform.position - centerEye.transform.position;
            //transform.position += flyDir.normalized * thrust;
            Vector3 flyDir = centerEye.transform.forward;
            transform.position += flyDir.normalized * thrust;
            //transform.position += centerEye.transform.position Rotate(0, increment, 0);
            //Debug.Log(transform.position);
        }
    }
    //IEnumerator fadeToBlack()
    //{
    //    blackScreen.SetActive(true);
    //    yield return new WaitForSeconds(2);
    //    blackScreen.SetActive(false);
    //}

    IEnumerator movementTransition()
    {
        //while (_exposure > 0)                                       // decrease brightness of skybox
        //{
        //    yield return new WaitForSeconds(0.0005f);
        //    darkenSkybox();
        //}
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        flyParticles.SetActive(!flyParticles.activeSelf);
        blackScreen.SetActive(false);

        //while (_exposure < 1)                                       // increase brightness of skybox
        //{
        //    yield return new WaitForSeconds(0.0005f);
        //    brightenSkybox();
        //}

    }

    //private void brightenSkybox()
    //{
    //    if (_exposure < 1)
    //    {
    //        _exposure += 0.005f;
    //        RenderSettings.skybox.SetFloat("_Exposure", _exposure);
    //    }
    //}

    //private void darkenSkybox()
    //{
    //    if (_exposure > 0)
    //    {
    //        _exposure -= 0.005f;
    //        RenderSettings.skybox.SetFloat("_Exposure", _exposure);
    //    }
    //}
}
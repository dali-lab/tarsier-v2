using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BeeRightControls : MonoBehaviour
{
    public Material normalSkybox;
    public Material beeSkybox;

    public GameObject cameraRig;
    public GameObject centerEye;
    public GameObject windParticles;
    public GameObject RController;

    public SceneFader sceneFader;
    public AudioSource windSound;
    public float speed = .06f;

    private VRTK_ControllerEvents RControllerEvents;
    private bool isFlying = false;
    private bool isNormalSkybox = true;

    private void OnEnable()
    {
        windParticles.SetActive(false);

        RController.GetComponent<VRTK_Pointer>().enabled = true;
        RController.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;

        RControllerEvents = RController.GetComponent<VRTK_ControllerEvents>();
        RControllerEvents.ButtonOnePressed += DoButtonOnePressed;
        RControllerEvents.ButtonTwoPressed += DoButtonTwoPressed;
    }

    private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)                        // fly: trigger transition, toggle teleport
    {
        StartCoroutine(movementTransition());
        RController.GetComponent<VRTK_Pointer>().enabled = !RController.GetComponent<VRTK_Pointer>().enabled;
        RController.GetComponent<VRTK_StraightPointerRenderer>().enabled = !RController.GetComponent<VRTK_StraightPointerRenderer>().enabled;
    }

    private void DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)                        // vision: skybox swap
    {
        isNormalSkybox = !isNormalSkybox;
        SkyboxSwap();
    }

    private void Update()
    {
        Fly();
    }


    private void Fly()                                              // fly via head tilt (tracks headset)
    {
        if (isFlying == true)
        {
            Vector3 flyDir = centerEye.transform.forward;
            cameraRig.transform.position += flyDir.normalized * speed;
        }
    }

    private void SkyboxSwap()
    {
        if (isNormalSkybox)
        {
            RenderSettings.skybox = normalSkybox;
        }
        else
        {
            RenderSettings.skybox = beeSkybox;
        }
    }

    private IEnumerator movementTransition()                        // fade to black and unfade for transition
    {
        sceneFader.StartFade();
        yield return new WaitForSeconds(1);
        windParticles.SetActive(!windParticles.activeSelf);
        isFlying = !isFlying;

        if (isFlying == false)                                       // fade out sound
        {
            float startVolume = windSound.volume;
            while (windSound.volume > 0)
            {
                windSound.volume -= startVolume * Time.deltaTime / 1;
                yield return null;
            }
        }
        else                                                        // fade in sound
        {
            while (windSound.volume < 1)
            {
                windSound.volume += 1 * Time.deltaTime / 1;
                yield return null;
            }
        }
    }
}
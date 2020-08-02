using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;
using Anivision.PlayerInteraction;

public class BeeRightControls : MonoBehaviour
{
    public Material normalSkybox;
    public Material beeSkybox;

    public GameObject cameraRig;                                    // to move the player when flying and teleport player back to hive when they run out of health
    public GameObject centerEye;                                    // for flying via headtilt
    public GameObject windParticles;

    public SceneFader sceneFader;
    public AudioSource windSound;
    public float speed = .06f;

    private InputManager _inputManager;
    private bool isFlying = false;
    private bool isNormalSkybox = true;


    private void OnEnable()
    {
        _inputManager = InputManager.Instance;

        if (_inputManager != null)
        {
            _inputManager.AttachInputHandler(StartMovementTransition, InputManager.InputState.ON_PRESS, InputManager.Button.A);
            _inputManager.AttachInputHandler(StartSkyboxSwap, InputManager.InputState.ON_PRESS, InputManager.Button.B);
        }
        else
        {
            throw new System.Exception("Must have an input manager script in the scene");
        }

        windParticles.SetActive(false);
        gameObject.GetComponent<Teleport>().enabled = true;
    }

    private void StartMovementTransition()                        // fly: trigger transition, toggle teleport
    {
        StartCoroutine(movementTransition());
        gameObject.GetComponent<Teleport>().enabled = !gameObject.GetComponent<Teleport>().enabled;
    }

    private void StartSkyboxSwap()                        // vision: skybox swap
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

    private void SkyboxSwap()                                       // toggle between the human and bee vision skybox
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
        isFlying = !isFlying;
        sceneFader.StartFade();
        yield return new WaitForSeconds(1);
        windParticles.SetActive(!windParticles.activeSelf);


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

    private void OnDisable()
    {
        if (_inputManager != null)
        {
            _inputManager.DetachInputHandler(StartMovementTransition, InputManager.InputState.ON_PRESS, InputManager.Button.A);
            _inputManager.DetachInputHandler(StartSkyboxSwap, InputManager.InputState.ON_PRESS, InputManager.Button.B);
        }
    }
}
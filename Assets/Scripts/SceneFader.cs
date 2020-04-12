using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public GameObject rightModel;
    public GameObject leftModel;
    public Camera eye;
    public GameObject fadePrefab;
    public float fadeDuration = 1;

    public delegate void FadeAction();
    public static event FadeAction OnFadeComplete;

    private GameObject fadeObject;
    private MaterialPropertyBlock block;
    private float alpha;
    private bool fade;
    private bool unfade;

    // Start is called before the first frame update
    void Start()
    {
        // Find or create the fade object
        Transform fadeTransform = eye.transform.Find("fadeObject");
        if (fadeTransform)
        {
            fadeObject = fadeTransform.gameObject;
        }
        else {
            fadeObject = Instantiate(fadePrefab, eye.transform);
            fadeObject.name = "fadeObject";
        }

        // When entering a scene, start the fade object as fully opaque
        alpha = 1;

        // Set up the material property block
        block = new MaterialPropertyBlock();
        UpdateFadeObject();

        // Hide controllers to start (so they don't interfere with the fade object
        rightModel.SetActive(false);
        leftModel.SetActive(false);

        // Start unfading
        unfade = true;
        fade = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            // Increase alpha value
            alpha += Time.deltaTime / fadeDuration;
            UpdateFadeObject();

            // If alpha is one, finish fade
            if (alpha >= 1)
            {
                alpha = 1;
                fade = false;
                unfade = true;

                // Send out fade complete event
                if (OnFadeComplete != null)
                    OnFadeComplete();
            }
        }
        else if (unfade)
        {
            // Decrease alpha value
            alpha -= Time.deltaTime / fadeDuration;
            UpdateFadeObject();

            // If alpha is zero, finish unfade
            if (alpha <= 0)
            {
                alpha = 0;
                unfade = false;

                rightModel.SetActive(true);
                leftModel.SetActive(true);
            }
        }
    }

    // Start fading
    public void StartFade()
    {
        fade = true;

        // Hide controllers during fade
        rightModel.SetActive(false);
        leftModel.SetActive(false);
    }

    public bool IsFading()
    {
        return (fade || unfade);
    }

    // Update the property block and the fade object's renderer to reflect the current alpha value
    private void UpdateFadeObject()
    {
        block.SetColor("_BaseColor", new Color(0, 0, 0, alpha));
        fadeObject.GetComponent<Renderer>().SetPropertyBlock(block);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

public class BeeTutorialNectar : MonoBehaviour
{
    public GameObject RController;
    public GameObject nextPanel;
    public GameObject nectarBar;                                                                        // nectar heaalth bar on left hand
    public GameObject gripHighlight;                                                                    // indicates which button to click
    public GameObject tutorialNectar;                                                                   // the nectar glob to pick up in the flower

    private InputManager _inputManager;
    private bool isPressing = false;
    private Vector3 RControllerPoint;
    private Collider nectarCollider;


    public void OnEnable()
    {
        _inputManager = InputManager.Instance;

        if (_inputManager == null)
        {
            throw new System.Exception("Must have an input manager script in the scene");
        }

        gripHighlight.SetActive(true);

        if (_inputManager != null)
        {
            _inputManager.OnRightGripPress += DoRightGripPressed;
            _inputManager.OnRightGripRelease += DoRightGripReleased;
        }
            
        nectarCollider = tutorialNectar.GetComponent<Collider>();
    }

    public void Update()
    {
        RControllerPoint = RController.transform.position;
        if (isPressing == true && nectarCollider.bounds.Contains(RControllerPoint))     // check to see if player is grabbing the nectar glob
        {
            tutorialNectar.SetActive(false);
            nectarBar.GetComponent<NectarUI>().addHealth(1);                            // set health to max
            gripHighlight.SetActive(false);
            gameObject.SetActive(false);
            nextPanel.SetActive(true);
        }
    }

    private void DoRightGripPressed()
    {
        isPressing = true;
    }

    private void DoRightGripReleased()
    {
        isPressing = false;
    }

    public void OnDisable()
    {
        if (_inputManager != null)
        {
            _inputManager.OnRightGripPress -= DoRightGripPressed;
            _inputManager.OnRightGripRelease -= DoRightGripReleased;
        }
            
    }
}

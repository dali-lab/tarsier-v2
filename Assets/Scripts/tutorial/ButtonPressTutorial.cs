﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Anivision.Core;

public class ButtonPressTutorial : TutorialBaseClass
{

    [System.Serializable]
    public class TutorialButton
    {
        public Tag tag;
        public Hand hand;
        public GameObject button;
    }

    public GameObject[] textScreens;

    public GameObject RController;
    public GameObject LController;

    public GameObject OculusOVRController;


    public TutorialButton[] buttons;

    private InputManager _inputManager;

    private Hashtable buttonsHashed;

    private int currentIndex = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        //get singleton instance of input manager
        _inputManager = InputManager.Instance;

        buttonsHashed = new Hashtable();

        textScreens[0].SetActive(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            HashKeys key = (HashKeys)System.Enum.Parse(typeof(HashKeys), buttons[i].hand.ToString() + "_" + buttons[i].tag.ToString());
            buttonsHashed.Add(key, buttons[i].button);
        }

        isDone = false;

        // TODO: Figure out how to update this, as this is obsolete
        InputTracking.Recenter();

        StartCoroutine(Wait());

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);

        // move to next screen
        textScreens[0].SetActive(false);
        textScreens[1].SetActive(true);

        currentIndex = 1;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].tag == Tag.BUTTON1 || buttons[i].tag == Tag.BUTTON2)
            {
                buttons[i].button.SetActive(true);
            }
        }

        // TODO: Figure out how to update this, as this is obsolete
        InputTracking.Recenter();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (currentIndex == 1)           // on button screen
        {

            GameObject R1 = (GameObject)(buttonsHashed[HashKeys.RIGHT_BUTTON1]);
            GameObject R2 = (GameObject)(buttonsHashed[HashKeys.RIGHT_BUTTON2]);
            GameObject L1 = (GameObject)(buttonsHashed[HashKeys.LEFT_BUTTON1]);
            GameObject L2 = (GameObject)(buttonsHashed[HashKeys.LEFT_BUTTON2]);

            if (_inputManager.IsButtonPressed(InputManager.Button.A))
            {
                R1.SetActive(false);                 // turn off glow on R button 1
            }
            if (_inputManager.IsButtonPressed(InputManager.Button.B))
            {
                R2.SetActive(false);                 // turn off glow on R button 2
               
            }
            if (_inputManager.IsButtonPressed(InputManager.Button.X))
            {
                L1.SetActive(false);                 // turn off glow on L button 1
            }
            if (_inputManager.IsButtonPressed(InputManager.Button.Y))
            {
                L2.SetActive(false);                 // turn off glow on L button 2
            }

            // once all buttons are pressed
            if (!R1.activeSelf && !R2.activeSelf && !L1.activeSelf && !L2.activeSelf)
            {
                // move to next screen
                textScreens[1].SetActive(false);
                textScreens[2].SetActive(true);

                currentIndex = 2;

                ((GameObject) buttonsHashed[HashKeys.LEFT_TRIGGER]).SetActive(true);
                ((GameObject)buttonsHashed[HashKeys.RIGHT_TRIGGER]).SetActive(true);
            }
        } else if (currentIndex == 2)           // on trigger screen
        {
            GameObject RTrigger = (GameObject)buttonsHashed[HashKeys.RIGHT_TRIGGER];
            GameObject LTrigger = (GameObject)buttonsHashed[HashKeys.LEFT_TRIGGER];

            if (_inputManager.IsButtonPressed(InputManager.Button.RIGHT_TRIGGER))
            {
                RTrigger.SetActive(false);                 // turn off glow on R trigger
            }
            if (_inputManager.IsButtonPressed(InputManager.Button.LEFT_TRIGGER))
            {
                LTrigger.SetActive(false);                 // turn off glow on L trigger
            }

            // once both triggers are pressed
            if (!RTrigger.activeSelf && !LTrigger.activeSelf)
            {
                // move to next screen
                textScreens[2].SetActive(false);
                textScreens[3].SetActive(true);

                currentIndex = 3;

                ((GameObject) buttonsHashed[HashKeys.LEFT_GRIP]).SetActive(true);
                ((GameObject)buttonsHashed[HashKeys.RIGHT_GRIP]).SetActive(true);
            }

        } else if (currentIndex == 3)       // on grip screen
        {
            GameObject RGrip = (GameObject)buttonsHashed[HashKeys.RIGHT_GRIP];
            GameObject LGrip = (GameObject)buttonsHashed[HashKeys.LEFT_GRIP];

            if (_inputManager.IsButtonPressed(InputManager.Button.RIGHT_GRIP))
            {
                RGrip.SetActive(false);                 // turn off glow on R grip
            }
            if (_inputManager.IsButtonPressed(InputManager.Button.LEFT_GRIP))
            {
                LGrip.SetActive(false);                 // turn off glow on L grip
            }

            // once both grips are pressed
            if (!RGrip.activeSelf && !LGrip.activeSelf)
            {
                // move to next screen
                textScreens[3].SetActive(false);
                isDone = true;
            }
        }
    }

    public override void Disable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].button.SetActive(false);
        }

        for (int i = 0; i < textScreens.Length; i++)
        {
            textScreens[i].SetActive(false);
        }
    }
}

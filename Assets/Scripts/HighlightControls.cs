// Highlights buttons when they are touched (using capacative sensing)
// Instantiates a HighlightObject for every touch-sensitive button, and shows them when they're touched

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HighlightControls : MonoBehaviour
{
    // VRTK's event system for when buttons are touched or pressed (or anything)
    protected VRTK_ControllerEvents controllerEvents;

    // Whether or not this script is on the right controller (true if on the right controller, false if on the left)
    public bool RightHand;
    // How fast to fade the highlights in and out
    public float FadeSpeed = 2;
    // What object to instantiate as the highlight, what object is overlayed on the buttons
    public GameObject HighlightObject;
    // The colors for every button's highlight
    public Color buttonOneColor = Color.green;
    public Color buttonTwoColor = Color.green;
    public Color touchpadColor = Color.green;
    public Color triggerColor = Color.green;
    public Color gripColor = Color.green;

    // GameObjects for each button's HighlightObject
    private GameObject ButtonOne;
    private GameObject ButtonTwo;
    private GameObject Touchpad;
    private GameObject Trigger;
    private GameObject Grip;

    private MaterialPropertyBlock buttonOneBlock;
    private MaterialPropertyBlock buttonTwoBlock;
    private MaterialPropertyBlock touchpadBlock;
    private MaterialPropertyBlock triggerBlock;
    private MaterialPropertyBlock gripBlock;

    private void Start()
    {
        // Get the VRTK ControllerEvents script attached to the same GameObject as this script
        controllerEvents = GetComponent<VRTK_ControllerEvents>();

        // Mirror the positions of the buttons if the script is on the right controller
        // (I found the initial values using the left hand)
        float sign = 1f;
        if (RightHand)
        {
            sign = -1f;
        }

        // Create the highlight objects (I pretty much just used trial and error for their position and size)
        ButtonOne = CreateHighlight(new Vector3(-0.00146f * sign, -0.00313f, -0.00488f), new Vector3(0.012f, 0.012f, 0.012f));
        ButtonTwo = CreateHighlight(new Vector3(0.0023f * sign, -0.0007f, 0.0088f), new Vector3(0.012f, 0.012f, 0.012f));
        Touchpad = CreateHighlight(new Vector3(-0.01791f * sign, 0.0067f, 0.0079f), new Vector3(0.016f, 0.016f, 0.016f));
        Trigger = CreateHighlight(new Vector3(-0.0095f * sign, -0.0207f, 0.0218f), new Vector3(0.028f, 0.028f, 0.028f));
        Grip = CreateHighlight(new Vector3(0.0028f * sign, -0.0302f, -0.0226f), new Vector3(0.026f, 0.026f, 0.026f));

        buttonOneBlock = CreateBlock(buttonOneColor, ButtonOne);
        buttonTwoBlock = CreateBlock(buttonTwoColor, ButtonTwo);
        touchpadBlock = CreateBlock(touchpadColor, Touchpad);
        triggerBlock = CreateBlock(triggerColor, Trigger);
        gripBlock = CreateBlock(gripColor, Grip);
    }

    private void Update()
    {
        // Update the highlights for every button, based on whether or not the button is pressed
        UpdateHighlight(ButtonOne, controllerEvents.buttonOneTouched, buttonOneBlock);
        UpdateHighlight(ButtonTwo, controllerEvents.buttonTwoTouched, buttonTwoBlock);
        UpdateHighlight(Touchpad, controllerEvents.touchpadTouched, touchpadBlock);
        UpdateHighlight(Trigger, controllerEvents.triggerTouched, triggerBlock);
        UpdateHighlight(Grip, controllerEvents.gripHairlinePressed, gripBlock);
    }


    // Create the HighlightObjects for a button
    // Takes a position for the object (where the button is in relation to the controller), and a scale
    // Returns an instance of the HighlightObject for the button
    private GameObject CreateHighlight(Vector3 Position, Vector3 Scale)
    {
        // Instantiate the object at the correct position
        GameObject Instance = Instantiate(HighlightObject, Position, Quaternion.identity, gameObject.transform);
        Instance.transform.localScale = Scale; // Scale the highlight
        Instance.transform.parent = gameObject.transform;
        // Hide the highlight to start
        Instance.GetComponent<Renderer>().enabled = false;

        return Instance;
    }

    // Updates the HighlightObject for a button
    // Takes the GameObject of the highlight, whether or not the button is touched, and the color of the highlight
    private void UpdateHighlight(GameObject control, bool active, MaterialPropertyBlock block)
    {
        Renderer render = control.GetComponent<Renderer>(); // Get the renderer of the HighlightObject
        // If the renderer is enabled, alter the object's opacity
        if (render.enabled)
        {
            Color color = block.GetColor("_BaseColor"); // Get the current color of the object
            // If the associated button is touched, increase the object's opacity
            if (active)
            {
                // Only increase opacity if it isn't at its maximum
                if (color.a <= 1f)
                {
                    color.a += Time.deltaTime * FadeSpeed;
                }
                // If the opacity has reached its max, set it to the max (just in case it has gone over)
                else
                {
                    color.a = 1f;
                }
            }
            // If the associated button isn't touched, decrease the object's opacity (or disable it)
            else
            {
                // Only decrease opacity if it isn't at its minimum
                if (color.a >= 0f)
                {
                    color.a -= Time.deltaTime * FadeSpeed;
                }
                // If the opacity has reached its minimum, hide the object
                else
                {
                    color.a = 0f;
                    render.enabled = false;
                }
            }
            // Change the color/opacity of the object
            block.SetColor("_BaseColor", color); // Set the block's opacity to the new opacity
            render.SetPropertyBlock(block);
        }
        // If the object isn't visible, but it's button is touched, make it visible
        else if (active)
        {
            render.enabled = true;
        }
    }

    private MaterialPropertyBlock CreateBlock(Color color, GameObject highlight)
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", new Color(color.r, color.g, color.b, 0f));
        highlight.GetComponent<Renderer>().SetPropertyBlock(block);
        return block;
    }
}
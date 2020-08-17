// Highlights buttons when they are touched (using capacative sensing)
// Instantiates a HighlightObject for every touch-sensitive button, and shows them when they're touched

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

namespace Anivision.PlayerInteraction
{
    public class HighlightControls : MonoBehaviour
    {

        //Input manager for when buttons are touched/pressed
        protected InputManager _inputManager;

        // Whether or not script is on the right hand; if true, it is on the right hand, if false, on the left hand 
        public bool RightHand;

        // How fast to fade the highlights in and out
        public float FadeSpeed = 4;
        // What object to instantiate as the highlight, what object is overlayed on the buttons
        public GameObject HighlightObject;
        // The colors for every button's highlight
        public Color buttonOneColor = Color.white;
        public Color buttonTwoColor = Color.white;
        public Color touchpadColor = Color.white;
        public Color triggerColor = Color.white;
        public Color gripColor = Color.white;

        // GameObjects for Right hand
        private GameObject rButtonA;
        private GameObject rButtonB;
        private GameObject rTouchpad;
        private GameObject rTrigger;
        private GameObject rGrip;

        // GameObjects for Left Hand
        private GameObject lButtonX;
        private GameObject lButtonY;
        private GameObject lTouchpad;
        private GameObject lTrigger;
        private GameObject lGrip;

        //Material blocks
        private MaterialPropertyBlock rButtonABlock;
        private MaterialPropertyBlock rButtonBBlock;
        private MaterialPropertyBlock rTouchpadBlock;
        private MaterialPropertyBlock rTriggerBlock;
        private MaterialPropertyBlock rGripBlock;

        private MaterialPropertyBlock lButtonXBlock;
        private MaterialPropertyBlock lButtonYBlock;
        private MaterialPropertyBlock lTouchpadBlock;
        private MaterialPropertyBlock lTriggerBlock;
        private MaterialPropertyBlock lGripBlock;


        private void Start()
        {

            //Get singleton instance of input manager
            _inputManager = InputManager.Instance;

            if (_inputManager == null)
            {
                throw new System.Exception("Must have Input Manager script in scene");
            }

            if (_inputManager != null)
            {
                // Create the highlight objects (I pretty much just used trial and error for their position and size)
                if (RightHand)
                {
                    CreateHighlightsRightHand();
                } else
                {
                    CreateHighlightsLeftHand();
                }
            
           
            }
           
        
        }

        private void Update()
        {
            if (_inputManager == null)
            {
                throw new System.Exception("Must have Input Manager script in scene");
            }

            if (RightHand)
            {
                UpdateRightHandHighlights();
            } else
            {
                UpdateLeftHandHighlights();
            }

        }

        // Update the highlights for every button, based on whether or not the button is pressed
        private void UpdateRightHandHighlights()
        {
            UpdateHighlight(rButtonA, _inputManager.ButtonATouched, rButtonABlock);
            UpdateHighlight(rButtonB, _inputManager.ButtonBTouched, rButtonBBlock);
            UpdateHighlight(rTouchpad, _inputManager.RightJoystickTouched, rTouchpadBlock);
            UpdateHighlight(rTrigger, _inputManager.RightTriggerTouched, rTriggerBlock);
            UpdateHighlight(rGrip, _inputManager.RightGripTouched, rGripBlock);
        }

        // Update the highlights for every button, based on whether or not the button is pressed
        private void UpdateLeftHandHighlights()
        {
            UpdateHighlight(lButtonX, _inputManager.ButtonXTouched, lButtonXBlock);
            UpdateHighlight(lButtonY, _inputManager.ButtonYTouched, lButtonYBlock);
            UpdateHighlight(lTouchpad, _inputManager.LeftJoystickTouched, lTouchpadBlock);
            UpdateHighlight(lTrigger, _inputManager.LeftTriggerTouched, lTriggerBlock);
            UpdateHighlight(lGrip, _inputManager.LeftGripTouched, lGripBlock);
        }

        private void CreateHighlightsLeftHand()
        {
            // Create the highlight objects (I pretty much just used trial and error for their position and size)
            lButtonX = CreateHighlight(new Vector3(-0.00146f, -0.00313f, -0.00488f), new Vector3(0.012f, 0.012f, 0.012f));
            lButtonY = CreateHighlight(new Vector3(0.0023f, -0.0007f, 0.0088f), new Vector3(0.012f, 0.012f, 0.012f));
            lTouchpad = CreateHighlight(new Vector3(-0.01791f, 0.0067f, 0.0079f), new Vector3(0.016f, 0.016f, 0.016f));
            lTrigger = CreateHighlight(new Vector3(-0.0095f, -0.0207f, 0.0218f), new Vector3(0.028f, 0.028f, 0.028f));
            lGrip = CreateHighlight(new Vector3(0.0028f, -0.0302f, -0.0226f), new Vector3(0.026f, 0.026f, 0.026f));

            lButtonXBlock = CreateBlock(buttonOneColor, lButtonX);
            lButtonYBlock = CreateBlock(buttonTwoColor, lButtonY);
            lTouchpadBlock = CreateBlock(touchpadColor, lTouchpad);
            lTriggerBlock = CreateBlock(triggerColor, lTrigger);
            lGripBlock = CreateBlock(gripColor, lGrip);
        }

        private void CreateHighlightsRightHand()
        {
            //Flip the sign for the right hand
            rButtonA = CreateHighlight(new Vector3(-0.00146f * -1, -0.00313f, -0.00488f), new Vector3(0.012f, 0.012f, 0.012f));
            rButtonB = CreateHighlight(new Vector3(0.0023f * -1, -0.0007f, 0.0088f), new Vector3(0.012f, 0.012f, 0.012f));
            rTouchpad = CreateHighlight(new Vector3(-0.01791f * -1, 0.0067f, 0.0079f), new Vector3(0.016f, 0.016f, 0.016f));
            rTrigger = CreateHighlight(new Vector3(-0.0095f * -1, -0.0207f, 0.0218f), new Vector3(0.028f, 0.028f, 0.028f));
            rGrip = CreateHighlight(new Vector3(0.0028f * -1, -0.0302f, -0.0226f), new Vector3(0.026f, 0.026f, 0.026f));

            rButtonABlock = CreateBlock(buttonOneColor, rButtonA);
            rButtonBBlock = CreateBlock(buttonTwoColor, rButtonB);
            rTouchpadBlock = CreateBlock(touchpadColor, rTouchpad);
            rTriggerBlock = CreateBlock(triggerColor, rTrigger);
            rGripBlock = CreateBlock(gripColor, rGrip);
        }


        // Create the HighlightObjects for a button
        // Takes a position for the object (where the button is in relation to the controller), and a scale
        // Returns an instance of the HighlightObject for the button
        private GameObject CreateHighlight(Vector3 Position, Vector3 Scale)
        {
            // Instantiate the object at the correct position
            GameObject Instance = Instantiate(HighlightObject, gameObject.transform);
            Instance.transform.localScale = Scale; // Scale the highlight
            Instance.transform.localPosition = Position;

            // Hide the highlight to start
            // Instance.GetComponent<Renderer>().enabled = false;

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
                    if (color.a <= .5f)
                    {
                        color.a += Time.deltaTime * FadeSpeed;
                    }
                    // If the opacity has reached its max, set it to the max (just in case it has gone over)
                    else
                    {
                        color.a = .5f;
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
}
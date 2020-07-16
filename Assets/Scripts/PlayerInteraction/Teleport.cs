using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Core;

namespace Anivision.PlayerInteraction
{
    public class Teleport : MonoBehaviour
    {
        [Tooltip("The Camera Rig that will be teleporting.")]
        public OVRCameraRig cameraRig;
        [Tooltip("The transform from which the teleport raycast will eminate from. Probably the Left or Right Hand Anchor.")]
        public Transform rayOrigin;
        [Header("Teleport Settings & Controls")]
        [Tooltip("The Layer Mask of layers that the teleportation raycast can collide with. (The ray will pass through layers not on this list).")]
        public LayerMask validRaycastLayers;
        [Tooltip("The Layer Mask of layers that are valid destinations for a teleport. Players will only be able to teleport to objects on these layers.")]
        public LayerMask validTeleportLayers;
        [Tooltip("The trigger used to determine when the player is attempting to teleport.")]
        public InputManager.Button teleportButton;
        [Tooltip("How far the trigger must be pulled before a raycast occurs.")]
        public float teleportRange = 500f;
        [Header("Renderer Settings")]
        [Tooltip("The line renderer to use to visualize the raycast.")]
        public LineRenderer lineRenderer;
        [Tooltip("The color to give the line renderer when the teleport is valid.")]
        public Color validColor = Color.green;
        [Tooltip("The color to give the line renderer when the teleport is invalid.")]
        public Color invalidColor = Color.red;

        // Whether the current teleportation attempt is valid (The raycast hit a layer in the 'validLayers' Layer Mask)
        private bool valid;
        // The coordinate the player will teleport to when the trigger is released (The location that the raycast hit)
        private Vector3 destination;
        // Input manager to check for button press
        private InputManager inputManager;

        void Start()
        {
            // Get an instance of an input maanger
            inputManager = InputManager.Instance;
            // Set default values
            valid = false;
            destination = Vector3.zero;
            // Disable the line renderer
            lineRenderer.enabled = false;
        }

        void Update()
        {
            // Check if the trigger has been indented
            if (inputManager.IsButtonPressed(teleportButton))
            {
                // Enable the line renderer
                lineRenderer.enabled = true;
                // The first point on the line renderer will be the raycast origin
                var points = new Vector3[2];
                points[0] = rayOrigin.position;

                // Send a raycast out from the rayOrigin
                RaycastHit hit;
                if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, teleportRange, validRaycastLayers))
                {
                    // Determine if the hit game object's layer is one of the valid layers. (from https://answers.unity.com/questions/50279/check-if-layer-is-in-layermask.html)
                    valid = validTeleportLayers == (validTeleportLayers | (1 << hit.transform.gameObject.layer));
                    // If the layer is valid, save the hit location
                    if (valid)
                    {
                        destination = hit.point;
                    }
                    // Set the second point on the renderer to the raycast's hit point
                    points[1] = hit.point;
                }
                else
                {
                    // If raycast does not hit, then the teleport cannot be valid
                    valid = false;
                    // Set the second point on the renderer to the max distance in front of the origin
                    points[1] = rayOrigin.position + rayOrigin.forward * teleportRange;
                }
                // Update the line renderer's points
                lineRenderer.SetPositions(points);
            }
            // if the trigger is not pressed, and a valid teleport was logged, teleport the player
            else if (valid)
            {
                // Move the player controller to the destination
                cameraRig.transform.position = destination;

                // Reset the teleporter
                valid = false;
            }
            else
            {
                // Disable the line renderer once the trigger is no longer held
                lineRenderer.enabled = false;
            }

            // Set the line renderer's color based on whether the teleport is valid
            SetLineRendererColor(valid, validColor, invalidColor);
        }

        // Set the line renderer's color based on whether the teleport is valid
        private void SetLineRendererColor(bool valid, Color validColor, Color invalidColor)
        {
            if (lineRenderer.enabled)
            {
                if (valid)
                {
                    // If the teleport is valid, make the line renderer green
                    lineRenderer.startColor = validColor;
                    lineRenderer.endColor = validColor;
                }
                else
                {
                    // If the teleport is invalid, make the line renderer red
                    lineRenderer.startColor = invalidColor;
                    lineRenderer.endColor = invalidColor;
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Anivision.Core;
using Anivision.PlayerInteraction;
using Anivision.Vision;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif
using UnityEngine;

namespace Anivision.PlayerInteraction
{
    /// <summary>
    /// Script that contains the movement information for an animal
    /// Developers/Artists can check various parameters through the inspector
    /// </summary>
    public class AnimalMovement : MonoBehaviour
    {
        public bool canFly = false;
        public bool canTeleport = true;
        public LayerMask validRaycastLayers;
        public LayerMask validTeleportLayers;
        public float teleportRange = 500f;
        
        //Create and return a MovementParameters object from the parameters chosen in the inspector
        public MovementParameters ConstructMovementParametersObject()
        {
            if (teleportRange < 1)
            {
                UnityEngine.Debug.LogError("Teleport Range must be >= 1f");
                teleportRange = 1f;
            }
            return new MovementParameters(canFly, canTeleport, validRaycastLayers, validTeleportLayers, teleportRange);
        }
        
#if UNITY_EDITOR
    /// <summary>
    /// Custom editor for AnimalMovement script
    /// </summary>
    [CustomEditor(typeof(AnimalMovement))]
    public class MovementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AnimalMovement movementScript = target as AnimalMovement;

            EditorGUILayout.Space(7);
            movementScript.canFly = GUILayout.Toggle(movementScript.canFly, "Can Fly");
            EditorGUILayout.Space(5);
            movementScript.canTeleport = GUILayout.Toggle(movementScript.canTeleport, "Can Teleport");
            EditorGUILayout.Space(3);
            
            //only show teleport options if teleport is enabled
            if (movementScript.canTeleport)
            {
                var raycastLayersSelected = EditorGUILayout.MaskField(new GUIContent("Valid Raycast Layers", "Which layers the teleport raycast can detect"),
                    LayerMaskToField(movementScript.validRaycastLayers), InternalEditorUtility.layers);
                var teleportLayersSelected = EditorGUILayout.MaskField(new GUIContent("Valid Teleport Layers", "Which layers the players can teleport to"),
                    LayerMaskToField(movementScript.validTeleportLayers), InternalEditorUtility.layers);
                movementScript.teleportRange = EditorGUILayout.FloatField("Teleport Range", movementScript.teleportRange);
            
                movementScript.validRaycastLayers = FieldToLayerMask(raycastLayersSelected);
                movementScript.validTeleportLayers = FieldToLayerMask(teleportLayersSelected);
           
            }

        }
    
        //Taken from https://answers.unity.com/questions/1073094/custom-inspector-layer-mask-variable.html
        // Converts the field value to a LayerMask
        private LayerMask FieldToLayerMask(int field)
        {
            LayerMask mask = 0;
            var layers = InternalEditorUtility.layers;
            for (int c = 0; c < layers.Length; c++)
            {
                if ((field & (1 << c)) != 0)
                {
                    mask |= 1 << LayerMask.NameToLayer(layers[c]);
                }
            }
            return mask;
        }
        // Converts a LayerMask to a field value
        private int LayerMaskToField(LayerMask mask)
        {
            int field = 0;
            var layers = InternalEditorUtility.layers;
            for (int c = 0; c < layers.Length; c++)
            {
                if ((mask & (1 << LayerMask.NameToLayer(layers[c]))) != 0)
                {
                    field |= 1 << c;
                }
            }
            return field;
        }
    }
#endif
    }
    
}
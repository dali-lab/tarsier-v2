using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Anivision.Core;

namespace Anivision.Vision
{
    /// <summary>
    /// Script that contains the vision information for an animal
    /// Developers/Artists can check various parameters through the inspector
    /// </summary>
    public class AnimalVision : MonoBehaviour
    {
        public Animal animal;
        public bool hasUvVision;
        public Colorblindness colorblindness;
        public bool swapMaterials;
        
        //following variables used to keep track of custom colorblind filter values
        public float rRed = 1f;
        public float rGreen;
        public float rBlue;
        
        public float gRed;
        public float gGreen = 1f;
        public float gBlue;
        
        public float bRed;
        public float bGreen;
        public float bBlue = 1f;
        
        public VisionParameters ConstructVisionParametersObject(Animal animal)
        {
            if (colorblindness == Colorblindness.Custom)
            {
                return new VisionParameters(animal, hasUvVision, colorblindness, ConstructColorblindMatrix(), swapMaterials);
            }
            else
            {
                return new VisionParameters(animal, hasUvVision, colorblindness, null, swapMaterials);
            } 
        }
        
        private Matrix4x4 ConstructColorblindMatrix()
        {
            return new Matrix4x4(
                new Vector4(rRed, rGreen, rBlue, 0f),
                new Vector4(gRed, gGreen, gBlue, 0f),
                new Vector4(bRed, bGreen, bBlue, 0f),
                new Vector4(0f, 0f, 0f, 1f));
        }
    }

    [CustomEditor(typeof(AnimalVision))]
    public class VisionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AnimalVision visionScript = target as AnimalVision;
    
            EditorGUILayout.Space(7);
            visionScript.swapMaterials = GUILayout.Toggle(visionScript.swapMaterials, new GUIContent("Swap Materials", "Swap the materials on environment objects that support material swap. If this is selected, objects that also support colorblindness and UV will not have colorblindness or UV applied"));
            EditorGUILayout.Space(3);

            if (visionScript.swapMaterials == false)
            {
                visionScript.hasUvVision = GUILayout.Toggle(visionScript.hasUvVision, new GUIContent("UV Vision", "Apply UV false color on objects that support it"));
                EditorGUILayout.Space(5);
                visionScript.colorblindness = (Colorblindness) EditorGUILayout.EnumPopup("Type of Colorblindness", visionScript.colorblindness);
                EditorGUILayout.Space(3);
            
                if (visionScript.colorblindness == Colorblindness.Custom)
                {
                    SetColorblindEditor(visionScript);
                }
            }

        }

        private void SetColorblindEditor(AnimalVision visionScript)
        {
            EditorGUILayout.LabelField(new GUIContent("Red Channel:", "How the red output channel should be mixed"), EditorStyles.boldLabel);
            visionScript.rRed = EditorGUILayout.Slider("Red", visionScript.rRed, 0.0f, 1.0f);
            visionScript.rGreen = EditorGUILayout.Slider("Green", visionScript.rGreen, 0.0f, 1.0f);
            visionScript.rBlue = EditorGUILayout.Slider("Blue", visionScript.rBlue, 0.0f, 1.0f);
            EditorGUILayout.Space(5);
                
            EditorGUILayout.LabelField(new GUIContent("Green Channel:", "How the green output channel should be mixed"), EditorStyles.boldLabel);
            visionScript.gRed = EditorGUILayout.Slider("Red", visionScript.gRed, 0.0f, 1.0f);
            visionScript.gGreen = EditorGUILayout.Slider("Green", visionScript.gGreen, 0.0f, 1.0f);
            visionScript.gBlue = EditorGUILayout.Slider("Blue", visionScript.gBlue, 0.0f, 1.0f);
            EditorGUILayout.Space(5);
                
            EditorGUILayout.LabelField(new GUIContent("Blue Channel:", "How the blue output channel should be mixed"), EditorStyles.boldLabel);
            visionScript.bRed = EditorGUILayout.Slider("Red", visionScript.bRed, 0.0f, 1.0f);
            visionScript.bGreen = EditorGUILayout.Slider("Green", visionScript.bGreen, 0.0f, 1.0f);
            visionScript.bBlue = EditorGUILayout.Slider("Blue", visionScript.bBlue, 0.0f, 1.0f);
            EditorGUILayout.Space(7);
        }

    }
}


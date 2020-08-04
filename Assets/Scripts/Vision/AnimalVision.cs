using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
#endif
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
        public ColorblindType colorblindType = ColorblindType.None;
        public List<VisionEffect> effects = new List<VisionEffect>();
        
        //following variables used to keep track of custom colorblind filter values
        private float rRed = 1f;
        private float rGreen;
        private float rBlue;
        
        private float gRed;
        private float gGreen = 1f;
        private float gBlue;
        
        private float bRed;
        private float bGreen;
        private float bBlue = 1f;

        public VisionParameters ConstructVisionParametersObject(Animal animal)
        {
            if (colorblindType == ColorblindType.Custom)
            {
                return new VisionParameters(animal, effects, colorblindType, ConstructColorblindMatrix());
            }
            else
            {
                return new VisionParameters(animal, effects, colorblindType,null);
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
        
#if UNITY_EDITOR
    /// <summary>
    /// Custom editor for AnimalVision script
    /// </summary>
    [CustomEditor(typeof(AnimalVision))]
    public class VisionEditor : Editor
    {
        private AnimalVision visionScript;
        private ReorderableList effectsList;

        private void OnEnable()
        {
            visionScript = target as AnimalVision;
            effectsList = new ReorderableList(serializedObject, serializedObject.FindProperty("effects"), true, true, true, true);
            effectsList.drawElementCallback = DrawListItems;
            effectsList.drawHeaderCallback = DrawHeader;
        }
        
        // Draws the elements on the list
        void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            visionScript.effects[index] = (VisionEffect) EditorGUI.EnumPopup(rect, visionScript.effects[index]);
        }

        //Draws the header
        void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, new GUIContent("Vision Effects", "Vision effects–applied in the order of this list"));
        }

        public override void OnInspectorGUI()
        {

            EditorGUILayout.Space(3);
            serializedObject.Update(); // Update the array property's representation in the inspector
            effectsList.DoLayoutList(); // Have the ReorderableList do its work
            // We need to call this so that changes on the Inspector are saved by Unity.
            serializedObject.ApplyModifiedProperties();
           
            if (visionScript.effects.Contains(VisionEffect.Colorblindness))
            {
                EditorGUILayout.LabelField("Colorblindness:", EditorStyles.boldLabel);
                visionScript.colorblindType = (ColorblindType) EditorGUILayout.EnumPopup("Type of Colorblindness", visionScript.colorblindType, new GUILayoutOption[]{GUILayout.MinWidth(150)});
                EditorGUILayout.Space(3);
            }
            
            if (visionScript.colorblindType == ColorblindType.Custom)
            {
                SetColorblindEditor();
            }
            
            if (GUI.changed)
            {
                EditorUtility.SetDirty(visionScript);
                EditorSceneManager.MarkSceneDirty(visionScript.gameObject.scene);
            }

        }

        private void SetColorblindEditor()
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
#endif
    }
}


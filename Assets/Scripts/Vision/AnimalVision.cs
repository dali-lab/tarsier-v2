using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
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
        private class VisionOrder
        {
            public MaterialVisionEffect visionEffect;
            public int order = 0;

            public VisionOrder(MaterialVisionEffect effect, int visionOrder)
            {
                visionEffect = effect;
                order = visionOrder;
            }
            
            public VisionOrder(MaterialVisionEffect effect)
            {
                visionEffect = effect;
            }

        }
        
        public Animal animal;
        public bool hasUvVision;
        public Colorblindness colorblindness;
        public bool swapMaterials;
        
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

        private VisionOrder uvVisonOrder = new VisionOrder(MaterialVisionEffect.UV);
        private VisionOrder colorblindnessOrder = new VisionOrder(MaterialVisionEffect.Colorblind);
        private VisionOrder swapMaterialsOrder = new VisionOrder(MaterialVisionEffect.MaterialSwap);
        private List<MaterialVisionEffect> _materialEffectOrders;

        private void Awake()
        {
            //determine order to apply material effects
            List<VisionOrder> visionOrders = new List<VisionOrder>();
            _materialEffectOrders = new List<MaterialVisionEffect>();
            if (hasUvVision)
            {
                visionOrders.Add(uvVisonOrder);
            }

            if (colorblindness != Colorblindness.None)
            {
                visionOrders.Add(colorblindnessOrder);
            }

            if (swapMaterials)
            {
                visionOrders.Add(swapMaterialsOrder);
            }
            
            visionOrders.Sort((a, b) => a.order - b.order);

            foreach (VisionOrder m in visionOrders)
            {
                _materialEffectOrders.Add(m.visionEffect);
            }
        }

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
        
#if UNITY_EDITOR
    /// <summary>
    /// Custom editor for AnimalVision script
    /// </summary>
    [CustomEditor(typeof(AnimalVision))]
    public class VisionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AnimalVision visionScript = target as AnimalVision;
            
            EditorGUILayout.Space(3);
            EditorGUILayout.LabelField(new GUIContent("Material Effects:", "These effects are applied to the materials of the objects in the world. If you choose multiple, you must choose the order you want them applied"), EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            visionScript.swapMaterials = GUILayout.Toggle(visionScript.swapMaterials, new GUIContent("Swap Materials", "Swap the materials on environment objects that support material swap. If this is selected, objects that also support colorblindness and UV will not have colorblindness or UV applied"), new GUILayoutOption[]{GUILayout.Width(150)});
            if (visionScript.swapMaterials)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.IntField(visionScript.swapMaterialsOrder.order, new GUILayoutOption[]{GUILayout.MaxWidth(150)});
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            visionScript.hasUvVision = GUILayout.Toggle(visionScript.hasUvVision, new GUIContent("UV Vision", "Apply UV false color on objects that support it"), new GUILayoutOption[]{GUILayout.Width(150)});
            if (visionScript.hasUvVision)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.IntField(visionScript.uvVisonOrder.order, new GUILayoutOption[]{GUILayout.MaxWidth(150)});
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);
            
            EditorGUILayout.BeginHorizontal();
            visionScript.colorblindness = (Colorblindness) EditorGUILayout.EnumPopup("Type of Colorblindness", visionScript.colorblindness, new GUILayoutOption[]{GUILayout.MinWidth(150)});
            if (visionScript.colorblindness != Colorblindness.None)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.IntField(visionScript.colorblindnessOrder.order, new GUILayoutOption[]{GUILayout.MaxWidth(150)});
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);
            
            if (visionScript.colorblindness == Colorblindness.Custom)
            {
                SetColorblindEditor(visionScript);
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
#endif
    }
}


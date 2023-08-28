using UnityEditor;
using UnityEngine;

namespace Particles.Packages.Core.Editor.Drawers
{
    public class VariableDrawer<T> : ParticleDrawer<T> where T : ScriptableObject
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects
                || property.objectReferenceValue == null)
            {
                base.OnGUI(position, property, label);
                return;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            var inner = new SerializedObject(property.objectReferenceValue);
            var valueProp = inner.FindProperty("value");
            
            var previewRect = new Rect(position);
            previewRect.width = GetPreviewSpace(valueProp?.type);
            position.xMin = previewRect.xMax;
            
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            // Draws preview window next assigned scriptable object.
            EditorGUI.BeginDisabledGroup(true);
            if (valueProp != null)
            {
                EditorGUI.PropertyField(previewRect, valueProp, GUIContent.none, false);
            }
            else
            {
                EditorGUI.LabelField(previewRect, "[Non Serialized Value]");
            }
            EditorGUI.EndDisabledGroup();

            position.x = position.x + 6f;
            position.width = position.width - 6f;
            base.OnGUI(position, property, GUIContent.none);
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        private static float GetPreviewSpace(string type)
        {
            return type switch
            {
                "Vector2" => 128,
                "Vector3" => 128,
                _ => 58
            };
        }
    }
}
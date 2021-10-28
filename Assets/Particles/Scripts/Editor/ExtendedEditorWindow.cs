using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExtendedEditorWindow : Editor
{
    protected SerializedObject m_SerializedObject;
    protected SerializedProperty m_SerializedProperty;

    protected void DrawProperties(SerializedProperty _property, bool _drawChildren)
    {
        string _lastPropertyPath = string.Empty;
        foreach (SerializedProperty _childProperty in _property)
        {
            if (_childProperty.isArray && _childProperty.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                _childProperty.isExpanded =
                    EditorGUILayout.Foldout(_childProperty.isExpanded, _childProperty.displayName);
                EditorGUILayout.EndHorizontal();

                if (_childProperty.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(_childProperty, _drawChildren);
                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_lastPropertyPath) && _childProperty.propertyPath.Contains(_lastPropertyPath))
                {
                    continue;
                }

                _lastPropertyPath = _childProperty.propertyPath;
                EditorGUILayout.PropertyField(_childProperty, _drawChildren);
            }
        }
    }
    
}

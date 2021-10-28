using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TimeSpanVariable))]
public class VariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TimeSpanVariable _variable = (TimeSpanVariable) target;
        string _value = GUILayout.TextField(_variable.m_Value.ToString());

        TimeSpan _newTimeSpan = TimeSpan.Zero;
        TimeSpan.TryParse(_value, out _newTimeSpan);
        _variable.m_Value = _newTimeSpan;
    }
}

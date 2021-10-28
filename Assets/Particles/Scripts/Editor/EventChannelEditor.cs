using UnityEditor;
using UnityEngine;

public abstract class EventChannelEditor<TType> : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GenericEventChannelSO<TType> _channel = (GenericEventChannelSO<TType>) target;
        
        
        if (GUILayout.Button("Raise"))
        {
            _channel.RaiseEvent(_channel.m_TestValue);
        }
    }
}

[CustomEditor(typeof(VoidEventChannelSO))]
public class VoidEventChannelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        VoidEventChannelSO _channel = (VoidEventChannelSO) target;

        if (GUILayout.Button("Raise"))
        {
            _channel.RaiseEvent();
        }
    }
}

[CustomEditor(typeof(IntEventChannelSO))]
public class IntEventChannelEditor : EventChannelEditor<int> { }

[CustomEditor(typeof(UpgradeEventChannelSO))]
public class UpgradeEventChannelEditor : EventChannelEditor<Upgrade> { }
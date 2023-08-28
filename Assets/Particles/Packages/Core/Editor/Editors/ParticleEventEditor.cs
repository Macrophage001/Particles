using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEditor;
using UnityEngine;

namespace Particles.Packages.Core.Editor.Editors
{
    [CustomEditor(typeof(GenericEvent<>))]
    public abstract class ParticleEventEditor<T, E> : UnityEditor.Editor
        where E : GenericEvent<T>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Raise"))
            {
                var e = target as E;
                if (e != null) e.RaiseEvent(e.InspectorValue);
            }
        }
    }
    
    [CustomEditor(typeof(GenericEventWithProps<>))]
    public abstract class ParticleEventEditorWithProps<T, E> : UnityEditor.Editor
        where E : GenericEvent<EventInvocationProperties<T>>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Raise"))
            {
                var e = target as E;
                if (e != null) e.RaiseEvent(e.InspectorValue);
            }
        }
    }
    

    [CustomEditor(typeof(VoidEvent))]
    public class VoidEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Raise"))
            {
                var e = target as VoidEvent;
                if (e != null) e.RaiseEvent();
            }
        }
    }
}
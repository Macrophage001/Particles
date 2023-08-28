using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Particles.Packages.Core.Runtime.Helpers
{
    public class EditorUtils : MonoBehaviour
    {
        public static void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
        {
            if (settings == null) return;
            
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using var check = new EditorGUI.ChangeCheckScope();
            if (!foldout) return;
            
            Editor.CreateCachedEditor(settings, null, ref editor);
            editor.OnInspectorGUI();
            if (check.changed)
            {
                onSettingsUpdated?.Invoke();
            }
        }
    }
}
#endif

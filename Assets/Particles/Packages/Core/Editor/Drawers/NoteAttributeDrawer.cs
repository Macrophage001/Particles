using Particles.Packages.Core.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Particles.Packages.Core.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(NoteAttribute))]
    public class NoteAttributeDrawer : DecoratorDrawer
    {
        private const float padding = 20f;
        private float height = 0f;
        public override float GetHeight()
        {
            var attr = (NoteAttribute)attribute;

            var style = EditorStyles.helpBox;
            style.alignment = TextAnchor.MiddleLeft;
            style.wordWrap = true;
            style.padding = new RectOffset(10, 10, 10, 10);
            style.fontSize = 12;

            height = style.CalcHeight(new GUIContent(attr.Text), Screen.width);
            return height + padding;
        }

        public override void OnGUI(Rect position)
        {
            var attr = (NoteAttribute)attribute;
            position.height = height;

            position.y += padding * 0.5f;
            EditorGUI.HelpBox(position, attr.Text, attr.MessageType);
        }
    }
}
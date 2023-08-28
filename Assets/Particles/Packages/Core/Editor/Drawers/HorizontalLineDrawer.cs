using Particles.Packages.Core.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Particles.Packages.Core.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(HorizontalLineAttribute))]
    public class HorizontalLineDrawer : DecoratorDrawer
    {
        public override float GetHeight()
        {
            var attr = (HorizontalLineAttribute)attribute;
            return Mathf.Max(attr.Padding, attr.Thickness);
        }

        public override void OnGUI(Rect position)
        {
            var attr = (HorizontalLineAttribute)attribute;
            position.height = attr.Thickness;
            
            position.y += attr.Padding * 0.5f;
            EditorGUI.DrawRect(position, EditorGUIUtility.isProSkin ? new Color(.3f, .3f, .3f, 1) : new Color(.7f, .7f, .7f, 1));
        }
    }
}
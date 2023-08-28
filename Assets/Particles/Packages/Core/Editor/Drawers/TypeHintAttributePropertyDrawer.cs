using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Particles.Packages.Core.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Particles.Packages.Core.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(TypeHintAttribute))]
    public class TypeHintAttributePropertyDrawer : PropertyDrawer
    {
        private static string GetGenericTypeArgumentsString(IEnumerable<Type> genericArgs)
        {
            var sb = new StringBuilder();
            sb.Append(string.Join(", ", genericArgs.Select(arg =>
            {
                if (arg.IsGenericType)
                {
                    var genericArgsText = GetGenericTypeArgumentsString(arg.GetGenericArguments());
                    var innerIndex = -1;
                    innerIndex = arg.Name.IndexOf('`');
                    return innerIndex != -1 ? $"{arg.Name.Substring(0, arg.Name.IndexOf('`'))}<{genericArgsText}>" : $"{arg.Name}<{genericArgsText}>";
                }

                var index = -1;
                index = arg.Name.IndexOf('`');
                return index != -1 ? arg.Name.Substring(0, arg.Name.IndexOf('`')) : arg.Name;
            })));
            return sb.ToString();
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var typeHint = (TypeHintAttribute)attribute;

            string tooltipText;

            if (typeHint.IsGeneric)
            {
                var targetType = property.serializedObject.targetObject.GetType();
                var field = targetType.GetField(property.name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (field != null && field.FieldType.IsGenericType)
                {
                    var genericArgs = field.FieldType.GetGenericArguments();
                    var genericArgsText = GetGenericTypeArgumentsString(genericArgs);
                    tooltipText = string.IsNullOrEmpty(typeHint.TypeHint) ? $"{field.Name}<{genericArgsText}>" : $"{typeHint.TypeHint}<{genericArgsText}>";
                }
                else
                {
                    tooltipText = typeHint.TypeHint;
                }
            }
            else
            {
                tooltipText = typeHint.TypeHint;
            }

            label.tooltip = tooltipText;
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}

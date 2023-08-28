using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Particles.Packages.Core.Runtime;
using UnityEditor;
using UnityEngine;

namespace Particles.Packages.Core.Editor.Drawers
{
    public abstract class ParticleDrawer<T> : PropertyDrawer where T : ScriptableObject
    {
        private class DrawerData
        {
            public bool userClickedToCreateParticle;
            public string nameOfNewParticle = "";
            public string warningText = "";
        }

        private const string NamingFieldControlName = "Naming Field";

        private Dictionary<string, DrawerData> perPropertyViewData = new();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects)
            {
                return EditorGUI.GetPropertyHeight(property);
            }
            
            var drawerData =  GetDrawerData(property.propertyPath);
            var isCreatingSO = drawerData.userClickedToCreateParticle && property.objectReferenceValue == null;
            if (!isCreatingSO || drawerData.warningText.Length <= 0) return base.GetPropertyHeight(property, label);
            return base.GetPropertyHeight(property, label) * 2 + 4f;
        }

        private DrawerData GetDrawerData(string propertyPath)
        {
            DrawerData drawerData;
            if (perPropertyViewData.TryGetValue(propertyPath, out drawerData)) return drawerData;
            drawerData = new DrawerData();
            perPropertyViewData.Add(propertyPath, drawerData);
            return drawerData;
        }

        private static Rect SnipRectV(Rect rect, float range)
        {
            if (range == 0) return new Rect(rect);
            if (range > 0)
            {
                return new Rect(rect.x, rect.y, rect.width, range);
            }

            return new Rect(rect.x, rect.y + rect.height + range, rect.width, -range);
        }

        private static Rect SnipRectV(Rect rect, float range, out Rect rest, float gutter = 0f)
        {
            if (range == 0) rest = new Rect();
            if (range > 0)
            {
                rest = new Rect(rect.x, rect.y + range + gutter, rect.width, rect.height - range - gutter);
            }
            else
            {
                rest = new Rect(rect.x, rect.y, rect.width, rect.height + range + gutter);
            }

            return SnipRectV(rect, range);
        }
        
        private static Rect SnipRectH(Rect rect, float range)
        {
            if (range == 0) return new Rect(rect);
            if (range > 0)
            {
                return new Rect(rect.x, rect.y, range, rect.height);
            }

            return new Rect(rect.x + rect.width + range, rect.y, -range, rect.height);
        }

        private static Rect SnipRectH(Rect rect, float range, out Rect rest, float gutter = 0f)
        {
            if (range == 0) rest = new Rect();
            if (range > 0)
            {
                rest = new Rect(rect.x + range + gutter, rect.y, rect.width - range - gutter, rect.height);
            }
            else
            {
                rest = new Rect(rect.x, rect.y, rect.width + range + gutter, rect.height);
            }

            return SnipRectH(rect, range);
        }

        private static string CleanPropertyName(string propertyName)
        {
            var cleanedProperty = propertyName;
            var regex = Regex.Match(cleanedProperty, @"[a-zA-Z]");
            if (!regex.Success) return cleanedProperty;
            var index = regex.Index;
            cleanedProperty = cleanedProperty[index].ToString().ToUpper() + cleanedProperty.Substring(index + 1);
            return cleanedProperty;
        }

        private static string CreateUniqueName(string atomName)
        {
            var results = AssetDatabase.FindAssets(atomName);
            return results.Length > 0 ? $"{atomName} ({results.Length})" : atomName;
        }

        private static object GetValue(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();
            var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (f == null)
            {
                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p == null)
                    return null;
                return p.GetValue(source, null);
            }
            return f.GetValue(source);
        }

        private static object GetValue(object source, string name, int index)
        {
            var enumerable = GetValue(source, name) as IEnumerable;
            var enm = enumerable.GetEnumerator();
            while (index-- >= 0)
                enm.MoveNext();
            return enm.Current;
        }

        private static object GetParent(SerializedProperty property)
        {
            var path = property.propertyPath.Replace(".Array.data[", "[");
            object obj = property.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1))
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue(obj, elementName, index);
                }
                else
                {
                    obj = GetValue(obj, element);
                }
            }
            return obj;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            
            var drawerData = GetDrawerData(property.propertyPath);
            
            var isCreatingSO = drawerData.userClickedToCreateParticle && property.objectReferenceValue == null;
            var restWidth = drawerData.userClickedToCreateParticle ? 50 : 58;
            var gutter = drawerData.userClickedToCreateParticle ? 2f : 6f;

            var restRect = new Rect();
            var warningRect = new Rect();

            if (drawerData.warningText.Length > 0)
            {
                position = SnipRectV(position, EditorGUIUtility.singleLineHeight, out warningRect, 2f);
            }

            if (property.objectReferenceValue == null)
            {
                position = SnipRectH(position, position.width - restWidth, out restRect, gutter);
            }
            
            var defaultGUIColour = GUI.color;
            GUI.color = isCreatingSO ? Color.yellow : defaultGUIColour;
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive),
                isCreatingSO && label != GUIContent.none ? new GUIContent("Name of new Particle") : label);
            GUI.color = defaultGUIColour;
            
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            GUI.SetNextControlName(NamingFieldControlName);
            drawerData.nameOfNewParticle = EditorGUI.TextField(isCreatingSO ? position : Rect.zero, drawerData.nameOfNewParticle);

            // If a particle is being assigned, show the object field and assign the particle
            if (!isCreatingSO)
            {
                EditorGUI.BeginChangeCheck();
                var obj = EditorGUI.ObjectField(position, property.objectReferenceValue, typeof(T), false);
                if (EditorGUI.EndChangeCheck())
                {
                    property.objectReferenceValue = obj;
                }
            }

            if (property.objectReferenceValue == null)
            {
                if (isCreatingSO)
                {
                    var buttonWidth = 24;
                    Rect secondButtonRect;
                    Rect firstButtonRect = SnipRectH(restRect, restRect.width - buttonWidth, out secondButtonRect, gutter);
                    if (GUI.Button(firstButtonRect, "✓") || (Event.current.keyCode == KeyCode.Return) &&
                        GUI.GetNameOfFocusedControl() == NamingFieldControlName)
                    {
                        if (drawerData.nameOfNewParticle.Length > 0)
                        {
                            try
                            {
                                var path = AssetDatabase.GetAssetPath(property.serializedObject.targetObject);
                                path = path == "" ? "Assets/" : Path.GetDirectoryName(path) + "/";

                                var so = ScriptableObject.CreateInstance<T>();
                                AssetDatabase.CreateAsset(so, path + drawerData.nameOfNewParticle + ".asset");
                                AssetDatabase.SaveAssets();
                                
                                property.objectReferenceValue = so;
                            }
                            catch (Exception e)
                            {
                                Debug.LogError($"Not able to create new particle: {e.Message}");
                            }
                            
                            drawerData.userClickedToCreateParticle = false;
                            drawerData.warningText = "";
                        }
                        else
                        {
                            drawerData.warningText = "Please enter a name for the new particle";
                            EditorGUI.FocusTextInControl(NamingFieldControlName);
                        }
                    }
                    if (GUI.Button(secondButtonRect, "✗") ||
                        (Event.current.keyCode == KeyCode.Escape && GUI.GetNameOfFocusedControl() == NamingFieldControlName))
                    {
                        drawerData.userClickedToCreateParticle = false;
                        drawerData.warningText = "";
                    }
                }
                else
                {
                    if (GUI.Button(restRect, "Create"))
                    {
                        drawerData.userClickedToCreateParticle = true;
                        EditorGUI.FocusTextInControl(NamingFieldControlName);
                        
                        var baseParticleName =  CleanPropertyName(property.name) + typeof(T).Name;
                        var particleName = GetParent(property) is Particle parentParticle
                            ? parentParticle.name + baseParticleName
                            : baseParticleName;
                        drawerData.nameOfNewParticle = CreateUniqueName(particleName);
                    }
                }

                EditorGUI.indentLevel = indent;
                EditorGUI.EndProperty();
            }
        }
    }
}

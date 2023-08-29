using System;
using System.IO;
using System.Text.RegularExpressions;
using Particles.Packages.BaseParticles.Runtime.Variables;
using Particles.Packages.Core.Editor.Drawers;
using Particles.Packages.Core.Runtime;
using Particles.Packages.Core.Runtime.Variables;
using UnityEditor;
using UnityEngine;
using TypeHelper = Particles.Packages.Core.Editor.Helpers.TypeHelper;

namespace Particles.Packages.Core.Editor.Generators
{
    public class ParticleDrawerGenerator : EditorWindow
    {
        private string GenerateParticleDrawerFromType(Type type, string @namespace, Type drawerType)
        {
            var drawerTypeName = drawerType.Name.Substring(0, drawerType.Name.IndexOf('`'));
            
            return $@"// Path: {saveDir}\{type.Name}Drawer.cs
            using Particles.Editor.Particles;
            using UnityEditor;
            
            namespace {@namespace}
            {{
                [CustomPropertyDrawer(typeof({type.Name}))]
                public sealed class {type.Name}Drawer : {drawerTypeName}<{type.Name}> {{ }}
            }} ";
        }

        private string GenerateParticleDrawerFromFile(string className, string @namespace, Type drawerType)
        {
            var drawerTypeName = drawerType.Name.Substring(0, drawerType.Name.IndexOf('`'));
            
            return $@"// Path: {saveDir}\{className}Drawer.cs
            using Particles.Editor.Particles;
            using UnityEditor;
            
            namespace {@namespace}
            {{
                [CustomPropertyDrawer(typeof({className}))]
                public sealed class {className}Drawer : {drawerTypeName}<{className}> {{ }}
            }}";
        }

        private static string assetsDir;
        private static string projectDir;
        private static string saveDir;

        private Particle particle;
        private TextAsset particleFile;

        [MenuItem("Particle Tools/Particle Drawer Generator")]
        public static void ShowWindow()
        {
            assetsDir = Application.dataPath;
            projectDir = Path.Combine(Directory.GetParent(assetsDir).FullName, "Assets");
            GetWindow(typeof(ParticleDrawerGenerator));
        }

        private void OnGUI()
        {
            GUILayout.Label("Particle Drawer Generator", EditorStyles.boldLabel);
            saveDir = EditorGUILayout.TextField("Relative Save Path", saveDir);
            particleFile = EditorGUILayout.ObjectField("Particle File ", particleFile, typeof(TextAsset), true) as TextAsset;
            if (GUILayout.Button("Generate"))
            {
                Generate();
            }
        }

        private void Generate()
        {
            if (particleFile == null)
            {
                Debug.LogError("Must select a file that contains at least one class that derives from Particle");
                return;
            }

            CreateParticleDrawerFromFile(particleFile);
        }

        private string[] FindAllClassesInFile(string code)
        {
            var regex = new Regex(@"\bclass\s+(\w+)\b");
            var matches = regex.Matches(code);
            var classes = new string[matches.Count];
            
            for (int i = 0; i < matches.Count; i++)
            {
                classes[i] = matches[i].Groups[1].Value;
            }
            
            return classes;
        }

        private string ConvertFilePathToNamespace(string filePath)
        {
            var dirSeparatorChar = filePath.Contains('/') ? '/' :
                filePath.Contains('\\') ? '\\' : throw new ArgumentException("Invalid file path!");
            
            return filePath.Replace(dirSeparatorChar, '.');
        }

        private void CreateParticleDrawer(Particle particle)
        {
            var particleType = particle.GetType();
            var isDerivedFromGenericVariable = TypeHelper.IsDerivedFromType(typeof(GenericVariable<>), particleType);
            var particleDrawerType = isDerivedFromGenericVariable ? typeof(VariableDrawer<>) : typeof(ParticleDrawer<>);
            
            var drawer = GenerateParticleDrawerFromType(particleType, ConvertFilePathToNamespace(saveDir), particleDrawerType);
            
            saveDir = Path.Combine(projectDir, saveDir);
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            
            var savePath = Path.Combine(saveDir, $"{particleType.Name}Drawer.cs");
            File.WriteAllText(savePath, drawer);
            
            AssetDatabase.Refresh();
        }

        private void CreateParticleDrawerFromFile(TextAsset particleFile)
        {
            var classes = FindAllClassesInFile(particleFile.text);
            foreach (var @class in classes)
            {
                if (!TypeHelper.TryFindType(@class, out var type))
                {
                    Debug.LogError($"Could not find type {@class}!");
                    continue;
                }
                
                if (!TypeHelper.IsDerivedFromType(typeof(Particle), type))
                {
                    Debug.LogError($"Class {@class} does not derive from Particle!");
                    continue;
                }
                
                var isDerivedFromGenericVariable = TypeHelper.IsDerivedFromType(typeof(GenericVariable<>), type);
                var drawerType = isDerivedFromGenericVariable ? typeof(VariableDrawer<>) : typeof(ParticleDrawer<>);
                var drawer = GenerateParticleDrawerFromType(type, ConvertFilePathToNamespace(saveDir), drawerType);
                
                saveDir = Path.Combine(projectDir, saveDir);
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }
                
                var savePath = Path.Combine(saveDir, $"{@class}Drawer.cs");
                File.WriteAllText(savePath, drawer);
                AssetDatabase.Refresh();
            }
        }

    }
}
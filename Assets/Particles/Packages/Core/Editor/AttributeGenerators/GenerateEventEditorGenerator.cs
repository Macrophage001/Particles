using System;
using System.IO;
using Particles.Packages.BaseParticles.Runtime.Events;
using Particles.Packages.Core.Runtime.Attributes;
using UnityEditor;
using UnityEngine;
using TypeHelper = Particles.Packages.Core.Editor.Helpers.TypeHelper;

namespace Particles.Packages.Core.Editor.AttributeGenerators
{
    
    [InitializeOnLoad]
    public class GenerateEventEditorGenerator
    {
        private static string assetsDir;
        private static string projectDir;
        private const string DefaultSaveDir = @"Particles\Packages\BaseParticles\Editor\Editors\Generated";
        
        static GenerateEventEditorGenerator()
        {
            assetsDir = Application.dataPath;
            
            DirectoryInfo parent;
            if ((parent = Directory.GetParent(assetsDir)) != null)
            {
                projectDir = Path.Combine(parent.FullName, "Assets");
            }
            else
            {
                Debug.LogError("Could not find project directory!");
                return;
            }
            
            Create();
        }
        
        private static string GenerateParticleDrawerFromType(Type classType, Type genericType, string typeNamespace, string @namespace, string saveDir)
        {
            string particleEventEditorType = TypeHelper.IsDerivedFromType(typeof(GenericEventWithProps<>), classType) ? "ParticleEventEditorWithProps" : "ParticleEventEditor";
            
            return $@"// Path: {saveDir}\{classType.Name}EventEditor.cs
using {genericType.Namespace};
using {typeNamespace};
using Particles.Packages.Core.Editor.Editors;
using UnityEditor;

namespace {@namespace}
{{
    [CustomEditor(typeof({classType.Name}))]
    public sealed class {classType.Name}EventEditor : {particleEventEditorType}<{TypeHelper.GetTypeName(genericType)}, {classType.Name}> {{ }}
}} ";
        }
        
        private static string ConvertFilePathToNamespace(string filePath)
        {
            var dirSeparatorChar = filePath.Contains('/') ? '/' :
                filePath.Contains('\\') ? '\\' : throw new ArgumentException("Invalid file path!");
            
            return filePath.Replace(dirSeparatorChar, '.');
        }

        private static void Create()
        {
            var attributeType = typeof(GenerateEventEditorAttribute);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var classType in assembly.GetTypes())
                {
                    if (classType.Name.Equals($"{classType.Name}EventEditor")) continue;
                    if (classType.GetCustomAttributes(attributeType, true).Length <= 0) continue;
                        
                    var attribute = (GenerateEventEditorAttribute) classType.GetCustomAttributes(attributeType, true)[0];
                    var saveDir = attribute.FilePath;
                    if (string.IsNullOrEmpty(saveDir)) saveDir = DefaultSaveDir;
                    
                    TypeHelper.TryGetTypeArgumentsFromGenericBaseClass(classType, out var genericTypeArguments);
                    if (genericTypeArguments is null or { Length: 0 or > 1 }) continue;
                    
                    var drawer = GenerateParticleDrawerFromType(classType, genericTypeArguments[0], classType.Namespace, ConvertFilePathToNamespace(saveDir), saveDir);
                    saveDir = Path.Combine(projectDir, saveDir);
                    if (!Directory.Exists(saveDir))
                    {
                        Directory.CreateDirectory(saveDir);
                    }
                    
                    var savePath = Path.Combine(saveDir, $"{classType.Name}EventEditor.cs");
                    File.WriteAllText(savePath, drawer);
                }
            }
        }
    }
}
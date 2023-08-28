using System;
using System.IO;
using Particles.Packages.BaseParticles.Runtime.Variables;
using Particles.Packages.Core.Editor.Drawers;
using Particles.Packages.Core.Runtime;
using Particles.Packages.Core.Runtime.Attributes;
using UnityEditor;
using UnityEngine;
using TypeHelper = Particles.Packages.Core.Editor.Helpers.TypeHelper;

namespace Particles.Packages.Core.Editor.AttributeGenerators
{
    
    [InitializeOnLoad]
    public class GenerateParticleDrawerGenerator
    {
        private static string assetsDir;
        private static string projectDir;
        private const string DefaultSaveDir = @"Particles\Packages\BaseParticles\Editor\Drawers\Generated";
        
        static GenerateParticleDrawerGenerator()
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
        
        private static string GenerateParticleDrawerFromType(Type type, string typeNamespace, string @namespace, string saveDir, Type drawerType)
        {
            var drawerTypeName = drawerType.Name.Substring(0, drawerType.Name.IndexOf('`'));
            
            return $@"// Path: {saveDir}\{type.Name}Drawer.cs
using {typeNamespace};
using Particles.Packages.Core.Editor.Drawers;
using UnityEditor;

namespace {@namespace}
{{
    [CustomPropertyDrawer(typeof({type.Name}))]
    public class {type.Name}Drawer : {drawerTypeName}<{type.Name}> {{ }}
}} ";
        }
        
        private static string ConvertFilePathToNamespace(string filePath)
        {
            var dirSeparatorChar = filePath.Contains('/') ? '/' :
                filePath.Contains('\\') ? '\\' : throw new ArgumentException("Invalid file path!");
            
            return filePath.Replace(dirSeparatorChar, '.');
        }

        public static void Create()
        {
            var attributeType = typeof(GenerateParticleDrawerAttribute);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var classType in assembly.GetTypes())
                {
                    if (classType.Name.Equals($"{classType.Name}Drawer")) continue;
                    if (classType.GetCustomAttributes(attributeType, true).Length <= 0) continue;
                    
                    var attribute = (GenerateParticleDrawerAttribute) classType.GetCustomAttributes(attributeType, true)[0];
                    var saveDir = attribute.filePath;
                    if (string.IsNullOrEmpty(saveDir)) saveDir = DefaultSaveDir;

                    if (!TypeHelper.IsDerivedFromType(typeof(Particle), classType))
                    {
                        Debug.LogError($"Class {classType.Name} does not derive from Particle!");
                        continue;
                    }
                    
                    var isDerivedFromGenericVariable = TypeHelper.IsDerivedFromType(typeof(GenericBaseVariable<>), classType);
                    var drawerType = isDerivedFromGenericVariable ? typeof(VariableDrawer<>) : typeof(ParticleDrawer<>);
                    
                    var drawer = GenerateParticleDrawerFromType(classType, classType.Namespace, ConvertFilePathToNamespace(saveDir), saveDir, drawerType);
                    saveDir = Path.Combine(projectDir, saveDir);
                    if (!Directory.Exists(saveDir))
                    {
                        Directory.CreateDirectory(saveDir);
                    }
                    
                    var savePath = Path.Combine(saveDir, $"{classType.Name}Drawer.cs");
                    File.WriteAllText(savePath, drawer);
                }
            }
        }
    }
}
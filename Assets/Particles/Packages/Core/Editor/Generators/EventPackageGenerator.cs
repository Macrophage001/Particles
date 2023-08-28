using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Particles.Packages.Core.Editor.Generators
{
    public class EventPackageGenerator : EditorWindow
    {
        private static readonly Dictionary<string, Type> ValueTypes = new()
        {
            { "bool", typeof(bool) },
            { "byte", typeof(byte) },
            { "sbyte", typeof(sbyte) },
            { "char", typeof(char) },
            { "decimal", typeof(decimal) },
            { "double", typeof(double) },
            { "float", typeof(float) },
            { "int", typeof(int) },
            { "uint", typeof(uint) },
            { "long", typeof(long) },
            { "ulong", typeof(ulong) },
            { "object", typeof(object) },
            { "short", typeof(short) },
            { "ushort", typeof(ushort) },
            { "string", typeof(string) }
        };

        private static readonly Dictionary<Type, string> TypesToString = new()
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(sbyte), "sbyte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(uint), "uint" },
            { typeof(long), "long" },
            { typeof(ulong), "ulong" },
            { typeof(object), "object" },
            { typeof(short), "short" },
            { typeof(ushort), "ushort" },
            { typeof(string), "string" }
        };
    
        private static string assetsDir;
        private static string projectDir;
        private static string templatesDir;

        private string className;
        private string[] typeNames;

        private int typeCount = 1;
        private int oldTypeCount = -1;

        private static readonly string[] templateNames = { "GenericEventChannelSOTemplate", "GenericListenerTemplate" };
        private static readonly string[] templateTypeNames = { "EventChannelSO", "EventListener" };
        private static readonly string[] folderPaths = { "Generated/EventChannels", "Generated/Listeners" };

        [MenuItem("Particle Tools/Event Package Generator")]
        public static void ShowWindow()
        {
            assetsDir = Application.dataPath;
            projectDir = Path.Combine(Directory.GetParent(assetsDir).FullName, "Assets");
            templatesDir = Path.Combine(projectDir, "Editor/Particles/Templates");

            GetWindow(typeof(EventPackageGenerator));
        }

        private void OnGUI()
        {
            GUILayout.Label("Event Package Generator", EditorStyles.boldLabel);
            GUILayout.Label("Type Count", EditorStyles.boldLabel);
            typeCount = EditorGUILayout.IntSlider(typeCount, 1, 5);
        
            className = EditorGUILayout.TextField("Class Name", className);

            GUILayout.Label("Type Names", EditorStyles.boldLabel);
            if (oldTypeCount != typeCount)
            {
                oldTypeCount = typeCount;
                typeNames = new string[typeCount];
            }
            for (int i = 0; i < typeCount; i++)
            {
                typeNames[i] = EditorGUILayout.TextField("Type " + i, typeNames[i]);
            }

            if (GUILayout.Button("Generate"))
            {
                Generate();
            }
        }

        private void Generate()
        {
            var validTypes = new List<Type>();
            foreach (var typeName in typeNames)
            {
                if (!TryFindType(typeName, out var type) && !ValueTypes.TryGetValue(typeName, out type)) 
                {
                    Debug.LogWarning($"Could not find type: {typeName}");
                    continue;
                }
                validTypes.Add(type);
            }
            GenerateFromTemplateWithTypes(validTypes, className, templateNames[0], templateTypeNames[0], folderPaths[0]);
            GenerateFromTemplateWithTypes(validTypes, className, templateNames[1], templateTypeNames[1], folderPaths[1]);
        }

        private bool TryFindType(string _typeName, out Type _type)
        {
            _type = default;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.Name.Equals(_typeName))
                    {
                        _type = type;
                        return true;
                    }
                }
            }
            return false;
        }

        private static string GetTemplateFile(string _templateName)
        {
            string _templatePath = Path.Combine(templatesDir, _templateName) + ".cs.template";
            return File.ReadAllText(_templatePath);
        }
    
        private static string BuildGenericTypesString(IReadOnlyList<Type> types)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < types.Count; i++)
            {
                stringBuilder.Append(TypesToString.TryGetValue(types[i], out var typeString) ? typeString : types[i].Name);
                if (i < types.Count - 1)
                    stringBuilder.Append(", ");
            }
            return stringBuilder.ToString();
        }

        private static string BuildUsingNamespaceString(IEnumerable<Type> types)
        {
            var stringHashSet = new HashSet<string>();
            foreach (var type in types.Where(t => string.IsNullOrEmpty(t.Namespace) == false))
            {
                stringHashSet.Add($"using {type.Namespace};\n");
            }
            return string.Join("", stringHashSet);
        }

        private static void GenerateFromTemplateWithTypes(IReadOnlyList<Type> types, string className, string templateName, string templateTypeName,
            string folderPath)
        {
            var namespaceString = BuildUsingNamespaceString(types);
            var genericTypesString = BuildGenericTypesString(types);
        
            var template = GetTemplateFile(templateName);
            var result = template
                .Replace("using TYPENAMESPACE;", namespaceString)
                .Replace("CLASSNAME", className)
                .Replace("TYPENAME", genericTypesString);

            var savePath = Path.Combine(projectDir, $"Scripts/{folderPath}");
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            var newFileName = $"{className}{templateTypeName}.cs";
            savePath = Path.Combine(savePath, newFileName);

            File.WriteAllText(savePath, result);
            AssetDatabase.Refresh();
        }

        static void GenerateFromTemplate(Type type, string templateName, string templateTypeName, string folderPath)
        {
            string template = GetTemplateFile(templateName);
            string result = template
                .Replace("using TYPENAMESPACE", type.Namespace != string.Empty ? $"using {type.Namespace}" : "")
                .Replace("TYPENAME", type.Name);

            string savePath = Path.Combine(projectDir, $"Scripts/{folderPath}");
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            string newFileName = $"{type.Name}{templateTypeName}.cs";
            savePath = Path.Combine(savePath, newFileName);

            File.WriteAllText(savePath, result);
            AssetDatabase.Refresh();
        }
    }
}

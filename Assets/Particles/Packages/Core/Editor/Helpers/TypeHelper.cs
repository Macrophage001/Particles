using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Particles.Packages.Core.Editor.Helpers
{
    public static class TypeHelper
    {
        private static readonly Dictionary<Type, string> TypeAliases = new()
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(sbyte), "sbyte" },
            { typeof(short), "short" },
            { typeof(ushort), "ushort" },
            { typeof(int), "int" },
            { typeof(uint), "uint" },
            { typeof(long), "long" },
            { typeof(ulong), "ulong" },
            { typeof(float), "float" },
            { typeof(double), "double" },
            { typeof(decimal), "decimal" },
            { typeof(string), "string"},
        };

        public static string GetTypeName(Type type)
        {
            if (type == null) return string.Empty;
            var typeName = string.Empty;
            var indexOfTilde = type.Name.IndexOf('`');
            if (indexOfTilde != -1) typeName = type.Name.Substring(0, indexOfTilde);
            else typeName = type.Name;
            
            return TypeAliases.TryGetValue(type, out string alias) ? alias : typeName;
        }
    
        public static bool TryGetTypeArgumentsFromGenericBaseClass(Type derivedType, out Type[] typeArguments)
        {
            var baseType = derivedType.BaseType;
            if (baseType == null)
            {
                typeArguments = null;
                return false;
            }
        
            typeArguments = null;
            if (!baseType.IsGenericType) return false;
            typeArguments = baseType.GetGenericArguments();
            return true;
        }

        public static bool IsDerivedFromType(Type baseType, Type derivedType)
        {
            var isDerivedFromGenericBase = false;
            while (derivedType != null && derivedType != typeof(object))
            {
                var cur = derivedType.IsGenericType ? derivedType.GetGenericTypeDefinition() : derivedType;
                isDerivedFromGenericBase = cur == baseType;
                if (isDerivedFromGenericBase)
                {
                    break;
                }

                derivedType = derivedType.BaseType;
            }
            return isDerivedFromGenericBase;
        }
    
        public static bool TryFindType(string typeName, out Type type)
        {
            type = default;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var assemblyType in assembly.GetTypes())
                {
                    if (assemblyType.Name.Equals(typeName))
                    {
                        type = assemblyType;
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool TryFindClassDerivingFromType(string baseTypeName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var assemblyType in assembly.GetTypes())
                {
                    var baseType = assemblyType.BaseType;

                    var baseTypeSignature =
                        $"{GetTypeName(baseType)}<{GetGenericTypeArgumentsString(new[] { baseType })}>";
                    if (baseType != null && baseTypeSignature.Equals(baseTypeName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static string GetGenericTypeArgumentsString(IEnumerable<Type> genericArgs)
        {
            var sb = new StringBuilder();
            sb.Append(string.Join(", ", genericArgs.Select(arg =>
            {
                if (arg == null) return string.Empty;
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
        
        /// <summary>
        /// Attempts to find a loaded type that directly derives from a specified generic type, represented by its string name.
        /// </summary>
        /// <param name="typeName">The string representation of the generic type, e.g., "Namespace.BaseType`1[System.String]".</param>
        /// <param name="foundType">The first type found that matches the criteria. Null if no type is found.</param>
        /// <returns>True if a matching type is found; otherwise, false.</returns> 
        public static bool TryFindSubtypeOfGenericString(string typeName, out Type foundType)
        {
            foundType = null;
            var sb = new StringBuilder();
            var a = Assembly.GetExecutingAssembly();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    sb.Clear();
                    if (type.BaseType == null || !type.BaseType.IsGenericType) continue;
                    var baseTypeName = GetTypeName(type.BaseType);
                    var genericArgsString = GetGenericTypeArgumentsString(type.BaseType.GetGenericArguments());
                    sb.Append(baseTypeName); sb.Append("<"); sb.Append(genericArgsString); sb.Append(">");
                    if (!sb.ToString().Equals(typeName)) continue;
                    foundType = type;
                    return true;
                }
            }
            return false;
        }
        
        public static bool TryFindSubtypesOfGenericString(string typeName, out Type[] foundTypes)
        {
            var validTypes = new List<Type>();
            foundTypes = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.BaseType == null || !type.BaseType.IsGenericType) continue;
                    var baseTypeName = GetTypeName(type.BaseType);
                    var genericArgsString = GetGenericTypeArgumentsString(type.BaseType.GetGenericArguments());
                    var fullTypeSignature = $"{baseTypeName}<{genericArgsString}>";
                    if (!fullTypeSignature.Equals(typeName)) continue;
                    validTypes.Add(type);
                }
            }
            foundTypes = validTypes.ToArray();
            return foundTypes.Length > 0;
        }
    }
}

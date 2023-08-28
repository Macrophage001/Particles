using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particles.Packages.Core.Runtime.Helpers
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
            var typeName = string.Empty;
            if (TypeAliases.TryGetValue(type, out var alias))
            {
                typeName = alias;
            }
            else
            {
                if (type.IsGenericType)
                {
                    var genericType = type.GetGenericTypeDefinition();
                    var genericArguments = type.GetGenericArguments();
                    typeName = $"{genericType.Name.Remove(genericType.Name.IndexOf('`'))}<{GetGenericTypeArgumentsString(genericArguments)}>";
                }
                else
                {
                    typeName = type.Name;
                }
            }
            
            return typeName;
        }
        
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

        public static bool TryFindClassDerivingFromType(Type baseType)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var assemblyType in assembly.GetTypes())
                {
                    if (assemblyType != baseType && IsDerivedFromType(baseType, assemblyType))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

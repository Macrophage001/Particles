using System;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class TypeHintAttribute : PropertyAttribute
    {
        public string TypeHint { get; }
        public bool IsGeneric { get; }


        public TypeHintAttribute(bool isGeneric = false)
        {
            IsGeneric = isGeneric;
        }
        public TypeHintAttribute(string typeHint, bool isGeneric = false)
        {
            TypeHint = typeHint;
            IsGeneric = isGeneric;
        }
    }
}

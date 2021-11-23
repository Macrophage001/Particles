using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Particles.Scripts.Functions.Constants;

namespace Particles.Scripts.Functions
{
    public struct None
    {
        internal static readonly None Default = new None();
    }
    
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Optional<T>
    {
        private bool m_HasValue;

        public T Value { get; }

        internal Optional(T _value)
        {
            m_HasValue = true;
            Value = _value;
        }

        public bool HasValue => m_HasValue;
        
        public static implicit operator Optional<T>(None _)
            => new Optional<T>();
        public static implicit operator Optional<T>(T _value)
            => _value == null ? Constants.None : Optional(_value);
        public static implicit operator bool(Optional<T> _optional)
            => _optional.HasValue;
        public TR Match<TR>(Func<TR> _none, Func<T, TR> _optional)
            => HasValue ? _optional(Value) : _none();

        public IEnumerable<T> AsEnumerable()
        {
            if (HasValue) yield return Value;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Particles.Scripts.ScriptableObjects.Variables
{
    public abstract class Variable<T> : ScriptableObject
    {
        public bool m_EnableEventHandling = true;
        public event Action<T> OnVariableChanged;

        public T _value;
        public T m_Value
        {
            get => _value;
            set
            {
                _value = value;
                if (m_EnableEventHandling) OnVariableChanged?.Invoke(_value);
            }
        }

        // This is probably unnecessary, however i'd like to come up with my equality comparisons for some values
        // and see if I can reduce GC Calls.
        public virtual bool Equals(T _value, T _other)
        {
            return _value.Equals(_other);
        }
    }

    public class ListVariable<T> : ScriptableObject
    {
        public bool m_ShouldReset;
        public delegate void VariableChange(List<T> _value);
        public event VariableChange OnVariableChanged;

        public List<T> _value;
        public List<T> m_Value
        {
            get => _value;
            set
            {
                _value = value;
            
                OnVariableChanged?.Invoke(_value);
            }
        }
    }

    public class SafeVariable<T> : ScriptableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }

        public T _value;
        public T m_Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                {
                    _value = value;
                    OnPropertyChanged("_value");
                }
            }
        }
    }
}
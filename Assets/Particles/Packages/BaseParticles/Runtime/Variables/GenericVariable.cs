using System;
using System.Collections.Generic;
using System.Linq;
using Particles.Packages.BaseParticles.Runtime.Events;
using Particles.Packages.Core.Runtime;
using Particles.Packages.Core.Runtime.Attributes;
using Particles.Packages.Core.Runtime.ScriptableObjects.Functions;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    public abstract class GenericBaseVariable<T> : Particle
    {
        [SerializeField] protected T value;
    }
    
    public abstract class GenericVariable<T> : GenericBaseVariable<T>, IEquatable<GenericVariable<T>> 
    {
        [HorizontalLine(2)]
        public GenericEvent<T> onVariableChanged;

        [Tooltip("Transforms to apply before assigning the value")]
        [SerializeField] protected List<ParticleFunction<T, T>> transformers;

        [SerializeField] private T initialValue;
        [SerializeField] private T oldValue;
        
        public T Value { get => value; set => SetValue(value); }
        public T InitialValue { get => initialValue; set => initialValue = value; }
        public T OldValue => oldValue;

        protected void OnEnable()
        {
            SetInitialValues();
        }

        private void SetInitialValues()
        {
            oldValue = initialValue;
            value = initialValue;
        }
        
        public bool SetValue(T newValue, bool forceEvent = false)
        {
            var preProcessedValue = RunTransformers(newValue);
            var changeValue = !Equals(preProcessedValue);
            var triggerEvents = changeValue || forceEvent;

            if (changeValue)
            {
                oldValue = value;
                value = preProcessedValue;
            }

            if (triggerEvents)
            {
                onVariableChanged.RaiseEvent(newValue);
            }

            return changeValue;
        }

        public bool SetValue(GenericVariable<T> variable)
        {
            return SetValue(variable.Value);
        }

        protected virtual T RunTransformers(T newValue)
        {
            if (transformers == null || transformers.Count == 0) return newValue;
            var result = newValue;
            foreach (var transformer in transformers.Where(t => t is not null))
            {
                result = transformer.Do(result);
            }
            return result;
        }

        private void OnValidate()
        {
            value = RunTransformers(value);
        }

        public bool Equals(GenericVariable<T> other)
        {
            return other == this;
        }
    }
}

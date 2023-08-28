using System;
using System.Collections.Generic;
using Particles.Packages.Core.Runtime;
using Particles.Packages.Core.Runtime.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Particles.Packages.BaseParticles.Runtime.Events
{
    public abstract class BaseEventInvocationProperties
    {
        public int invokerSourceId;
    }
    
    [Serializable]
    public class EventInvocationProperties<T> : BaseEventInvocationProperties
    {
        public T value;
        public void Deconstruct(out int invokerSourceId, out T value)
        {
            invokerSourceId = this.invokerSourceId;
            value = this.value;
        }
    }
    
    
    public abstract class GenericEvent<T> : Particle, ISerializationCallbackReceiver
    {
        [HorizontalLine(2)]
        public UnityAction<T> onEventRaised;
        
        [SerializeField] protected T inspectorValue;

        public T InspectorValue => inspectorValue;

#if UNITY_EDITOR
        private static HashSet<GenericEvent<T>> instances = new();
#endif

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (EditorSettings.enterPlayModeOptionsEnabled)
            {
                instances.Add(this);
            }
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            instances.Remove(this);
#endif
            UnregisterAll();
        }

        public void RaiseEvent(T obj)
        {
            onEventRaised?.Invoke(obj);
        }
        
        public void RegisterListener(UnityAction<T> action)
        {
            onEventRaised += action;
        }

        public void UnregisterListener(UnityAction<T> action)
        {
            onEventRaised -= action;
        }
        
        public void UnregisterAll()
        {
            onEventRaised = null;
        }

        public void OnBeforeSerialize() { }

        public virtual void OnAfterDeserialize()
        {
            if (onEventRaised != null)
            {
                foreach (var d in onEventRaised.GetInvocationList())
                {
                    onEventRaised -= (UnityAction<T>) d;
                }
            }
        }
    }

    public abstract class GenericEvent<T0, T1> : Particle, ISerializationCallbackReceiver
    {
        [HorizontalLine(2)]
        public UnityAction<T0, T1> onEventRaised;
        
        [SerializeField] protected T0 inspectorValue0;
        [SerializeField] protected T1 inspectorValue1;
        
        public T0 InspectorValue0 => inspectorValue0;
        public T1 InspectorValue1 => inspectorValue1;

        
#if UNITY_EDITOR
        private static HashSet<GenericEvent<T0, T1>> instances = new();
#endif

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (EditorSettings.enterPlayModeOptionsEnabled)
            {
                instances.Add(this);
            }
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            instances.Remove(this);
#endif
            UnregisterAll();
        }
        
        public void RegisterListener(UnityAction<T0, T1> action)
        {
            onEventRaised += action;
        }

        public void UnregisterListener(UnityAction<T0, T1> action)
        {
            onEventRaised -= action;
        }
        
        public void UnregisterAll()
        {
            onEventRaised = null;
        }
        
        public void RaiseEvent(T0 obj0, T1 obj1)
        {
            onEventRaised?.Invoke(obj0, obj1);
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            if (onEventRaised != null)
            {
                foreach (var d in onEventRaised.GetInvocationList())
                {
                    onEventRaised -= (UnityAction<T0, T1>) d;
                }
            }
        }
    }

    public abstract class GenericEvent<T0, T1, T2> : Particle, ISerializationCallbackReceiver
    {
        [HorizontalLine(2)]
        public UnityAction<T0, T1, T2> onEventRaised;
        
        [SerializeField] protected T0 inspectorValue0;
        [SerializeField] protected T1 inspectorValue1;
        [SerializeField] protected T2 inspectorValue2;
        
        public T0 InspectorValue0 => inspectorValue0;
        public T1 InspectorValue1 => inspectorValue1;
        public T2 InspectorValue2 => inspectorValue2;
        
#if UNITY_EDITOR
        private static HashSet<GenericEvent<T0, T1, T2>> instances = new();
#endif

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (EditorSettings.enterPlayModeOptionsEnabled)
            {
                instances.Add(this);
            }
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            instances.Remove(this);
#endif
            UnregisterAll();
        }
        
        public void RegisterListener(UnityAction<T0, T1, T2> action)
        {
            onEventRaised += action;
        }
        
        public void UnregisterListener(UnityAction<T0, T1, T2> action)
        {
            onEventRaised -= action;
        }
        
        public void RaiseEvent(T0 obj0, T1 obj1, T2 obj2)
        {
            onEventRaised?.Invoke(obj0, obj1, obj2);
        }
        
        public void UnregisterAll()
        {
            onEventRaised = null;
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            if (onEventRaised != null)
            {
                foreach (var d in onEventRaised.GetInvocationList())
                {
                    onEventRaised -= (UnityAction<T0, T1, T2>) d;
                }
            }
        }
    }
}
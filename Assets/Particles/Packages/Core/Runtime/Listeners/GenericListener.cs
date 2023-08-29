using System.Collections.Generic;
using System.Linq;
using Particles.Packages.BaseParticles.Runtime.Events;
using Particles.Packages.Core.Runtime.Actions;
using Particles.Packages.Core.Runtime.Attributes;
using Particles.Packages.Core.Runtime.Functions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Particles.Packages.Core.Runtime.Listeners
{
    public enum ParticleConditionOperators
    {
        And,
        Or
    }
    
    /// <summary>
    /// Just a way to provide the `DoIdsMatch` method to all generic listeners.
    /// This method helps prevent multiple subscribers from responding to one event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseGenericListener : MonoBehaviour
    {
        [Tooltip("To prevent multiple subscribers from responding to one event, set this to true; it ensures the event triggers only if the listener's game object ID matches the event raiser's ID.")]
        [SerializeField] protected bool matchInstanceIDsForEvent;

        protected virtual bool DoIdsMatch(object eventSource)
        {
            return eventSource switch
            {
                GameObject target => target.GetInstanceID().Equals(gameObject.GetInstanceID()),
                Component target => target.gameObject.GetInstanceID().Equals(gameObject.GetInstanceID()),
                ScriptableObject target => target.GetInstanceID().Equals(gameObject.GetInstanceID()),
                int instanceId => instanceId.Equals(gameObject.GetInstanceID()),
                _ => throw new UnityException($"Cannot match instance IDs for type {eventSource.GetType()}")
            };
        }
    }
    
    public abstract class GenericListener<T> : BaseGenericListener 
    {
        [TypeHint(true)]
        [SerializeField] private GenericEvent<T> channel;
    
        public UnityEvent<T> eventResponse;
        public UnityEvent<object, T> eventResponseWithSource;

        [TypeHint(true)]
        [SerializeField] private List<ParticleAction<T>> actionResponses;
        
        [TypeHint(true)]
        [SerializeField] private List<ParticleFunction<bool>> conditions;
        
        [SerializeField] private ParticleConditionOperators conditionOperator;
        
        private void OnEnable()
        {
            if (channel != null)
            {
                channel.onEventRaised += Response;
            }
        }
        private void OnDisable()
        {
            if (channel != null)
            {
                channel.onEventRaised -= Response;
            }
        }

        private void Response(T value)
        {
            if (matchInstanceIDsForEvent)
            {
                if (!DoIdsMatch(channel.EventSource)) return;
            }
            var shouldRespond = conditionOperator == ParticleConditionOperators.And;

            for (var i = 0; conditions != null && i < conditions.Count; i++)
            {
                var condition = conditions[i];
                if (condition == null) continue;
                shouldRespond = condition.Do();
                if (conditionOperator == ParticleConditionOperators.And && !shouldRespond) return;
                if (conditionOperator == ParticleConditionOperators.Or && shouldRespond) break;
            }

            eventResponse?.Invoke(value);
            eventResponseWithSource?.Invoke(channel.EventSource, value);
            
            foreach (var action in actionResponses.Where(action => action != null))
            {
                action.Do(value);
            }
        }
    }

    public abstract class GenericListener<T0, T1> : BaseGenericListener
    {
        [Tooltip("To prevent multiple subscribers from responding to one event, set this to true; it ensures the event triggers only if the listener's game object ID matches the event raiser's ID.")]
        [SerializeField] private bool matchInstanceIDsForEvent = true;
        
        [TypeHint(true)]
        [SerializeField] private GenericEvent<T0, T1> channel;

        public UnityEvent<T0, T1> eventResponse;
        public UnityEvent<object, T0, T1> eventResponseWithSource;

        [TypeHint(true)]
        [SerializeField] private List<ParticleAction<T0, T1>> actionResponses;
        
        [TypeHint(true)]
        [SerializeField] private List<ParticleFunction<bool>> conditions;
        
        [SerializeField] private ParticleConditionOperators conditionOperator;
        
        private void OnEnable()
        {
            if (channel != null)
            {
                channel.onEventRaised += Response;
            }
        }
        private void OnDisable()
        {
            if (channel != null)
            {
                channel.onEventRaised -= Response;
            }
        }

        protected virtual void Response(T0 t0, T1 t1)
        {
            if (matchInstanceIDsForEvent)
            {
                if (!DoIdsMatch(channel.EventSource)) return;
            }
            
            var shouldRespond = conditionOperator == ParticleConditionOperators.And;

            for (var i = 0; conditions != null && i < conditions.Count; i++)
            {
                var condition = conditions[i];
                if (condition == null) continue;
                shouldRespond = condition.Do();
                if (conditionOperator == ParticleConditionOperators.And && !shouldRespond) return;
                if (conditionOperator == ParticleConditionOperators.Or && shouldRespond) break;
            }
            eventResponse?.Invoke(t0, t1);
            foreach (var action in actionResponses)
            {
                if (action == null) continue;
                action.Do(t0, t1);
            }
        }
    }

    public abstract class GenericListener<T0, T1, T2> : BaseGenericListener 
    {
        [TypeHint(true)]
        [SerializeField] private GenericEvent<T0, T1, T2> channel;

    
        public UnityEvent<T0, T1, T2> eventResponse;
        public UnityEvent<object, T0, T1, T2> eventResponseWithSource;
        
        
        [TypeHint(true)]
        [SerializeField] private List<ParticleAction<T0, T1, T2>> actionResponses;
        
        
        [TypeHint(true)]
        [SerializeField] private List<ParticleFunction<bool>> conditions;
        
        
        [SerializeField] private ParticleConditionOperators conditionOperator;
        
    
        private void OnEnable()
        {
            if (channel != null)
            {
                channel.onEventRaised += Response;
            }
        }
        private void OnDisable()
        {
            if (channel != null)
            {
                channel.onEventRaised -= Response;
            }
        }
    
        private void Response(T0 t0, T1 t1, T2 t2)
        {
            if (matchInstanceIDsForEvent)
            {
                if (!DoIdsMatch(channel.EventSource)) return;
            }
            
            var shouldRespond = conditionOperator == ParticleConditionOperators.And;

            for (var i = 0; conditions != null && i < conditions.Count; i++)
            {
                var condition = conditions[i];
                if (condition == null) continue;
                shouldRespond = condition.Do();
                if (conditionOperator == ParticleConditionOperators.And && !shouldRespond) return;
                if (conditionOperator == ParticleConditionOperators.Or && shouldRespond) break;
            }
            
            eventResponse?.Invoke(t0, t1, t2);
            eventResponseWithSource?.Invoke(channel.EventSource, t0, t1, t2);
            
            foreach (var action in actionResponses)
            {
                if (action == null) continue;
                action.Do(t0, t1, t2);
            }
        }
    }
}
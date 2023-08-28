using System.Collections.Generic;
using System.Linq;
using Particles.Packages.BaseParticles.Runtime.Events;
using Particles.Packages.Core.Runtime.Attributes;
using Particles.Packages.Core.Runtime.ScriptableObjects.Actions;
using Particles.Packages.Core.Runtime.ScriptableObjects.Functions;
using UnityEngine;
using UnityEngine.Events;

namespace Particles.Packages.BaseParticles.Runtime.Listeners
{
    public enum ParticleConditionOperators
    {
        And,
        Or
    }
    
    public abstract class GenericListener<TType> : MonoBehaviour
    {
        [TypeHint(true)]
        [SerializeField] private GenericEvent<TType> channel;
    
        public UnityEvent<TType> eventResponse;

        [TypeHint(true)]
        [SerializeField] private List<ParticleAction<TType>> actionResponses;
        
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

        private void Response(TType value)
        {
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
            foreach (var action in actionResponses.Where(action => action != null))
            {
                action.Do(value);
            }
        }
    }

    public abstract class GenericListener<T0, T1> : MonoBehaviour
    {
        [TypeHint(true)]
        [SerializeField] private GenericEvent<T0, T1> channel;

        public UnityEvent<T0, T1> OnEventRaised;

        [TypeHint(true)]
        [SerializeField] private List<ParticleAction<T0, T1>> actionResponses;
        
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

        private void Response(T0 t0, T1 t1)
        {
            OnEventRaised?.Invoke(t0, t1);
            foreach (var action in actionResponses)
            {
                if (action == null) continue;
                action.Do(t0, t1);
            }
        }
    }

    public abstract class GenericListener<T0, T1, T2> : MonoBehaviour
    {
        [TypeHint(true)]
        [SerializeField] private GenericEvent<T0, T1, T2> channel;
    
        public UnityEvent<T0, T1, T2> OnEventRaised;
        
        [TypeHint(true)]
        [SerializeField] private List<ParticleAction<T0, T1, T2>> actionResponses;
    
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
            OnEventRaised?.Invoke(t0, t1, t2);
            foreach (var action in actionResponses)
            {
                if (action == null) continue;
                action.Do(t0, t1, t2);
            }
        }
    }
}
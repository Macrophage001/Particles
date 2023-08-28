using System.Collections.Generic;
using System.Linq;
using Particles.Packages.BaseParticles.Runtime.Events;
using Particles.Packages.Core.Runtime.Attributes;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    public abstract class GenericListVariable<L, T> : GenericVariable<L> 
        where L : List<T>, new()
    {
        [HorizontalLine(2)]
        public GenericEvent<EventInvocationProperties<L>> onVariablesAdded;
        public GenericEvent<EventInvocationProperties<T>> onVariableAdded;
        public GenericEvent<EventInvocationProperties<T>> onVariableRemoved;
        
        public void Add(T newValue, int instanceId = -1)
        {
            Value.Add(newValue);
            onVariableAdded.RaiseEvent(new EventInvocationProperties<T>(){invokerSourceId = instanceId, value = newValue});
        }
        private L RunTransformers(L newValue)
        {
            if (transformers == null || transformers.Count == 0) return newValue;
            var result = new L(); 
            foreach (var transformer in transformers.Where(t => t is not null)) result = transformer.Do(result);
            return result;
        }
        
        public void AddRange(L newValue, int instanceId = -1)
        {
            var preProcessedValue = RunTransformers(newValue);
            Value.AddRange(preProcessedValue);
            onVariablesAdded.RaiseEvent(new EventInvocationProperties<L>(){invokerSourceId = instanceId, value = preProcessedValue});
        }
        
        public void Remove(T value, int instanceId = -1)
        {
            Value.Remove(value);
            onVariableRemoved.RaiseEvent(new EventInvocationProperties<T>(){invokerSourceId = instanceId, value = value});
        }
        
        public void Clear()
        {
            Value.Clear();
        }
    }
}
using System.Collections.Generic;
using Particles.Packages.BaseParticles.Runtime.Events;
using Particles.Packages.Core.Runtime.ScriptableObjects.Actions;
using UnityEngine;
using UnityEngine.Events;

namespace Particles.Packages.BaseParticles.Runtime.Listeners
{
    public class VoidEventListener : MonoBehaviour
    {
        [SerializeField] private VoidEvent m_Channel;
        public UnityEvent OnEventRaised;
    
        [SerializeField] private List<ParticleAction> actionResponses;
    
        private void OnEnable()
        {
            if (m_Channel != null)
            {
                m_Channel.onEventRaised += Response;
            }
        }

        private void OnDisable()
        {
            if (m_Channel != null)
            {
                m_Channel.onEventRaised -= Response;
            }
        }

        private void Response()
        {
            OnEventRaised?.Invoke();
            actionResponses.ForEach(action => action.Do());
        }
    }
}
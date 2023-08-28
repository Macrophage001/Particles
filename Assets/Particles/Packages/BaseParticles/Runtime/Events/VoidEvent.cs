using Particles.Packages.Core.Runtime;
using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Particles.Packages.BaseParticles.Runtime.Events
{
    [GenerateEventEditor]
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Void/Event")]
    public class VoidEvent : Particle
    {
        public UnityAction onEventRaised;

        public void RaiseEvent()
        {
            onEventRaised?.Invoke();
        }
    }
}

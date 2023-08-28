using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Events
{
    [GenerateEventEditor]
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/String/Event")]
    public class StringEvent : GenericEvent<string> {}
}

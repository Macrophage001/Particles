using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Events
{
    [GenerateEventEditor]
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Bool/Event With Props")]
    public class BoolEventWithProps : GenericEventWithProps<bool> { }
}
using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Events
{
    [GenerateEventEditor]
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Vector2/Event")]
    public class Vector2Event : GenericEvent<Vector2> { }
}

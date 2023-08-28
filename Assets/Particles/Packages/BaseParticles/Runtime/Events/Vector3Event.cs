using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Events
{
    [GenerateEventEditor]
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Vector3/Event")]
    public class Vector3Event : GenericEvent<Vector3> { }
}
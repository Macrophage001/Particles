using Particles.Packages.Core.Runtime.Attributes;
using Particles.Packages.Core.Runtime.Variables;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/TimeSpan/Variable", fileName = "Time Span")]
    public class TimeSpanVariable : GenericVariable<System.TimeSpan> { }
}

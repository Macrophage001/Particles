using Particles.Packages.Core.Runtime.Attributes;
using Particles.Packages.Core.Runtime.Variables;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Double/Variable", fileName = "Double")]
    public class DoubleVariable : GenericVariable<double>
    { }
}

using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Gradient/Variable", fileName = "Gradient")]
    public class GradientVariable : GenericVariable<Gradient> {}
}

using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Bool/Variable", fileName = "Bool")]
    public class BoolVariable : GenericVariable<bool> {}
}

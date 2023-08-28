using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/UInt/Variable", fileName = "UInt")]
    public class UIntVariable : GenericVariable<uint> { }
}

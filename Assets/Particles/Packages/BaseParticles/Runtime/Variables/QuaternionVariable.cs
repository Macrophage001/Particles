using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Quaternion/Variable", fileName = "Quaternion")]
    public class QuaternionVariable : GenericVariable<Quaternion> { }
}

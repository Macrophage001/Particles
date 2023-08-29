using Particles.Packages.Core.Runtime.Attributes;
using Particles.Packages.Core.Runtime.Variables;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Vector3/Variable")]
    public class Vector3Variable : GenericVariable<Vector3> { }
}

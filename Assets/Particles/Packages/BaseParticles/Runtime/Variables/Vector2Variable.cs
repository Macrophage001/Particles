using Particles.Packages.Core.Runtime.Attributes;
using Particles.Packages.Core.Runtime.Variables;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Vector2/Variable")]
    public class Vector2Variable : GenericVariable<Vector2> {}
}

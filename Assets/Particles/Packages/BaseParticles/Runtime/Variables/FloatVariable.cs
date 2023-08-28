using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Float/Variable", fileName = "Float")]
    public class FloatVariable : GenericVariable<float> { }
}

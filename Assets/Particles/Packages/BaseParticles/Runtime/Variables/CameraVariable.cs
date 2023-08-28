using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Camera/Variable", fileName = "Camera")]
    public class CameraVariable : GenericVariable<Camera> {}
}

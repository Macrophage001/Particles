using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/DateTime/Variable", fileName = "Date Time")]
    public class DateTimeVariable : GenericVariable<System.DateTime> {}
}

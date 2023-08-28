﻿using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.Variables
{
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Int/Variable", fileName = "Int")]
    public class IntVariable : GenericVariable<int> { }
}
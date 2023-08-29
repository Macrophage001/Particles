using Particles.Packages.BaseParticles.Runtime.Variables;
using Particles.Packages.Core.Runtime.Actions;
using Particles.Packages.Core.Runtime.Variables;
using UnityEngine;

namespace Particles.Packages.BaseParticles.Runtime.SetVariables
{
    public abstract class SetVariable<T> : ParticleAction 
    {
        [SerializeField] private GenericVariable<T> variable;
        [SerializeField] private T value;
        public override void Do()
        {
            variable.Value = value;
        }
    }
}

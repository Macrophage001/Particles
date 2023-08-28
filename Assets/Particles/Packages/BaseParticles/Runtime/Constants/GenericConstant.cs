using Particles.Packages.BaseParticles.Runtime.Variables;

namespace Particles.Packages.BaseParticles.Runtime.Constants
{
    public abstract class GenericConstant<T> : GenericBaseVariable<T>
    {
        public T Value => value; 
    }
}
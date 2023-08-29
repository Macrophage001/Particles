using Particles.Packages.BaseParticles.Runtime.Variables;
using Particles.Packages.Core.Runtime.Variables;

namespace Particles.Packages.Core.Runtime
{
    public abstract class BaseVariableReference<T>
    {
        public bool useConstant;
        public T constantValue;
        public GenericVariable<T> variable;

        public BaseVariableReference() { }

        public BaseVariableReference(T value)
        {
            useConstant = true;
            constantValue = value;
        }

        public T Value => useConstant ? constantValue : variable.Value;

        public static implicit operator T(BaseVariableReference<T> reference)
        {
            return reference.Value;
        }
    }
}
namespace Particles.Packages.Core.Runtime.Actions
{
    public abstract class ParticleAction : Particle
    {
        public abstract void Do();
    }
    
    public abstract class ParticleAction<T0> : Particle
    {
        public abstract void Do(T0 value);
    }
    
    public abstract class ParticleAction<T0, T1> : Particle
    {
        public abstract void Do(T0 value0, T1 value1);
    }
    
    public abstract class ParticleAction<T0, T1, T2> : Particle
    {
        public abstract void Do(T0 value0, T1 value1, T2 value2);
    }
    
    public abstract class ParticleAction<T0, T1, T2, T3> : Particle
    {
        public abstract void Do(T0 value0, T1 value1, T2 value2, T3 value3);
    }
    
    public abstract class ParticleAction<T0, T1, T2, T3, T4> : Particle
    {
        public abstract void Do(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4);
    }
    
    public abstract class ParticleAction<T0, T1, T2, T3, T4, T5> : Particle
    {
        public abstract void Do(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5);
    }
}

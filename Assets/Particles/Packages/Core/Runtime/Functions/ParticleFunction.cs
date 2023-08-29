namespace Particles.Packages.Core.Runtime.Functions
{
    public abstract class ParticleFunction<R> : Particle
    {
        public abstract R Do();
    }
    
    public abstract class ParticleFunction<R, T> : Particle
    {
        public abstract R Do(T t);
    }
    
    public abstract class ParticleFunction<R, T1, T2> : Particle
    {
        public abstract R Do(T1 t1, T2 t2);
    }
    
    public abstract class ParticleFunction<R, T1, T2, T3> : Particle
    {
        public abstract R Do(T1 t1, T2 t2, T3 t3);
    }
    
    public abstract class ParticleFunction<R, T1, T2, T3, T4> : Particle
    {
        public abstract R Do(T1 t1, T2 t2, T3 t3, T4 t4);
    }
    
    public abstract class ParticleFunction<R, T1, T2, T3, T4, T5> : Particle
    {
        public abstract R Do(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
    }
}
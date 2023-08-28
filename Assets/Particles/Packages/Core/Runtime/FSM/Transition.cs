using Particles.Packages.Core.Runtime.Attributes;
using Particles.Packages.Core.Runtime.ScriptableObjects.Functions;

namespace Particles.Packages.Core.Runtime.FSM
{
    public abstract class Transition<TContext, TStateMachine> : Particle
        where TStateMachine : FiniteStateMachine<TContext>
    {
        public State<TContext, TStateMachine> from;
        public State<TContext, TStateMachine> to;
        
        [TypeHint(true)]
        public ParticleFunction<bool> condition;
    }
}
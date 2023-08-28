using Particles.Packages.Core.Runtime.ScriptableObjects.Actions;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.FSM
{
    public abstract class State<TContext, TStateMachine> : Particle
        where TStateMachine : FiniteStateMachine<TContext>
    {
        [SerializeField] private ParticleAction<TContext, TStateMachine> OnEnterAction;
        [SerializeField] private ParticleAction<TContext, TStateMachine> OnExitAction;
        [SerializeField] private ParticleAction<TContext, TStateMachine> TickAction;
        [SerializeField] private ParticleAction<TContext, TStateMachine> FixedTickAction;
        [SerializeField] private ParticleAction<TContext, TStateMachine> LateTickAction;
        
        public void OnEnter(TContext ctx, TStateMachine fsm)
        {
            if (OnEnterAction == null) return;
            OnEnterAction.Do(ctx, fsm);
        }
        
        public void OnExit(TContext ctx, TStateMachine fsm)
        {
            if (OnExitAction == null) return;
            OnExitAction.Do(ctx, fsm);
        }
        
        public void Tick(TContext ctx, TStateMachine fsm)
        {
            if (TickAction == null) return;
            TickAction.Do(ctx, fsm);
        }
        
        public void FixedTick(TContext ctx, TStateMachine fsm)
        {
            if (FixedTickAction == null) return;
            FixedTickAction.Do(ctx, fsm);
        }
        
        public void LateTick(TContext ctx, TStateMachine fsm)
        {
            if (LateTickAction == null) return;
            LateTickAction.Do(ctx, fsm);
        }
    }
}
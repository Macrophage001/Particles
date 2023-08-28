using System.Collections.Generic;
using Particles.Packages.BaseParticles.Runtime.Events;
using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.FSM
{
    public abstract class FiniteStateMachine<TContext> : ParticleBaseFiniteStateMachine
    {
        [Header("Finite State Machine Settings")]
        [HorizontalLine(2)]
        [Note("When generating the necessary types, make sure to create the necessary particle actions and functions as well.")]
        [TypeHint(true)]
        public State<TContext, FiniteStateMachine<TContext>> initialState;
        
        [TypeHint(true)]
        public State<TContext, FiniteStateMachine<TContext>> currentState;
        
        [TypeHint(true)]
        [SerializeField] protected List<Transition<TContext, FiniteStateMachine<TContext>>> transitions;
        [SerializeField] protected TContext context;
        [SerializeField] protected GenericEvent<State<TContext, FiniteStateMachine<TContext>>, State<TContext, FiniteStateMachine<TContext>>> onStateTransition;
        
        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null) SetState(transition.to);
            if (currentState != null)
            {
                currentState.Tick(context, this);
            }
        }
        
        public void FixedTick()
        {
            var transition = GetTransition();
            if (transition != null) SetState(transition.to);
            if (currentState != null)
            {
                currentState.FixedTick(context, this);
            }
        }
        
        public void LateTick()
        {
            var transition = GetTransition();
            if (transition != null) SetState(transition.to);
            if (currentState != null)
            {
                currentState.LateTick(context, this);
            }
        }

        public void SetState(State<TContext, FiniteStateMachine<TContext>> state)
        {
            if (state == currentState) return;
            if (currentState != null)
            {
                currentState.OnExit(context, this);
            }
            
            var prevState = currentState;
            currentState = state;

            if (onStateTransition != null)
            {
                onStateTransition.RaiseEvent(prevState, state);
            }

            if (currentState != null)
            {
                currentState.OnEnter(context, this);
            }
        }

        public void SetContext(TContext ctx)
        {
            this.context = ctx;
        }
        
        private Transition<TContext, FiniteStateMachine<TContext>> GetTransition()
        {
            Transition<TContext, FiniteStateMachine<TContext>> validTransition = null;
            foreach (var t in transitions)
            {
                if (t.condition.Do())
                {
                    validTransition = t;
                    break;
                }
            }

            return validTransition;
        }
    }
}
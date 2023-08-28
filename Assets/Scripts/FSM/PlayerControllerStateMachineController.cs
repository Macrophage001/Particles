using System;
using Particles.Packages.Core.Runtime.FSM;
using UnityEngine;

namespace FSM
{
    public class PlayerControllerStateMachineController : StateMachineController<PlayerController>
    {
        private void OnEnable()
        {
            if (context == null) throw new NullReferenceException("Make sure to assign the context to the state machine controller.");
            
            stateMachine.SetContext(context);
            stateMachine.SetState(stateMachine.initialState);
        }


        public void HealthChanged(int health)
        {
            Debug.Log($"New Health: {health}");
        }
    }
}
using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.FSM
{
    public abstract class StateMachineController<T> : MonoBehaviour
    {
        [TypeHint]
        [SerializeField]
        protected FiniteStateMachine<T> stateMachine;
        
        [SerializeField] protected T context;

        protected virtual void Update()
        {
            stateMachine.Tick();
        }

        protected virtual void FixedUpdate()
        {
            stateMachine.FixedTick();
        }
    
        protected virtual void LateUpdate()
        {
            stateMachine.LateTick();
        }
    }
}

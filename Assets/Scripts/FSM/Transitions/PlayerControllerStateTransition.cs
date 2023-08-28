using Particles.Packages.Core.Runtime.FSM;
using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(menuName = "Game/FSM/Transitions/Player Controller State Transition")]
    public class PlayerControllerStateTransition : Transition<PlayerController, FiniteStateMachine<PlayerController>> { }
}

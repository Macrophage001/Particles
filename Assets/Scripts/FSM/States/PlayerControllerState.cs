using Particles.Packages.Core.Runtime.FSM;
using UnityEngine;

namespace FSM.States
{
    [CreateAssetMenu(menuName = "Game/FSM/States/Player Controller State")]
    public class PlayerControllerState : State<PlayerController, FiniteStateMachine<PlayerController>> 
    {
    }
}

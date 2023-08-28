using Particles.Packages.Core.Runtime.FSM;
using UnityEngine;

namespace FSM.Actions
{
    [CreateAssetMenu(menuName = "Game/FSM/Actions/Player Alive Action")]
    public class PlayerAliveAction : PlayerControllerStateAction
    {
        public override void Do(PlayerController value0, FiniteStateMachine<PlayerController> value1)
        {
            Debug.Log("Player is alive!");
        }
    }
}

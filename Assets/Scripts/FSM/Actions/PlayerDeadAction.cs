using Particles.Packages.Core.Runtime.FSM;
using UnityEngine;

namespace FSM.Actions
{
    [CreateAssetMenu(menuName = "Game/FSM/Actions/Player Dead Action")]
    public class PlayerDeadAction : PlayerControllerStateAction 
    {
        public override void Do(PlayerController value0, FiniteStateMachine<PlayerController> value1)
        {
            Debug.Log("Player is dead!");
        }
    }
}

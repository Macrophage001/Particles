using Particles.Packages.Core.Runtime.FSM;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "Game/FSM/Enemy Controller Finite State Machine")]
    public class EnemyControllerStateMachine : FiniteStateMachine<EnemyController> { }
}

using Particles.Packages.BaseParticles.Runtime.Functions;
using Particles.Packages.BaseParticles.Runtime.Variables;
using Particles.Packages.Core.Runtime.ScriptableObjects.Functions;
using UnityEngine;

namespace FSM.Transitions.Actions
{
    [CreateAssetMenu(menuName = "Game/FSM/Transitions/Actions/Check Player Status Action")]
    public class CheckPlayerStatusAction : BoolFunction 
    {
        private enum PlayerStatus
        {
            Alive,
            Dead
        }

        [SerializeField] private BoolVariable playerAlive;
        [SerializeField] private PlayerStatus playerStatusToCheck;

        public override bool Do()
        {
            return playerStatusToCheck switch
            {
                PlayerStatus.Alive => playerAlive.Value == true,
                PlayerStatus.Dead => playerAlive.Value == false,
                _ => false
            };
        }
    }
}

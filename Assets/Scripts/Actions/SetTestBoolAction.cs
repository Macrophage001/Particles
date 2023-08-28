using Particles.Packages.BaseParticles.Runtime.Variables;
using Particles.Packages.Core.Runtime.ScriptableObjects.Actions;
using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Game/Set Variables/Test Bool")]
    public class SetTestBoolAction : ParticleAction<bool>
    {
        [SerializeField] private BoolVariable boolVariable;
        public override void Do(bool value)
        {
            boolVariable.Value = value;
        }
    }
}

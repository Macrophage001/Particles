using Particles.Packages.BaseParticles.Runtime.Constants;
using Particles.Packages.Core.Runtime.ScriptableObjects.Functions;
using UnityEngine;

namespace Transformers.Player
{
    [CreateAssetMenu(menuName = "Game/Functions/Clamp Int Value")]
    public class ClampIntValue : ParticleFunction<int, int>
    {
        [SerializeField] private IntConstant minValue;
        [SerializeField] private IntConstant maxValue;

        private int MinValue => minValue ? minValue.Value : 0;

        private int MaxValue => maxValue ? maxValue.Value : 0;

        public override int Do(int t) => Mathf.Clamp(t, MinValue, MaxValue);
    }
}

using System.Linq;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Input
{
    [CreateAssetMenu(menuName = "Particles/Input/Key Combination")]
    public class ParticleKeyCombination : Particle
    {
        [SerializeField] private ParticleKey[] particleKeys;
        
        public bool IsKeyCombination()
        {
            if (particleKeys.Where(p => p.IsModifierKey).Any(key => !key.IsKeyDown()))
            {
                return false;
            }
            return particleKeys.Where(p => !p.IsModifierKey).All(key => key.IsKey());
        }
    }
}
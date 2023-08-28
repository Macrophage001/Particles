using UnityEngine;

namespace Particles.Packages.Core.Runtime.Input
{
    [CreateAssetMenu(menuName = "Particles/Input/Key")]
    public class ParticleKey : Particle
    {
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private bool isModifierKey;
        
        public bool IsModifierKey => isModifierKey;

        public bool IsKey() => UnityEngine.Input.GetKey(keyCode);
        public bool IsKeyDown() => UnityEngine.Input.GetKeyDown(keyCode);
        public bool IsKeyUp() => UnityEngine.Input.GetKeyUp(keyCode);
    }
}
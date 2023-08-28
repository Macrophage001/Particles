using Particles.Packages.Core.Runtime.Input;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public class OnKeyDownHook : VoidHook
    {
        [SerializeField] private ParticleKey particleKey;
        
        private void Update()
        {
            if (particleKey.IsKeyDown())
            {
                OnHook();
            }
        }
    }
}
using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public abstract class VoidHook : MonoBehaviour
    {
        [SerializeField] private VoidEvent voidEvent;
        protected void OnHook()
        {
            voidEvent.RaiseEvent();
        }
    }
}
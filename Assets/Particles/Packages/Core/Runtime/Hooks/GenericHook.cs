using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public class GenericHook<E, T> : MonoBehaviour
        where E : GenericEvent<T>
    {
        [SerializeField] private E eventChannelSO;
        protected void OnHook(T value)
        {
            eventChannelSO.RaiseEvent(value);
        }
    }
}
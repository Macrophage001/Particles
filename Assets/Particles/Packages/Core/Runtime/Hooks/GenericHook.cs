using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public class GenericHook<T, E, EV> : MonoBehaviour
        where EV : EventInvocationProperties<T>, new()
        where E : GenericEvent<EV>
    {
        [SerializeField] private E eventChannelSO;
        protected void OnHook(EV value)
        {
            eventChannelSO.RaiseEvent(value);
        }
    }
}
using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public class Collider2DHook : GenericHook<
        Collider2D, 
        GenericEvent<EventInvocationProperties<Collider2D>>, 
        EventInvocationProperties<Collider2D>> { }
}
using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public class Collision2DHook : GenericHook<
        Collision2D,
        GenericEvent<EventInvocationProperties<Collision2D>>,
        EventInvocationProperties<Collision2D>> { }
}
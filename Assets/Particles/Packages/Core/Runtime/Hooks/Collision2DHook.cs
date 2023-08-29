using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public class Collision2DHook : GenericHook<
        GenericEvent<Collision2D>,
        Collision2D> { }
}
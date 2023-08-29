using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public class Collider2DHook : GenericHook<
        GenericEvent<Collider2D>, 
        Collider2D> { }
}
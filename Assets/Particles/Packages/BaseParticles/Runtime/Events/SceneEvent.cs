using Particles.Packages.Core.Runtime.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Particles.Packages.BaseParticles.Runtime.Events
{
    [GenerateEventEditor]
    [GenerateParticleDrawer]
    [CreateAssetMenu(menuName = "Particles/Scene/Event")]
    public class SceneEvent : GenericEvent<Scene, LoadSceneMode>
    { }
}

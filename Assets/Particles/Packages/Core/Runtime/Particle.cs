using UnityEngine;

namespace Particles.Packages.Core.Runtime
{
    public class Particle : ScriptableObject
    {
        public string Id => id;
        public string Description => description;
        
        [SerializeField] private string id;
        
        [SerializeField]
        [TextArea(3, 6)]
        private string description;
    }
}
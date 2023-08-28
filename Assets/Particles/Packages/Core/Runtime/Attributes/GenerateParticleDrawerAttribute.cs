using System;

namespace Particles.Packages.Core.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenerateParticleDrawerAttribute : Attribute 
    {
        public string filePath { get; }
        public GenerateParticleDrawerAttribute(string filePath = "")
        {
            this.filePath = filePath;
        }
    }
}

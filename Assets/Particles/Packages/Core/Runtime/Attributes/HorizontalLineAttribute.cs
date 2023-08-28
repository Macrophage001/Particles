using UnityEngine;

namespace Particles.Packages.Core.Runtime.Attributes
{
    public class HorizontalLineAttribute : PropertyAttribute
    {
        public int Thickness { get; }
        public float Padding { get; }
        
        public HorizontalLineAttribute(int thickness = 1, float padding = 10)
        {
            Thickness = thickness;
            Padding = padding;
        }
    }
}
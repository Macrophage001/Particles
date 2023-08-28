using System;

namespace Particles.Packages.Core.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenerateEventEditorAttribute : Attribute
    {
        public string FilePath { get; }
        
        public GenerateEventEditorAttribute(string filePath = "")
        {
            FilePath = filePath;
        }
    }
}
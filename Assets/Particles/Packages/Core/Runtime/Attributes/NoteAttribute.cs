using UnityEditor;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Attributes
{
    public class NoteAttribute : PropertyAttribute
    {
        public string Text { get; }
        public MessageType MessageType { get; }
        
        public NoteAttribute(string text, MessageType messageType = MessageType.Info)
        {
            Text = text;
            MessageType = messageType;
        }
    }
}
using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public enum Trigger2DHookType
    {
        Enter,
        Exit,
        Stay
    }
    
    public class OnTrigger2DHook : Collider2DHook
    {
        [SerializeField] private Trigger2DHookType triggerType;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (triggerType == Trigger2DHookType.Enter) OnHook(other);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (triggerType == Trigger2DHookType.Exit) OnHook(other);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (triggerType == Trigger2DHookType.Stay) OnHook(other);
        }
    }
}
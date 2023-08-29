using Particles.Packages.BaseParticles.Runtime.Events;
using UnityEngine;

namespace Particles.Packages.Core.Runtime.Hooks
{
    public enum Collision2DHookType
    {
        Enter,
        Exit,
        Stay
    }
    
    public class OnCollision2DHook : Collision2DHook
    {
        [SerializeField] private Collision2DHookType collisionType;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (collisionType == Collision2DHookType.Enter) OnHook(other);
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if (collisionType == Collision2DHookType.Exit) OnHook(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (collisionType == Collision2DHookType.Stay)
                OnHook(other);
        }
    }
}
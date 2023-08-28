namespace Particles.Packages.Core.Runtime.Hooks
{
    public class OnAwakeHook : VoidHook 
    {
        private void Awake()
        {
            OnHook();
        }
    }
}